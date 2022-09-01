using System;
using System.Collections.Generic;

namespace GameKit.Setting
{
    internal sealed class SettingManager : GameKitModule, ISettingManager
    {
        private ISettingHelper m_SettingHelper;

        public SettingManager()
        {
            m_SettingHelper = null;
        }

        public int Count
        {
            get
            {
                if (m_SettingHelper == null)
                {
                    throw new GameKitException("Setting helper is invalid.");
                }

                return m_SettingHelper.Count;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
            Save();
        }

        public void SetSettingHelper(ISettingHelper settingHelper)
        {
            if (settingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            m_SettingHelper = settingHelper;
        }

        public bool Load()
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            return m_SettingHelper.Load();
        }

        public bool Save()
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            return m_SettingHelper.Save();
        }

        public string[] GetAllSettingNames()
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            return m_SettingHelper.GetAllSettingNames();
        }

        public void GetAllSettingNames(List<string> results)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            m_SettingHelper.GetAllSettingNames(results);
        }

        public bool HasSetting(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.HasSetting(settingName);
        }

        public bool RemoveSetting(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.RemoveSetting(settingName);
        }

        public void RemoveAllSettings()
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            m_SettingHelper.RemoveAllSettings();
        }

        public bool GetBool(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetBool(settingName);
        }

        public bool GetBool(string settingName, bool defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetBool(settingName, defaultValue);
        }

        public void SetBool(string settingName, bool value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetBool(settingName, value);
        }

        public int GetInt(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetInt(settingName);
        }

        public int GetInt(string settingName, int defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetInt(settingName, defaultValue);
        }

        public void SetInt(string settingName, int value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetInt(settingName, value);
        }

        public float GetFloat(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetFloat(settingName);
        }

        public float GetFloat(string settingName, float defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetFloat(settingName, defaultValue);
        }

        public void SetFloat(string settingName, float value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetFloat(settingName, value);
        }

        public string GetString(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetString(settingName);
        }

        public string GetString(string settingName, string defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetString(settingName, defaultValue);
        }

        public void SetString(string settingName, string value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetString(settingName, value);
        }

        public T GetObject<T>(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject<T>(settingName);
        }

        public object GetObject(Type objectType, string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (objectType == null)
            {
                throw new GameKitException("Object type is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(objectType, settingName);
        }

        public T GetObject<T>(string settingName, T defaultObj)
        {
            Utility.Debugger.LogSuccess("GetObject {0}-{1}", settingName, defaultObj);
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(settingName, defaultObj);
        }

        public object GetObject(Type objectType, string settingName, object defaultObj)
        {
            Utility.Debugger.LogSuccess("GetObject {0}-{1}", settingName, defaultObj);
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (objectType == null)
            {
                throw new GameKitException("Object type is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(objectType, settingName, defaultObj);
        }

        public void SetObject<T>(string settingName, T obj)
        {
            Utility.Debugger.LogSuccess("SetObject {0}-{1}", settingName, obj);
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetObject(settingName, obj);
        }

        public void SetObject(string settingName, object obj)
        {
            Utility.Debugger.LogSuccess("SetObject {0}-{1}", settingName, obj);
            if (m_SettingHelper == null)
            {
                throw new GameKitException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameKitException("Setting name is invalid.");
            }

            m_SettingHelper.SetObject(settingName, obj);
        }
    }
}
