using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using UnityEngine.Events;

public class UI_Backpack : UIGroup
{
    private IInventory inventory;
    BackpackSystem backpackSystem;
    [SerializeField] private RectTransform content;
    [SerializeField] private UI_BackpackChunk chunkPrototype;
    private List<UI_BackpackChunk> chunks;
    private IStock m_CachedCurrentStock;
    private UI_BackpackInfo uI_StockInfo;
    protected override void OnStart()
    {
        base.OnStart();
        chunks = new List<UI_BackpackChunk>();
        backpackSystem = BackpackSystem.current;
        EventManager.instance.AddEventListener<CollectItemSuccessEventArgs>(CollectItemSuccessEventArgs.EventId, OnCollectItemSuccess);
    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
        UpdateChunks();
    }

    private void Refresh()
    {
        // Debug.Log($"Refresh");
        IStock[] stocksMap = inventory.StockMap;

        if (chunks.Count != stocksMap.Length)
        {
            Utility.Debugger.LogFail("Unconsistence bettwen chunk ui and chunks.");
            return;
        }

        for (int i = 0; i < stocksMap.Length; i++)
        {
            if (stocksMap[i] != null)
            {
                chunks[i].SetIndex(i);
                chunks[i].SetData(stocksMap[i]);
            }
        }
    }

    public void UpdateChunks()
    {
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
                    UI_BackpackChunk newChunk = GameObject.Instantiate(chunkPrototype, Vector3.zero, Quaternion.identity, content.transform);
                    newChunk.SetStockInfoUI(uI_StockInfo);
                    newChunk.gameObject.SetActive(true);
                    chunks.Add(newChunk);
                }
            }
            Refresh();
        }
    }

    public void SetInventory(IInventory newInventory)
    {
        inventory = newInventory;
        UpdateChunks();
    }

    public void SetStockInfoUI(UI_BackpackInfo StockInfo)
    {
        uI_StockInfo = StockInfo;
    }

    private void OnCollectItemSuccess(CollectItemSuccessEventArgs CollectItemSuccessEventArgs)
    {
        Refresh();
    }
}