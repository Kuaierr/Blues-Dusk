using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using GameKit.DataStructure;

public class InventoryManager : SingletonBase<InventoryManager>
{
    private Dictionary<string, IInventory> inventories;
    private IInventory currentCachedInventory;
    public InventoryManager()
    {
        inventories = new Dictionary<string, IInventory>();
        currentCachedInventory = null;
    }

    public T GetFromInventory<T>(string name, string stockName) where T : class
    {
        if (HasInventory(name))
        {
            return inventories[name].GetStock(stockName) as T;
        }
        return null;
    }

    public bool AddToInventory<T>(int id, string name, T data) where T : class
    {
        if (HasInventory(name))
        {
            IStock stock = inventories[name].CreateStock<T>(id, name, data);
            inventories[name].AddStock(stock);
            return true;
        }
        return false;
    }
    public IInventory GetInventory(string name)
    {
        if (HasInventory(name))
            return inventories[name];
        Utility.Debugger.LogFail("Can not get inventory, no inventory named {0}.", name);
        return null;
    }

    public bool CreateInventory(string name, int size, IInventoryHelper helper)
    {
        if (HasInventory(name))
        {
            Utility.Debugger.LogFail("Can not create inventory, inventory named {0} has exist.", name);
            return false;
        }
        Inventory inventory = new Inventory(name, size, Inventory.GetInventorySerialId());
        inventory.SetHelper(helper);
        inventories.Add(name, inventory);

        return true;
    }

    public bool RemoveInventory(string name)
    {
        if (!HasInventory(name))
        {
            Utility.Debugger.LogFail("Can not remove inventory, no inventory named {0}", name);
            return false;
        }
        inventories.Remove(name);
        return true;
    }

    public bool HasInventory(string name)
    {
        if (!inventories.ContainsKey(name))
            return false;
        return true;
    }
}
