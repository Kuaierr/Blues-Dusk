using System.Collections.Generic;

namespace GameKit.Inventory
{
    [System.Serializable]
    public partial class Inventory : IInventory
    {
        private string m_name;
        private int m_size;
        private int m_serialId;
        private IStock m_cachedStock;
        private int m_cachedStockIndex;
        private int m_cachedChunkIndex;
        private IInventoryHelper m_helper;
        private readonly List<IStock> m_stocks;
        private readonly List<IInventoryChunk> m_cachedChunks;
        private static int s_currentSerialId = 0;

        public int Id
        {
            get
            {
                return m_serialId;
            }
        }
        public int Size
        {
            get
            {
                return m_size;
            }
        }
        public int Count
        {
            get
            {
                return m_stocks.Count;
            }
        }
        public int OverlapCount
        {
            get
            {
                return m_cachedChunks.Count;
            }
        }
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        public IStock[] StockMap
        {
            get
            {
                List<IStock> tmpStocks = new List<IStock>();
                for (int i = 0; i < m_cachedChunks.Count; i++)
                {
                    tmpStocks.Add(m_cachedChunks[i].GetStock());
                }
                return tmpStocks.ToArray();
            }
        }

        public Inventory(string name, int size, int serialId)
        {
            m_name = name;
            m_size = size;
            m_serialId = serialId;
            m_stocks = new List<IStock>();
            m_cachedChunks = new List<IInventoryChunk>();
            for (int i = 0; i < size; i++)
            {
                m_cachedChunks.Add(new InventoryChunk(i));
            }

            m_cachedStock = null;
            m_cachedStockIndex = -1;
            m_cachedChunkIndex = -1;
        }

        public IStock CreateStock<T>(int id, string name, T data = default(T)) where T : class
        {
            IStock newStock = new Stock(id, Stock.GetStockSerialId(), name, data);
            if (m_helper == null)
            {
                Utility.Debugger.LogError("Inventory Helper of {0} is null.", Name);
                return null;
            }
            m_helper.InitStock(newStock, data);
            return newStock;
        }

        public bool HasFull(IStock stock, bool cache = true)
        {
            for (int index = 0; index < m_size; index++)
            {
                if (!m_cachedChunks[index].HasFull(stock))
                {
                    if (cache)
                        m_cachedChunkIndex = index;
                    return false;
                }
            }
            if (cache)
                m_cachedChunkIndex = -1;
            return true;
        }

        public bool HasStock(string name, bool useCache = true)
        {
            if (useCache)
            {
                if (m_cachedStock != null && m_cachedStockIndex != -1)
                    return true;
            }

            for (int i = 0; i < m_stocks.Count; i++)
            {
                if (m_stocks[i].Name == name)
                {
                    if (useCache)
                    {
                        m_cachedStock = m_stocks[i];
                        m_cachedStockIndex = i;
                    }
                    return true;
                }
            }

            if (useCache)
            {
                m_cachedStock = null;
                m_cachedStockIndex = -1;
            }
            return false;
        }

        public bool HasStock(int id, string name, bool useCache = true)
        {
            if (useCache)
            {
                if (m_cachedStock != null && m_cachedStockIndex != -1)
                    return true;
            }

            for (int i = 0; i < m_stocks.Count; i++)
            {
                if (m_stocks[i].Id == id || m_stocks[i].Name == name)
                {
                    if (useCache)
                    {
                        m_cachedStock = m_stocks[i];
                        m_cachedStockIndex = i;
                    }
                    return true;
                }
            }

            if (useCache)
            {
                m_cachedStock = null;
                m_cachedStockIndex = -1;
            }
            return false;
        }

        public bool AddStock(IStock stock, int count)
        {
            bool isStockExist = CachesHasStock(stock, useCache: true);
            bool isFull = HasFull(stock, true);

            if (isFull)
            {
                Utility.Debugger.LogWarning("The chunks of inventory {0} has been filled.", this.Name);
                return false;
            }

            if (stock.MaxOverlap < 1)
            {
                Utility.Debugger.LogError("The Max overlap settings for {0} is incorrect", stock.Name);
                return false;
            }

            // 无论库存物是否存在，都将添加stock的人物交给下层Chunks
            AddStockToChunk(stock, useCache: true);

            // 如果库存不存在，或者这个物最大堆叠为1，则添加到Stocks
            if (!isStockExist || stock.MaxOverlap == 1)
                m_stocks.Add(stock);

            return true;
        }

        public bool AddStock(IStock stock)
        {
            bool isStockExist = CachesHasStock(stock, useCache: true);
            bool isFull = HasFull(stock, true);

            if (isFull)
            {
                Utility.Debugger.LogWarning("The chunks of inventory {0} has been filled.", this.Name);
                return false;
            }

            if (stock.MaxOverlap < 1)
            {
                Utility.Debugger.LogError("The Max overlap settings for {0} is incorrect", stock.Name);
                return false;
            }

            // 无论库存物是否存在，都将添加stock的人物交给下层Chunks
            AddStockToChunk(stock, useCache: true);

            // 如果库存不存在，或者这个物最大堆叠为1，则添加到Stocks
            if (!isStockExist || stock.MaxOverlap == 1)
                m_stocks.Add(stock);

            return true;
        }

