using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

public class InventorySystem : GameKitComponent
{
    private InventoryManager inventoryManager;
    [SerializeField]
    private string m_InventoryHelperTypeName = "GameKit.DefaultInventoryHelper";
    [SerializeField]
    private InventoryHelperBase m_CustomInventoryHelper = null;
    [SerializeField] private InventoryHelperBase helper;
    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        helper = Helper.CreateHelper(m_InventoryHelperTypeName, m_CustomInventoryHelper);
    }

    public T GetFromInventory<T>(string name, string stockName) where T : class
    {
        return inventoryManager.GetFromInventory<T>(name, stockName);
    }

    public bool AddToInventory<T>(int id, string name, T data) where T : class
    {
        return inventoryManager.AddToInventory<T>(id, name, data);
    }
    public IInventory GetInventory(string name)
    {
        return inventoryManager.GetInventory(name);
    }

    public bool CreateInventory(string name, int size)
    {
        return inventoryManager.CreateInventory(name, size, helper);
    }

    public bool RemoveInventory(string name)
    {
        return inventoryManager.RemoveInventory(name);
    }

    public bool HasInventory(string name)
    {
        return inventoryManager.HasInventory(name);
    }
}
