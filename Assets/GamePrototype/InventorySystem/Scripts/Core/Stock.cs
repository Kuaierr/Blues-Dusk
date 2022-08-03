using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

[System.Serializable]
public class Stock : IStock
{
    // 约定：id==-1代表该Stock为EmptyStock
    private int m_id;
    private int m_serialId;
    private int m_index;
    private string m_name;
    private bool m_avalible;
    private object m_data;
    private IInventory m_inventory;
    private int m_overlap;
    private int m_maxOverlap;
    private static int s_currentSerialId = 0;
    public int Id
    {
        get
        {
            return m_id;
        }
    }

    public int SerialId
    {
        get
        {
            return m_serialId;
        }
    }
    public int SlotIndex
    {
        get
        {
            return m_index;
        }
    }
    public string Name
    {
        get
        {
            return m_name;
        }
    }
    public bool IsAvaliable
    {
        get
        {
            return m_avalible;
        }
    }

    public object Data
    {
        get
        {
            return m_data;
        }
    }
    public IInventory Inventory
    {
        get
        {
            return m_inventory;
        }
    }
    public int Overlap
    {
        get
        {
            return m_overlap;
        }
    }
    public int MaxOverlap
    {
        get
        {
            return m_maxOverlap;
        }
    }

    public Stock(int id, int serialId, string name, object data)
    {
        m_id = id;
        m_serialId = serialId;
        m_name = name;
        m_data = data;
        m_avalible = true;
    }

    public void OnChunkSlot(int index)
    {
        m_index = index;
    }

    public void OnHelperInit(int overlap, int maxOverlap)
    {
        m_overlap = overlap;
        m_maxOverlap = maxOverlap;
    }

    public void AddOverlap(int count = 1)
    {
        m_overlap += count;
    }
    public virtual void OnInteract()
    {
        Utility.Debugger.Log("Interact Stock {0}", Name);
    }

    public static int GetStockSerialId()
    {
        return ++s_currentSerialId;
    }

}