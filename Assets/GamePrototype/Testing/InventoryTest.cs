using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameKit;
using SimpleJSON;
using LubanConfig.DataTable;
using LubanConfig;


public class InventoryTest : MonoBehaviour
{
    InventorySystem inventorySystem;
    IEnumerator Start()
    {
        yield return null;
        inventorySystem = GameKitComponentCenter.GetComponent<InventorySystem>();
        Tables tables = new LubanConfig.Tables(Load);
        Item item = tables.TbItem.Get(10001);
        // UnityEngine.Color color = ExternalTypeUtility.NewFromCfgColor item.InteractCallback;

        inventorySystem.CreateInventory("TempInventory", 60);
        inventorySystem.AddToInventory<Item>("TempInventory", 10001, item.Name, item);

        Item data = inventorySystem.GetFromInventory<Item>("TempInventory", item.Name);
        Debug.Log(data);
        Utility.Debugger.LogSuccess(data.ToString());


        
    }


    private static JSONNode Load(string fileName)
    {
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/LubanGen/Datas/json/" + fileName + ".json"));
    }
}
