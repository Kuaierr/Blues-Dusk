using GameKit;
using GameKit.Setting;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Setting Component")]
    public sealed class SettingComponent : GameKitComponent
    {
        private ISettingManager m_SettingManager = null;

        [SerializeField]
        private string m_SettingHelperTypeName = "UnityGameKit.Runtime.DefaultSettingHelper";

        [SerializeField]
        private SettingHelperBase m_CustomSettingHelper = null;

        public int CurrentSaveIndex { get; private set; } = 0;
        private const string _currentSaveIndex = "CurrentSaveIndex"; 

        public int Count
        {
            get { return m_SettingManager.Count; }
        }

        protected override void Awake()
        {
            base.Awake();

            m_SettingManager = GameKitModuleCenter.GetModule<ISettingManager>();
            if (m_SettingManager == null)
            {
                Log.Fatal("Setting manager is invalid.");
                return;
            }

            SettingHelperBase settingHelper = Helper.CreateHelper(m_SettingHelperTypeName, m_CustomSettingHelper);
            if (settingHelper == null)
            {
                Log.Error("Can not create setting helper.");
                return;
            }

            settingHelper.name = "Setting Helper";
            Transform transform = settingHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_SettingManager.SetSettingHelper(settingHelper);
        }

        private void Start()
        {
            if (!m_SettingManager.Load())
            {
                Log.Error("Load settings failure.");
            }
        }

        public void Load()
        {
            m_SettingManager.Load();
        }

        public void Save()
        {
            m_SettingManager.Save();
            PlayerPrefs.SetInt(_currentSaveIndex, CurrentSaveIndex);
        }

        public void SetCurrentSaveIndex(int i)
        {
            if (i < 0 && i > 3) 
                Debug.LogError("SaveData Out of Index");
            CurrentSaveIndex = i;
        }

        public string[] GetAllSettingNames()
        {
            return m_SettingManager.GetAllSettingNames();
        }

        public void GetAllSettingNames(List<string> results)
        {
            m_SettingManager.GetAllSettingNames(results);
        }

        public bool HasSetting(string settingName)
        {
            return m_SettingManager.HasSetting(settingName);
        }

        public void RemoveSetting(string settingName)
        {
            m_SettingManager.RemoveSetting(settingName);
        }

        public void RemoveAllSettings()
        {
            m_SettingManager.RemoveAllSettings();
        }

        public bool GetBool(string settingName)
        {
            return m_SettingManager.GetBool(settingName);
        }

        public bool GetBool(string settingName, bool defaultValue)
        {
            return m_SettingManager.GetBool(settingName, defaultValue);
        }

        public void SetBool(string settingName, bool value)
        {
            m_SettingManager.SetBool(settingName, value);
        }

        public int GetInt(string settingName)
        {
            return m_SettingManager.GetInt(settingName);
        }

        public int GetInt(string settingName, int defaultValue)
        {
            return m_SettingManager.GetInt(settingName, defaultValue);
        }

        public void SetInt(string settingName, int value)
        {
            m_SettingManager.SetInt(settingName, value);
        }

        public float GetFloat(string settingName)
        {
            return m_SettingManager.GetFloat(settingName);
        }

        public float GetFloat(string settingName, float defaultValue)
        {
            return m_SettingManager.GetFloat(settingName, defaultValue);
        }

        public void SetFloat(string settingName, float value)
        {
            m_SettingManager.SetFloat(settingName, value);
        }

        public string GetString(string settingName)
        {
            return m_SettingManager.GetString(settingName);
        }

        public string GetString(string settingName, string defaultValue)
        {
            return m_SettingManager.GetString(settingName, defaultValue);
        }

        public void SetString(string settingName, string value)
        {
            m_SettingManager.SetString(settingName, value);
        }

        public T GetObject<T>(string settingName)
        {
            return m_SettingManager.GetObject<T>(settingName);
        }

        public object GetObject(Type objectType, string settingName)
        {
            return m_SettingManager.GetObject(objectType, settingName);
        }

        public T GetObject<T>(string settingName, T defaultObj)
        {
            return m_SettingManager.GetObject(settingName, defaultObj);
        }

        public object GetObject(Type objectType, string settingName, object defaultObj)
        {
            return m_SettingManager.GetObject(objectType, settingName, defaultObj);
        }

        public void SetObject<T>(string settingName, T obj)
        {
            m_SettingManager.SetObject(settingName, obj);
        }

        public void SetObject(string settingName, object obj)
        {
            m_SettingManager.SetObject(settingName, obj);
        }
    }
}