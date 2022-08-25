using GameKit;
using GameKit.Localization;
// using GameKit.Resource;
using UnityEngine;

namespace UnityGameKit.Runtime
{

    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Localization Component")]
    public sealed class LocalizationComponent : GameKitComponent
    {
        private const int DefaultPriority = 0;

        private ILocalizationManager m_LocalizationManager = null;
        private EventComponent m_EventComponent = null;

        [SerializeField]
        private string m_LocalizationHelperTypeName = "UnityGameKit.Runtime.DefaultLocalizationHelper";

        [SerializeField]
        private LocalizationHelperBase m_CustomLocalizationHelper = null;

        [SerializeField]
        private int m_CachedBytesSize = 0;

        public Language Language
        {
            get
            {
                return m_LocalizationManager.Language;
            }
            set
            {
                m_LocalizationManager.Language = value;
            }
        }

        public Language SystemLanguage
        {
            get
            {
                return m_LocalizationManager.SystemLanguage;
            }
        }

        public int DictionaryCount
        {
            get
            {
                return m_LocalizationManager.DictionaryCount;
            }
        }

        public int CachedBytesSize
        {
            get
            {
                return m_LocalizationManager.CachedBytesSize;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_LocalizationManager = GameKitModuleCenter.GetModule<ILocalizationManager>();
            if (m_LocalizationManager == null)
            {
                Log.Fatal("Localization manager is invalid.");
                return;
            }

            m_LocalizationManager.ReadDataSuccess += OnReadDataSuccess;
            m_LocalizationManager.ReadDataFailure += OnReadDataFailure;
        }

        private void Start()
        {
            GameKitCoreComponent baseComponent = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameKitComponentCenter.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            // if (baseComponent.EditorResourceMode)
            // {
            //     m_LocalizationManager.SetResourceManager(baseComponent.EditorResourceHelper);
            // }
            // else
            // {
            //     m_LocalizationManager.SetResourceManager(GameKitEntry.GetModule<IResourceManager>());
            // }

            LocalizationHelperBase localizationHelper = Helper.CreateHelper(m_LocalizationHelperTypeName, m_CustomLocalizationHelper);
            if (localizationHelper == null)
            {
                Log.Error("Can not create localization helper.");
                return;
            }

            localizationHelper.name = "Localization Helper";
            Transform transform = localizationHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_LocalizationManager.SetDataProviderHelper(localizationHelper);
            m_LocalizationManager.SetLocalizationHelper(localizationHelper);
            m_LocalizationManager.Language = m_LocalizationManager.SystemLanguage;
            // m_LocalizationManager.Language = baseComponent.EditorResourceMode && baseComponent.EditorLanguage != Language.Unspecified ? baseComponent.EditorLanguage : m_LocalizationManager.SystemLanguage;
            if (m_CachedBytesSize > 0)
            {
                EnsureCachedBytesSize(m_CachedBytesSize);
            }
        }

        public void EnsureCachedBytesSize(int ensureSize)
        {
            m_LocalizationManager.EnsureCachedBytesSize(ensureSize);
        }

        public void FreeCachedBytes()
        {
            m_LocalizationManager.FreeCachedBytes();
        }

        public void ReadData(string dictionaryAssetName)
        {
            m_LocalizationManager.ReadData(dictionaryAssetName);
        }

        public void ReadData(string dictionaryAssetName, int priority)
        {
            m_LocalizationManager.ReadData(dictionaryAssetName, priority);
        }

        public void ReadData(string dictionaryAssetName, object userData)
        {
            m_LocalizationManager.ReadData(dictionaryAssetName, userData);
        }

        public void ReadData(string dictionaryAssetName, int priority, object userData)
        {
            m_LocalizationManager.ReadData(dictionaryAssetName, priority, userData);
        }

        public bool ParseData(string dictionaryString)
        {
            return m_LocalizationManager.ParseData(dictionaryString);
        }

        public bool ParseData(string dictionaryString, object userData)
        {
            return m_LocalizationManager.ParseData(dictionaryString, userData);
        }

        public bool ParseData(byte[] dictionaryBytes)
        {
            return m_LocalizationManager.ParseData(dictionaryBytes);
        }

        public bool ParseData(byte[] dictionaryBytes, object userData)
        {
            return m_LocalizationManager.ParseData(dictionaryBytes, userData);
        }

        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length)
        {
            return m_LocalizationManager.ParseData(dictionaryBytes, startIndex, length);
        }

        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length, object userData)
        {
            return m_LocalizationManager.ParseData(dictionaryBytes, startIndex, length, userData);
        }

        public string GetString(string key)
        {
            return m_LocalizationManager.GetString(key);
        }

        public string GetString<T>(string key, T arg)
        {
            return m_LocalizationManager.GetString(key, arg);
        }

        public string GetString<T1, T2>(string key, T1 arg1, T2 arg2)
        {
            return m_LocalizationManager.GetString(key, arg1, arg2);
        }

        public string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
        {
            return m_LocalizationManager.GetString(key, arg1, arg2, arg3);
        }

        public string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return m_LocalizationManager.GetString(key, arg1, arg2, arg3, arg4);
        }

        public string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return m_LocalizationManager.GetString(key, arg1, arg2, arg3, arg4, arg5);
        }

        public bool HasRawString(string key)
        {
            return m_LocalizationManager.HasRawString(key);
        }

        public string GetRawString(string key)
        {
            return m_LocalizationManager.GetRawString(key);
        }

        public bool RemoveRawString(string key)
        {
            return m_LocalizationManager.RemoveRawString(key);
        }

        public void RemoveAllRawStrings()
        {
            m_LocalizationManager.RemoveAllRawStrings();
        }

        private void OnReadDataSuccess(object sender, ReadDataSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, LoadDictionarySuccessEventArgs.Create(e));
        }

        private void OnReadDataFailure(object sender, ReadDataFailureEventArgs e)
        {
            Log.Warning("Load dictionary failure, asset name '{0}', error message '{1}'.", e.DataAssetName, e.ErrorMessage);
            m_EventComponent.Fire(this, LoadDictionaryFailureEventArgs.Create(e));
        }
    }
}
