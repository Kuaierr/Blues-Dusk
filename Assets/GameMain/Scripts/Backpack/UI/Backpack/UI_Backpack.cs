using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using GameKit;
using GameKit.QuickCode;
using GameKit.Inventory;
using UnityGameKit.Runtime;

public class UI_Backpack : UIFormChildBase
{
    private IInventory inventory;
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private UI_BackpackChunk chunkPrototype;
    private IStock m_CachedCurrentStock;
    private UI_BackpackInfo uI_StockInfo;
    private List<UI_BackpackChunk> chunks = new List<UI_BackpackChunk>();
    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        GameKitCenter.Event.Subscribe(CollectItemSuccessEventArgs.EventId, OnCollectItemSuccess);
        GameKitCenter.Event.Subscribe(CollectItemFailEventArgs.EventId, OnCollectItemFail);
    }

    public override void OnShow(UnityAction callback = null)
    {
        base.OnShow(callback);
        UpdateChunks();
    }

    public override void OnHide(UnityAction callback = null)
    {
        base.OnHide(callback);
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

    private void UpdateChunks()
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
                    newChunk.OnInit(uI_StockInfo);
                    newChunk.gameObject.SetActive(true);
                    chunks.Add(newChunk);
                }
            }
            Refresh();
        }
    }

    public void SetInventory(IInventory newInventory, UI_BackpackType type)
    {
        inventory = newInventory;
        if (type == UI_BackpackType.Store)
            title.text = "商店";
        else if (type == UI_BackpackType.Backpack)
            title.text = "物品栏";
        UpdateChunks();
    }

    public void SetStockInfoUI(UI_BackpackInfo StockInfo)
    {
        uI_StockInfo = StockInfo;
    }

    private void OnCollectItemSuccess(object sender, GameKit.Event.GameEventArgs CollectItemSuccessEventArgs)
    {
        Refresh();
    }

    private void OnCollectItemFail(object sender, GameKit.Event.GameEventArgs CollectItemFailEventArgs)
    {
        Log.Fail("Collect Item Fail");
        Refresh();
    }
}