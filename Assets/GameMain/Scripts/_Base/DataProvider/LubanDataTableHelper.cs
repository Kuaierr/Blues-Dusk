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

        public override bool ReadExternalData(DataTableBase dataTable, string dataTableData, object userData)
        {
            string rawData = DataTable.instance.GetRawString(dataTableData);
            if (rawData == null)
            {
                Log.Fail("Can not to load external data {0}", dataTableData);
                return false;
            }
            return dataTable.ParseData(rawData, userData);
        }

        public override bool ParseData(DataTableBase dataTable, string dataTableString, object userData)
        {
            try
            {
                int position = 0;
                string configLineString = null;
                while ((configLineString = dataTableString.ReadLine(ref position)) != null)
                {
                    configLineString = configLineString.RemoveBrackets().RemoveLast();
                    Log.Info(configLineString);
                    string[] splitedLine = configLineString.Split(',');
                    for (int i = 0; i < splitedLine.Length; i++)
                    {
                        string[] splitedField = splitedLine[i].Split(':');
                        string dataRowName = splitedField[0].Correction();
                        string dataRowValue = splitedField[1].Correction();
                        Log.Info(dataRowName);
                        if (!dataTable.AddDataRow(dataRowName, dataRowValue))
                        {
                            Log.Warning("Can not add config with config name '{0}' which may be invalid or duplicate.", dataRowName);
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse config string with exception '{0}'.", exception);
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
