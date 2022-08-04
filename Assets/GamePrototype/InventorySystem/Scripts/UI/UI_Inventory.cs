using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using UnityEngine.Events;

public class UI_Inventory : UIGroup
{
    private IInventory inventory;
    InventorySystem inventorySystem;
    [SerializeField] private RectTransform content;
    [SerializeField] private UI_InventoryChunk chunkPrototype;
    private List<UI_InventoryChunk> chunks;
    protected override void OnStart()
    {
        base.OnStart();
        chunks = new List<UI_InventoryChunk>();
        inventorySystem = GameKitComponentCenter.GetComponent<InventorySystem>();

    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
        if (inventory != null)
        {
            if (inventory.Size < chunks.Count)
            {
                for (int i = inventory.Size; i < chunks.Count; i++)
                {
                    Destroy(chunks[i].gameObject);
                }
                chunks.RemoveRange(inventory.Size - 1, chunks.Count - 1);
            }
            else if (inventory.Size > chunks.Count)
            {
                int currentCount = chunks.Count;
                for (int i = currentCount; i < inventory.Size; i++)
                {
                    UI_InventoryChunk newChunk = GameObject.Instantiate(chunkPrototype, Vector3.zero, Quaternion.identity, content.transform);
                    newChunk.gameObject.SetActive(true);
                    chunks.Add(newChunk);
                }
            }
            Refresh();
        }
    }

    private void Refresh()
    {
        Debug.Log($"Refresh");
        IStock[] stocksMap = inventory.StockMap;

        if (chunks.Count != stocksMap.Length)
        {
            Utility.Debugger.LogFail("Unconsistence bettwen chunk ui and chunks.");
            return;
        }

        for (int i = 0; i < stocksMap.Length; i++)
        {
            if (stocksMap[i] != null)
                chunks[i].SetData(stocksMap[i].Data);
        }
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory = inventorySystem.GetInventory("TempInventory");
            this.Show();
        }
    }
}