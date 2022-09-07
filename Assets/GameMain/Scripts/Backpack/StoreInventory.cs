using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using GameKit.QuickCode;
using GameKit.Inventory;

public class StoreInventory : MonoSingletonBase<StoreInventory>
{
    private const string StoreInventoryName = "Store's Inventory";
    private IInventory m_StoreInventory;
    private int m_CachedUiId;
    private void Start()
    {
        m_StoreInventory = GameKitCenter.Inventory.GetOrCreateInventory(StoreInventoryName, 60);
        GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnInventoryUIOpenSuccess);
        GameKitCenter.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnInventoryUIOpenFailure);
        GameKitCenter.Event.Subscribe(DoPurchaseEventArgs.EventId, OnDoPurchase);
    }

    public void TryOpenStoreUI()
    {
        var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_Backpack", userData: this);
        if (uiForm != null)
            m_CachedUiId = (int)uiForm;
    }

    private void OnInventoryUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
        if (args.UserData == null || (StoreInventory)args.UserData != this)
        {
            Log.Warning("Not Load Inventory UI");
            return;
        }

        if (args.UIForm == null)
        {
            Log.Fail("Loaded Inventory UI Form  is null");
            return;
        }
        UI_BackpackSystem uI_StoreInventory = (UI_BackpackSystem)args.UIForm.Logic;
        uI_StoreInventory.SetInventory(m_StoreInventory, UI_BackpackType.Store);
    }

    private void OnInventoryUIOpenFailure(object sender, GameKit.Event.GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
        if (args.UserData == null || (StoreInventory)args.UserData != this)
        {
            Log.Warning("Not Load Inventory UI");
            return;
        }

        if (args.UIForm == null)
        {
            Log.Fail("Loaded Inventory UI Form  is null");
            return;
        }
    }

    private void OnDoPurchase(object sender, GameKit.Event.GameEventArgs e)
    {
        Log.Success("UI_BackpackInfo do purchase");
        DoPurchaseEventArgs eventArgs = (DoPurchaseEventArgs)e;
        if (eventArgs == null)
        {
            Log.Warning("UI_BackpackInfo DoPurchaseEventArgs is null");
            return;
        }

        if (eventArgs.UserData.GetType() != typeof(UI_BackpackInfo))
        {
            Log.Warning("DoPurchase is not called by UI_BackpackInfo");
            return;
        }

        if (!this.RemoveFromInventory((Item)eventArgs.Stock.Data))
        {
            Log.Fatal("Purchase Add To PlayerBackpack Fail.");

        }

        if (!PlayerBackpack.current.AddToBackpack((Item)eventArgs.Stock.Data))
        {
            Log.Fail("Purchase Add To PlayerBackpack Fail.");
            GameKitCenter.Event.Fire(PurchaseFailureEventArgs.EventId, PurchaseFailureEventArgs.Create(eventArgs.Stock, this));
            return;
        }

        GameKitCenter.Event.Fire(PurchaseSuccessEventArgs.EventId, PurchaseSuccessEventArgs.Create(eventArgs.Stock, this));
    }

    public bool AddToInventory(Item data)
    {
        return GameKitCenter.Inventory.AddToInventory<Item>(StoreInventoryName, data.Id, data.Name, data);
    }

    public bool RemoveFromInventory(Item data)
    {
        return GameKitCenter.Inventory.RemoveFromInventory<Item>(StoreInventoryName, data.Id, data.Name, data);
    }

    public IStock GetStockFromInventory(string stockName)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(StoreInventoryName, stockName);
    }

    public Item GetFromInventory(string stockName)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(StoreInventoryName, stockName);
    }

    public IStock GetStockFromInventory(int id, string stockName)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(StoreInventoryName, id, stockName);
    }

    public Item GetFromInventory(int id, string stockName)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(StoreInventoryName, id, stockName);
    }

    public IStock GetStockFromInventory(int index)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(StoreInventoryName, index);
    }

    public Item GetFromInventory(int index)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(StoreInventoryName, index);
    }
}