using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using GameKit.QuickCode;
using GameKit.Inventory;
using GameKit.UI;

public class BackpackSystem : MonoSingletonBase<BackpackSystem>
{
    private const string PlayerBackpackName = "Player's Backpack";
    private UI_BackpackSystem uI_Backpack;
    private IInventory playerBackpack;
    private void Start()
    {
        playerBackpack = GameKitCenter.Inventory.GetOrCreateInventory(PlayerBackpackName, 60);
    }

    private void Update()
    {
        if (InputManager.instance.GetWorldKeyDown(KeyCode.I))
        {
            GameKitCenter.UI.TryOpenUIForm("UI_Backpack", userData: InitUIInfo.Create(KeyCode.I, playerBackpack));
        }
    }

    public bool AddToBackpack(Item data)
    {
        bool result = GameKitCenter.Inventory.AddToInventory<Item>(PlayerBackpackName, data.Id, data.Name, data);
        if (result)
            EventManager.instance.EventTrigger<CollectItemSuccessEventArgs>(CollectItemSuccessEventArgs.EventId, CollectItemSuccessEventArgs.Create(data));
        else
            EventManager.instance.EventTrigger<CollectItemFailEventArgs>(CollectItemFailEventArgs.EventId, CollectItemFailEventArgs.Create(data));
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