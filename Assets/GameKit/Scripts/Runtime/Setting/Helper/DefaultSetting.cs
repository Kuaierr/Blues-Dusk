using System.Net.Mime;
using GameKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnityGameKit.Runtime
{
    public sealed class DefaultSetting
    {
        private readonly SortedDictionary<string, string> m_Settings = new SortedDictionary<string, string>(StringComparer.Ordinal);

        public DefaultSetting()
        {
        }

        public int Count
        {
            get
            {
                return m_Settings.Count;
            }
        }

        public string[] GetAllSettingNames()
        {
            int index = 0;
            string[] allSettingNames = new string[m_Settings.Count];
            foreach (KeyValuePair<string, string> setting in m_Settings)
            {
                allSettingNames[index++] = setting.Key;
            }

            return allSettingNames;
        }

        public void GetAllSettingNames(List<string> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, string> setting in m_Settings)
            {
                results.Add(setting.Key);
            }
        }

        public bool HasSetting(string settingName)
        {
            return m_Settings.ContainsKey(settingName);
        }

        public bool RemoveSetting(string settingName)
        {
            return m_Settings.Remove(settingName);
        }

        public void RemoveAllSettings()
        {
            m_Settings.Clear();
        }

        public bool GetBool(string settingName)
        {
            if (!m_Settings.ContainsKey(settingName))
            {
                Log.Warning("Setting '{0}' is not exist.", settingName);
                return false;
            }
            return Convert.ToBoolean(m_Settings[settingName]);
        }

        public bool GetBool(string settingName, bool defaultValue)
        {
            if (!m_Settings.ContainsKey(settingName))
            {
                Log.Warning("Setting '{0}' is not exist.", settingName);
                return defaultValue;
            }
            return Convert.ToBoolean(m_Settings[settingName]);
        }

        public void SetBool(string settingName, bool value)
        {
            m_Settings[settingName] = value ? "True" : "False";
        }

        public int GetInt(string settingName)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                Log.Warning("Setting '{0}' is not exist.", settingName);
                return 0;
            }

            return int.Parse(value);
        }

        public int GetInt(string settingName, int defaultValue)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                return defaultValue;
            }

            return int.Parse(value);
        }

        public void SetInt(string settingName, int value)
        {
            m_Settings[settingName] = value.ToString();
        }

        public float GetFloat(string settingName)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                Log.Warning("Setting '{0}' is not exist.", settingName);
                return 0f;
            }

            return float.Parse(value);
        }

        public float GetFloat(string settingName, float defaultValue)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                return defaultValue;
            }

            return float.Parse(value);
        }

        public void SetFloat(string settingName, float value)
        {
            m_Settings[settingName] = value.ToString();
        }

        public string GetString(string settingName)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                Log.Warning("Setting '{0}' is not exist.", settingName);
                return null;
            }

            return value;
        }

        public string GetString(string settingName, string defaultValue)
        {
            string value = null;
            if (!m_Settings.TryGetValue(settingName, out value))
            {
                return defaultValue;
            }

            return value;
        }

        public void SetString(string settingName, string value)
        {
            m_Settings[settingName] = value;
        }

        public void Serialize(Stream stream)
        {

            // foreach (KeyValuePair<string, string> setting in m_Settings)
            // {
            //     Log.Info(setting.Key + " : " + setting.Value);
            // }

            using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8))
            {
                binaryWriter.Write7BitEncodedInt32(m_Settings.Count);
                foreach (KeyValuePair<string, string> setting in m_Settings)
                {
                    binaryWriter.Write(setting.Key);
                    binaryWriter.Write(setting.Value);
                }
            }
        }

        public void Deserialize(Stream stream)
        {
            m_Settings.Clear();
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
            {
                int settingCount = binaryReader.Read7BitEncodedInt32();
                for (int i = 0; i < settingCount; i++)
                {
                    m_Settings.Add(binaryReader.ReadString(), binaryReader.ReadString());
                }
            }

            // foreach (KeyValuePair<string, string> setting in m_Settings)
            // {
            //     Log.Info(setting.Key + " : " + setting.Value);
            // }
        }
    }
}
