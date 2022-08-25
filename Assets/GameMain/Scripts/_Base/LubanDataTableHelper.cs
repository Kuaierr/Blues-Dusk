using GameKit;
using GameKit.DataTable;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public class LubanDataTableHelper : DataTableHelperBase
    {
        private static readonly string BytesAssetExtension = ".bytes";

        // private ResourceComponent m_ResourceComponent = null;

        public override bool ReadData(DataTableBase dataTable, string dataTableAssetName, object dataTableAsset, object userData)
        {
            TextAsset dataTableTextAsset = dataTableAsset as TextAsset;
            if (dataTableTextAsset != null)
            {
                if (dataTableAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
                {
                    return dataTable.ParseData(dataTableTextAsset.bytes, userData);
                }
                else
                {
                    return dataTable.ParseData(dataTableTextAsset.text, userData);
                }
            }

            Log.Warning("Data table asset '{0}' is invalid.", dataTableAssetName);
            return false;
        }

        public override bool ReadData(DataTableBase dataTable, string dataTableAssetName, byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            if (dataTableAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
            {
                return dataTable.ParseData(dataTableBytes, startIndex, length, userData);
            }
            else
            {
                return dataTable.ParseData(Utility.Converter.GetString(dataTableBytes, startIndex, length), userData);
            }
        }

        public override bool ParseData(DataTableBase dataTable, string dataTableString, object userData)
        {
            try
            {
                int position = 0;
                string dataRowString = null;
                while ((dataRowString = dataTableString.ReadLine(ref position)) != null)
                {
                    if (dataRowString[0] == '#')
                    {
                        continue;
                    }

                    if (!dataTable.AddDataRow(dataRowString, userData))
                    {
                        Log.Warning("Can not parse data row string '{0}'.", dataRowString);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse data table string with exception '{0}'.", exception);
                return false;
            }
        }

        public override bool ParseData(DataTableBase dataTable, byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(dataTableBytes, startIndex, length, false))
                {
                    using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                    {
                        while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                        {
                            int dataRowBytesLength = binaryReader.Read7BitEncodedInt32();
                            if (!dataTable.AddDataRow(dataTableBytes, (int)binaryReader.BaseStream.Position, dataRowBytesLength, userData))
                            {
                                Log.Warning("Can not parse data row bytes.");
                                return false;
                            }

                            binaryReader.BaseStream.Position += dataRowBytesLength;
                        }
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse dictionary bytes with exception '{0}'.", exception);
                return false;
            }
        }

        public override bool ParseInternalData(object internalRawData, byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            IDataRow rowData = (IDataRow)internalRawData;
            rowData.ParseDataRow(dataRowBytes, startIndex, length, userData);
            return true;
        }

        public override bool ParseInternalData(object internalRawData, string dataRowString, object userData)
        {
            IDataRow rowData = (IDataRow)internalRawData;
            rowData.ParseDataRow(dataRowString, userData);
            return true;
        }

        public override bool ParseInternalData(object internalRawData, object dataRaw, object userData)
        {
            IDataRow rowData = (IDataRow)internalRawData;
            rowData.ParseDataRow(dataRaw.ToString(), userData);
            return true;
        }

        public override int ParseExternalDataId(object dataRaw)
        {
            IDataRow rowData = (IDataRow)dataRaw;
            return rowData.Id;
        }

        public override void ReleaseDataAsset(DataTableBase dataTable, object dataTableAsset)
        {
            // m_ResourceComponent.UnloadAsset(dataTableAsset);
        }

        private void Start()
        {
            // m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            // if (m_ResourceComponent == null)
            // {
            //     Log.Fatal("Resource component is invalid.");
            //     return;
            // }
        }
    }
}
