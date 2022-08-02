using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void OnInit(string name, object data)
    {

    }

    public void OnChunkSlot(int index)
    {
        m_index = index;
    }
    public void AddOverlap(int count = 1)
    {

    }
    public void OnInteract()
    {

    }

}