using System.Net.Mime;
using System;
using UnityEngine;

namespace GameKit
{
    internal sealed class DataProvider<T> : IDataProvider<T>
    {
        private const int DefaultPriority = 0;
        private const int BlockSize = 1024 * 4;
        private static byte[] s_CachedBytes = null;
        private readonly T m_Owner;
        // private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
        // private readonly LoadBinaryCallbacks m_LoadBinaryCallbacks;
        // private IResourceManager m_ResourceManager;
        private IDataProviderHelper<T> m_DataProviderHelper;
        private EventHandler<ReadDataSuccessEventArgs> m_ReadDataSuccessEventHandler;
        private EventHandler<ReadDataFailureEventArgs> m_ReadDataFailureEventHandler;

        public DataProvider(T owner)
        {
            m_Owner = owner;
            // m_LoadAssetCallbacks = new LoadAssetCallbacks(LoadAssetSuccessCallback, LoadAssetOrBinaryFailureCallback, LoadAssetUpdateCallback, LoadAssetDependencyAssetCallback);
            // m_LoadBinaryCallbacks = new LoadBinaryCallbacks(LoadBinarySuccessCallback, LoadAssetOrBinaryFailureCallback);
            // m_ResourceManager = null;
            m_DataProviderHelper = null;
            m_ReadDataSuccessEventHandler = null;
            m_ReadDataFailureEventHandler = null;
        }

