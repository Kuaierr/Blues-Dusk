// using GameKit.Resource;

namespace GameKit.Localization
{
    public interface ILocalizationManager : IDataProvider<ILocalizationManager>
    {
        Language Language
        {
            get;
            set;
        }

        Language SystemLanguage
        {
            get;
        }

        int DictionaryCount
        {
            get;
        }

        int CachedBytesSize
        {
            get;
        }

        // void SetResourceManager(IResourceManager resourceManager);

        void SetDataProviderHelper(IDataProviderHelper<ILocalizationManager> dataProviderHelper);

        void SetLocalizationHelper(ILocalizationHelper localizationHelper);

        void EnsureCachedBytesSize(int ensureSize);

        void FreeCachedBytes();

        string GetString(string key);

        string GetString<T>(string key, T arg);

        string GetString<T1, T2>(string key, T1 arg1, T2 arg2);

        string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3);

        string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        bool HasRawString(string key);

        string GetRawString(string key);

        bool AddRawString(string key, string value);

        bool RemoveRawString(string key);

        void RemoveAllRawStrings();
    }
}
