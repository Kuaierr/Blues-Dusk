using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Inventory
{
    public sealed class InventoryChunk
    {
        private int m_index;
        private IStock m_stock;
        public int Index
        {
            get
            {
                return m_index;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return m_stock == null;
            }
        }

        public InventoryChunk(int index)
        {
            index = m_index;
        }
        public bool HasFull(IStock stock)
        {
            if (m_stock == null)
                return false;
            if (!stock.Equals(m_stock))
                return true;
            if (m_stock.MaxOverlap > 1)
                return m_stock.Overlap + stock.Overlap >= m_stock.MaxOverlap;
            return false;
        }

        public IStock GetStock()
        {
            return m_stock;
        }
        public void SetStock(IStock stock)
        {
            m_stock = stock;
        }

        public bool ModifyStock<T>(InventoryCallback<T> callback) where T : class
        {
            if (m_stock == null)
                return false;
            callback?.Invoke(m_stock as T);
            return true;
        }

        public void Clear()
        {
            m_stock = null;
        }
    }
}
