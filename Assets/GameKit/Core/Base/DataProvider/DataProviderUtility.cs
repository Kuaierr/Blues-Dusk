namespace GameKit
{
    /// 数据提供者效用
    public static class DataProviderUtility
    {

        /// 获取缓冲二进制流的大小。
        public static int GetCachedBytesSize<T>()
        {
            return DataProvider<T>.CachedBytesSize;
        }


        /// 确保二进制流缓存分配足够大小的内存并缓存。
        public static void EnsureCachedBytesSize<T>(int ensureSize)
        {
            DataProvider<T>.EnsureCachedBytesSize(ensureSize);
        }

        /// 释放缓存的二进制流
        public static void FreeCachedBytes<T>()
        {
            DataProvider<T>.FreeCachedBytes();
        }


        /// 创建数据提供者
        public static IDataProvider<T> Create<T>(T owner, IDataProviderHelper<T> dataProviderHelper)
        {
            if (owner == null)
            {
                throw new GameKitException("Owner is invalid.");
            }

            // if (resourceManager == null)
            // {
            //     throw new GameKitException("Resource manager is invalid.");
            // }

            if (dataProviderHelper == null)
            {
                throw new GameKitException("Data provider helper is invalid.");
            }

            DataProvider<T> dataProvider = new DataProvider<T>(owner);
            // dataProvider.SetResourceManager(resourceManager);
            dataProvider.SetDataProviderHelper(dataProviderHelper);
            return dataProvider;
        }
    }
}
