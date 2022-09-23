using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using GameKit.QuickCode;
using GameKit.Inventory;

public class PlayerBackpack : MonoSingletonBase<PlayerBackpack>
{
    private const string PlayerBackpackName = "PlayerBackpack";
    private IInventory m_PlayerBackpack;
    private int m_CachedUiId;
    private void Start()
    {
        m_PlayerBackpack = GameKitCenter.Inventory.GetOrCreateInventory(PlayerBackpackName, 60);
        GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnBackpackUIOpenSuccess);
        GameKitCenter.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnBackpackUIOpenFailure);
        GameKitCenter.Event.Subscribe(OnOpenPlayerBackpackEventArgs.EnentId,OnOpenPlayerBackpack);
    }

    private void Update()
    {
        if (InputManager.instance.GetWorldKeyDown(KeyCode.M))
        {
            GameKitCenter.UI.CloseUIForm(m_CachedUiId, userData: this);
        }
    }

    private void OnOpenPlayerBackpack(object sender, GameKit.Event.GameEventArgs e)
    {
        if (GameKitCenter.UI.GetUIForm(m_CachedUiId))
        {
            //GameKitCenter.UI.TryGetUIForm("UI_Backpack").Resume();
            GameKitCenter.UI.GetUIForm(m_CachedUiId).OnResume();
        }
        
        var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_Backpack", userData: this);
        if (uiForm != null)
            m_CachedUiId = (int)uiForm;
    }

    private void OnBackpackUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
        if (args.UserData == null || args.UserData.GetType() != typeof(PlayerBackpack))
            return;

        if (args.UIForm == null)
        {
            Log.Fail("Loaded Backpack UI Form  is null");
            return;
        }
        UI_BackpackSystem uI_PlayerBackpack = (UI_BackpackSystem)args.UIForm.Logic;
        uI_PlayerBackpack.SetInventory(m_PlayerBackpack, UI_BackpackType.Backpack);
        uI_PlayerBackpack.SetChangeKey(KeyCode.Escape);
    }

    private void OnBackpackUIOpenFailure(object sender, GameKit.Event.GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
        if (args.UserData == null || (PlayerBackpack)args.UserData != this)
        {
            Log.Warning("Not Load Backpack UI");
            return;
        }

        if (args.UIForm == null)
        {
            Log.Fail("Loaded Backpack UI Form  is null");
            return;
        }
    }

    public bool AddToBackpack(Item data)
    {
        return GameKitCenter.Inventory.AddToInventory<Item>(PlayerBackpackName, data.Id, data.Name, data);
    }

    public bool CollectToBackpack(Item data)
    {
        bool result = GameKitCenter.Inventory.AddToInventory<Item>(PlayerBackpackName, data.Id, data.Name, data);
        if (result)
            GameKitCenter.Event.Fire(CollectItemSuccessEventArgs.EventId, CollectItemSuccessEventArgs.Create(data));
        else
            GameKitCenter.Event.Fire(CollectItemFailEventArgs.EventId, CollectItemFailEventArgs.Create(data));
        return result;
    }

    public IStock GetStockFromInventory(string stockName)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(PlayerBackpackName, stockName);
    }

    public Item GetFromInventory(string stockName)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(PlayerBackpackName, stockName);
    }

    public IStock GetStockFromInventory(int id, string stockName)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(PlayerBackpackName, id, stockName);
    }

    public Item GetFromInventory(int id, string stockName)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(PlayerBackpackName, id, stockName);
    }

    public IStock GetStockFromInventory(int index)
    {
        return GameKitCenter.Inventory.GetStockFromInventory(PlayerBackpackName, index);
    }

    public Item GetFromInventory(int index)
    {
        return GameKitCenter.Inventory.GetFromInventory<Item>(PlayerBackpackName, index);
    }
}