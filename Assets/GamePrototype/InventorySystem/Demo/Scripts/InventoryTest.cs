using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameKit;
using LubanConfig.DataTable;



public class InventoryTest : MonoBehaviour
{
    InventoryComponent inventorySystem;
    IEnumerator Start()
    {
        yield return null;
        inventorySystem = GameKitComponentCenter.GetComponent<InventoryComponent>();
        Item item = TableManager.instance.ItemTable.Get(10001);

        inventorySystem.CreateInventory("TempInventory", 60);
        inventorySystem.AddToInventory<Item>("TempInventory", 10001, item.Name, item);

        IStock stock = inventorySystem.GetStockFromInventory("TempInventory", item.Name);
        Item data = stock.Data as Item;
        stock.OnInteract();
        Debug.Log(data);
        Utility.Debugger.LogSuccess(data.ToString());        
    }
}
