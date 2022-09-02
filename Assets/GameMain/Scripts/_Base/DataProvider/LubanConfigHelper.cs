

using GameKit;
using GameKit.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameKit.Runtime;

public class LubanConfigHelper : ConfigHelperBase
{
    private static readonly string[] ColumnSplitSeparator = new string[] { "\t" };
    private static readonly string BytesAssetExtension = ".bytes";
    private const int ColumnCount = 4;

    // private ResourceComponent m_ResourceComponent = null;

    public override bool ReadData(IConfigManager configManager, string configAssetName, object configAsset, object userData)
    {
        TextAsset configTextAsset = configAsset as TextAsset;
        if (configTextAsset != null)
        {
            if (configAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
            {
                return configManager.ParseData(configTextAsset.bytes, userData);
            }
            else
            {
                return configManager.ParseData(configTextAsset.text, userData);
            }
        }

        Log.Warning("Config asset '{0}' is invalid.", configAssetName);
        return false;
    }

    public override bool ReadData(IConfigManager configManager, string configAssetName, byte[] configBytes, int startIndex, int length, object userData)
    {
        if (configAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
        {
            return configManager.ParseData(configBytes, startIndex, length, userData);
        }
        else
        {
            return configManager.ParseData(Utility.Converter.GetString(configBytes, startIndex, length), userData);
        }
    }

    public override bool ReadExternalData(IConfigManager configManager, string configAssetName, object userData)
    {
        string rawData = DataTable.instance.GetRawString(configAssetName);
        if (rawData == null)
        {
            Log.Fail("Can not to load external data {0}", configAssetName);
            return false;
        }
        return configManager.ParseData(rawData, userData);
    }

    public override bool ParseData(IConfigManager configManager, string configString, object userData)
    {
        try
        {
            int position = 0;
            string configLineString = null;
            while ((configLineString = configString.ReadLine(ref position)) != null)
            {
                configLineString = configLineString.RemoveBrackets().RemoveLast();
                Log.Info(configLineString);
                string[] splitedLine = configLineString.Split(',');
                for (int i = 0; i < splitedLine.Length; i++)
                {
                    string[] splitedField = splitedLine[i].Split(':');
                    string configName = splitedField[0].Correction();
                    string configValue = splitedField[1].Correction();
                    // Log.Info(configName);
                    if (!configManager.AddConfig(configName, configValue))
                    {
                        Log.Warning("Can not add config with config name '{0}' which may be invalid or duplicate.", configName);
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

    public override bool ParseData(IConfigManager configManager, byte[] configBytes, int startIndex, int length, object userData)
    {
        try
        {
            using (MemoryStream memoryStream = new MemoryStream(configBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        string configName = binaryReader.ReadString();
                        string configValue = binaryReader.ReadString();
                        if (!configManager.AddConfig(configName, configValue))
                        {
                            Log.Warning("Can not add config with config name '{0}' which may be invalid or duplicate.", configName);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        catch (Exception exception)
        {
            Log.Warning("Can not parse config bytes with exception '{0}'.", exception);
            return false;
        }
    }

    public override void ReleaseDataAsset(IConfigManager configManager, object configAsset)
    {
        // m_ResourceComponent.UnloadAsset(configAsset);
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

