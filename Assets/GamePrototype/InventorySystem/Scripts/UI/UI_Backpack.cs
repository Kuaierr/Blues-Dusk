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
    protected override void OnStart()
    {
        base.OnStart();
        chunks = new List<UI_BackpackChunk>();
        backpackSystem = BackpackSystem.current;
    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
        Debug.Log($"inventory is " + inventory);
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

    public void SetInventory(IInventory newInventory)
    {
        inventory = newInventory;
    }
}