        public IStock GetStock(string name)
        {
            if (m_cachedStock != null || HasStock(name))
                return m_cachedStock;
            return null;
        }

        public IStock GetStock(int id, string name)
        {
            if (m_cachedStock != null || HasStock(id, name))
                return m_cachedStock;
            return null;
        }

        public bool RemoveStock(string name)
        {
            if (m_cachedStock != null || HasStock(name))
            {
                m_stocks.Remove(m_cachedStock);
                m_cachedChunks.RemoveAt(m_cachedStock.SlotIndex);
                return true;
            }
            return false;
        }

        public bool RemoveStock(int id, string name)
        {
            if (m_cachedStock != null || HasStock(id, name))
            {
                m_stocks.Remove(m_cachedStock);
                m_cachedChunks.RemoveAt(m_cachedStock.SlotIndex);
                return true;
            }
            return false;
        }

        public bool SetStock(string name, IStock stock)
        {
            if (m_cachedStock != null || HasStock(name))
            {
                m_stocks[m_cachedStockIndex] = stock;
                m_cachedChunks[m_cachedStock.SlotIndex].SetStock(stock);
                return true;
            }
            return false;
        }

        public bool SetStock(int id, string name, IStock stock)
        {
            if (m_cachedStock != null || HasStock(id, name))
            {
                m_stocks[m_cachedStockIndex] = stock;
                m_cachedChunks[m_cachedStock.SlotIndex].SetStock(stock);
                return true;
            }
            return false;
        }

        public bool ModifyStock<T>(string name, InventoryCallback<T> callback) where T : class
        {
            if (HasStock(name))
            {
                callback?.Invoke(m_cachedStock as T);
                m_cachedChunks[m_cachedStock.SlotIndex].SetStock(m_cachedStock);
                return true;
            }
            return false;
        }

        public bool ModifyStock<T>(int id, string name, InventoryCallback<T> callback) where T : class
        {
            if (HasStock(id, name))
            {
                callback?.Invoke(m_cachedStock as T);
                m_cachedChunks[m_cachedStock.SlotIndex].SetStock(m_cachedStock);
                return true;
            }
            return false;
        }

        public bool HasStock(int index, bool useCache = true)
        {
            if (m_cachedChunks[index].IsEmpty)
                return false;
            return CachesHasStock(m_cachedChunks[index].GetStock(), useCache);
        }

        public IStock GetStock(int index)
        {
            if (HasStock(index))
                return null;
            return m_cachedChunks[index].GetStock();
        }
        public bool RemoveStock(int index)
        {
            if (!HasStock(index))
                return false;

            m_stocks.Remove(m_cachedChunks[index].GetStock());
            m_cachedChunks[index].Clear();
            return true;
        }
        public bool SetStock(int index, IStock stock)
        {
            if (HasStock(index))
                return false;
            m_cachedChunks[index].SetStock(stock);
            return true;
        }

        public bool ModifyStock<T>(int index, InventoryCallback<T> callback) where T : class
        {
            if (!m_cachedChunks[index].IsEmpty)
            {
                callback?.Invoke(m_cachedChunks[index].GetStock() as T);
                return true;
            }
            return false;
        }

        public static int GetInventorySerialId()
        {
            return ++s_currentSerialId;
        }

        public void Clear()
        {
            m_stocks.Clear();
            foreach (var chunk in m_cachedChunks)
                chunk.Clear();
        }

        public void SetHelper(IInventoryHelper helper)
        {
            m_helper = helper;
        }



        #region Private
        private bool CachesHasStock(IStock stock, bool useCache)
        {
            bool result = m_stocks.Contains(stock);
            if (useCache)
            {
                m_cachedStock = result ? stock : null;
                m_cachedStockIndex = result ? m_stocks.IndexOf(stock) : -1;
            }
            return result;
        }

        private bool ChunksHasStock(IStock stock, bool useCache)
        {
            return m_stocks.Contains(stock);
        }

        private bool AddStockToChunk(IStock stock, bool useCache)
        {
            if (!CheckChunkCache(useCache) && HasFull(stock))
                return false;
            m_cachedChunks[m_cachedChunkIndex].SetStock(stock);
            stock.OnChunkSlot(m_cachedChunkIndex);
            return true;
        }

        private bool CheckChunkCache(bool useCache)
        {
            if (useCache)
                if (m_cachedChunkIndex != -1)
                    return true;
            return false;
        }

        private bool CheckStockCache(bool useCache)
        {
            if (useCache)
                if (m_cachedStock != null && m_cachedStockIndex != -1)
                    return true;
            return false;
        }
        #endregion
    }
}
