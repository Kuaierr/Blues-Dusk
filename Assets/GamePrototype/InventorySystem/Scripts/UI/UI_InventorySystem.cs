using UnityEngine;
using GameKit;

public class UI_InventorySystem : UIGroup
{
    public UI_Inventory uI_Inventory;
    public UI_StockInfo uI_StockInfo;

    protected override void OnStart()
    {
        base.OnStart();
        uI_Inventory = GetComponentInChildren<UI_Inventory>();
        uI_StockInfo = GetComponentInChildren<UI_StockInfo>();
    }
}