        public static int CachedBytesSize
        {
            get
            {
                return s_CachedBytes != null ? s_CachedBytes.Length : 0;
            }
        }

        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_ReadDataSuccessEventHandler += value;
            }
            remove
            {
                m_ReadDataSuccessEventHandler -= value;
            }
        }

        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_ReadDataFailureEventHandler += value;
            }
            remove
            {
                m_ReadDataFailureEventHandler -= value;
            }
        }

        public static void EnsureCachedBytesSize(int ensureSize)
        {
            if (ensureSize < 0)
            {
                throw new GameKitException("Ensure size is invalid.");
            }

            if (s_CachedBytes == null || s_CachedBytes.Length < ensureSize)
            {
                FreeCachedBytes();
                int size = (ensureSize - 1 + BlockSize) / BlockSize * BlockSize;
                s_CachedBytes = new byte[size];
            }
        }

        public static void FreeCachedBytes()
        {
            s_CachedBytes = null;
        }

        public void ReadData(string dataAssetName)
        {
            ReadData(dataAssetName, DefaultPriority, null);
        }

        public void ReadData(string dataAssetName, int priority)
        {
            ReadData(dataAssetName, priority, null);
        }

        public void ReadData(string dataAssetName, object userData)
        {
            ReadData(dataAssetName, DefaultPriority, userData);
        }

        public void ReadData(string dataAssetName, int priority, object userData)
        {
            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            // HasAssetResult result = m_ResourceManager.HasAsset(dataAssetName);
            // EnsureCachedBytesSize(dataLength);
            // if (dataLength != m_ResourceManager.LoadBinaryFromFileSystem(dataAssetName, s_CachedBytes))
            // {
            //     throw new GameKitException(Utility.Text.Format("Load binary '{0}' from file system with internal error.", dataAssetName));
            // }

            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data provider helper first.");
            }

            try
            {
                AddressableManager.instance.GetBinaryAsyn(dataAssetName, onSuccess: (byte[] bytes) =>
                {
                    LoadBinarySuccessCallback(dataAssetName, bytes, 0, null);
                    s_CachedBytes = bytes;
                    if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, s_CachedBytes, userData))
                    {
                        throw new GameKitException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                    }

                    if (m_ReadDataSuccessEventHandler != null)
                    {
                        ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, 0f, userData);
                        m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                        ReferencePool.Release(loadDataSuccessEventArgs);
                    }
                },
                onFail: () =>
                {
                    LoadAssetOrBinaryFailureCallback(dataAssetName, "Load binary data fail", null);
                });

            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }
                throw;
            }
        }


        public void ReadExternalData(string dataAssetName, int priority, object userData)
        {
            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            // HasAssetResult result = m_ResourceManager.HasAsset(dataAssetName);
            // EnsureCachedBytesSize(dataLength);
            // if (dataLength != m_ResourceManager.LoadBinaryFromFileSystem(dataAssetName, s_CachedBytes))
            // {
            //     throw new GameKitException(Utility.Text.Format("Load binary '{0}' from file system with internal error.", dataAssetName));
            // }

            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data provider helper first.");
            }

            try
            {
                if (!m_DataProviderHelper.ReadExternalData(m_Owner, dataAssetName, userData))
                {
                    throw new GameKitException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                }

                if (m_ReadDataSuccessEventHandler != null)
                {
                    ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, 0f, userData);
                    m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                    ReferencePool.Release(loadDataSuccessEventArgs);
                }
            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }
                throw;
            }
        }


        public bool ParseData(string dataString)
        {
            return ParseData(dataString, null);
        }

        public bool ParseData(string dataString, object userData)
        {
            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data helper first.");
            }

            if (dataString == null)
            {
                throw new GameKitException("Data string is invalid.");
            }

            try
            {
                return m_DataProviderHelper.ParseData(m_Owner, dataString, userData);
            }
            catch (Exception exception)
            {
                if (exception is GameKitException)
                {
                    throw;
                }

                throw new GameKitException(Utility.Text.Format("Can not parse data string with exception '{0}'.", exception), exception);
            }
        }

        public bool ParseData(byte[] dataBytes)
        {
            if (dataBytes == null)
            {
                throw new GameKitException("Data bytes is invalid.");
            }

            return ParseData(dataBytes, 0, dataBytes.Length, null);
        }

        public bool ParseData(byte[] dataBytes, object userData)
        {
            if (dataBytes == null)
            {
                throw new GameKitException("Data bytes is invalid.");
            }

            return ParseData(dataBytes, 0, dataBytes.Length, userData);
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length)
        {
            return ParseData(dataBytes, startIndex, length, null);
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length, object userData)
        {
            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data helper first.");
            }

            if (dataBytes == null)
            {
                throw new GameKitException("Data bytes is invalid.");
            }

            if (startIndex < 0 || length < 0 || startIndex + length > dataBytes.Length)
            {
                throw new GameKitException("Start index or length is invalid.");
            }

            try
            {
                return m_DataProviderHelper.ParseData(m_Owner, dataBytes, startIndex, length, userData);
            }
            catch (Exception exception)
            {
                if (exception is GameKitException)
                {
                    throw;
                }

                throw new GameKitException(Utility.Text.Format("Can not parse data bytes with exception '{0}'.", exception), exception);
            }
        }

        // internal void SetResourceManager(IResourceManager resourceManager)
        // {
        //     if (resourceManager == null)
        //     {
        //         throw new GameKitException("Resource manager is invalid.");
        //     }

        //     m_ResourceManager = resourceManager;
        // }

        internal void SetDataProviderHelper(IDataProviderHelper<T> dataProviderHelper)
        {
            if (dataProviderHelper == null)
            {
                throw new GameKitException("Data provider helper is invalid.");
            }

            m_DataProviderHelper = dataProviderHelper;
        }

        private void LoadAssetSuccessCallback(string dataAssetName, object dataAsset, float duration, object userData)
        {
            try
            {
                if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, dataAsset, userData))
                {
                    throw new GameKitException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                }

                if (m_ReadDataSuccessEventHandler != null)
                {
                    ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
                    m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                    ReferencePool.Release(loadDataSuccessEventArgs);
                }
            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }
                throw;
            }
            finally
            {
                m_DataProviderHelper.ReleaseDataAsset(m_Owner, dataAsset);
            }
        }

        private void LoadAssetOrBinaryFailureCallback(string dataAssetName, string errorMessage, object userData)
        {
            string appendErrorMessage = Utility.Text.Format("Load data failure, data asset name '{0}', error message '{2}'.", dataAssetName, errorMessage);
            if (m_ReadDataFailureEventHandler != null)
            {
                ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, appendErrorMessage, userData);
                m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                ReferencePool.Release(loadDataFailureEventArgs);
                return;
            }
            throw new GameKitException(appendErrorMessage);
        }

        private void LoadBinarySuccessCallback(string dataAssetName, byte[] dataBytes, float duration, object userData)
        {
            try
            {
                if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, dataBytes, 0, dataBytes.Length, userData))
                {
                    throw new GameKitException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                }

                if (m_ReadDataSuccessEventHandler != null)
                {
                    ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
                    m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                    ReferencePool.Release(loadDataSuccessEventArgs);
                }
            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }

                throw;
            }
        }
    }
}
