using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

public class InventorySystem : GameKitComponent
{
    [SerializeField] private string m_InventoryHelperTypeName = "GameKit.DefaultInventoryHelper";
    [SerializeField] private InventoryHelperBase m_CustomInventoryHelper = null;
    [SerializeField] private InventoryHelperBase helper;

    private void Start()
    {
        helper = Helper.CreateHelper(m_InventoryHelperTypeName, m_CustomInventoryHelper);
        helper.gameObject.transform.SetParent(this.gameObject.transform);
        helper.name = "InventoryHelper";
    }
    public IStock GetStockFromInventory(string inventoryName, string stockName)
    {
        return InventoryManager.instance.GetStockFromInventory(inventoryName, stockName);
    }

    public T GetFromInventory<T>(string inventoryName, string stockName) where T : class
    {
        return InventoryManager.instance.GetFromInventory<T>(inventoryName, stockName);
    }

    public bool AddToInventory<T>(string inventoryName, int id, string stockName, T data) where T : class
    {
        return InventoryManager.instance.AddToInventory<T>(inventoryName, id, stockName, data);
    }
    public IInventory GetInventory(string inventoryName)
    {
        return InventoryManager.instance.GetInventory(inventoryName);
    }

    public bool CreateInventory(string inventoryName, int size)
    {
        return InventoryManager.instance.CreateInventory(inventoryName, size, helper);
    }

    public bool RemoveInventory(string inventoryName)
    {
        return InventoryManager.instance.RemoveInventory(inventoryName);
    }

    public bool HasInventory(string inventoryName)
    {
        return InventoryManager.instance.HasInventory(inventoryName);
    }
}
