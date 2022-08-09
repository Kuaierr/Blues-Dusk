using UnityEngine;
using GameKit;
using LubanConfig.DataTable;
public class BackpackSystem : MonoSingletonBase<BackpackSystem>
{
    private string PlayerBackpackName = "Player's Backpack";
    private InventoryComponent inventory;
    private UI_BackpackSystem uI_Backpack;
    private IInventory playerBackpack;
    private void Start()
    {
        inventory = GameKitComponentCenter.GetComponent<InventoryComponent>();
        uI_Backpack = UIManager.instance.GetUI<UI_BackpackSystem>("UI_BackpackSystem");
        playerBackpack = inventory.GetOrCreateInventory(PlayerBackpackName, 60);
        uI_Backpack.uI_Backpack.SetInventory(playerBackpack);
    }

    public bool AddToBackpack(Item data)
    {
        bool result = inventory.AddToInventory<Item>(PlayerBackpackName, data.Id, data.Name, data);
        if (result)
            EventManager.instance.EventTrigger<CollectItemSuccessEventArgs>(CollectItemSuccessEventArgs.EventId, CollectItemSuccessEventArgs.Create(data));
        else
            EventManager.instance.EventTrigger<CollectItemFailEventArgs>(CollectItemFailEventArgs.EventId, CollectItemFailEventArgs.Create(data));
        return result;
    }

    public IStock GetStockFromInventory(string stockName)
    {
        return inventory.GetStockFromInventory(PlayerBackpackName, stockName);
    }

    public Item GetFromInventory(string stockName)
    {
        return inventory.GetFromInventory<Item>(PlayerBackpackName, stockName);
    }

    public IStock GetStockFromInventory(int id, string stockName)
    {
        return inventory.GetStockFromInventory(PlayerBackpackName, id, stockName);
    }

    public Item GetFromInventory(int id, string stockName)
    {
        return inventory.GetFromInventory<Item>(PlayerBackpackName, id, stockName);
    }

    public IStock GetStockFromInventory(int index)
    {
        return inventory.GetStockFromInventory(PlayerBackpackName, index);
    }

    public Item GetFromInventory(int index)
    {
        return inventory.GetFromInventory<Item>(PlayerBackpackName, index);
    }
}