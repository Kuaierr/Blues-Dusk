using LubanConfig.DataTable;
using InteractCallType = LubanConfig.ItemEnum.InteractCallbackType;
using GameKit;
using UnityGameKit.Runtime;
using GameKit.Inventory;

public sealed class DefaultInventoryHelper : InventoryHelperBase
{
    public override IStock InitStock(IStock stock, object data)
    {
        if (data.GetType() == typeof(Item))
        {
            Item itemData = (Item)data;
            stock.OnHelperInit(1, itemData.MaxOverlap);
            for (int i = 0; i < itemData.InteractCallback.Count; i++)
            {
                SetItemCallback(stock, itemData.InteractCallback[i].CallbackType, itemData.InteractCallback[i].CallbackArgs);
            }
            return stock;
        }
        return stock;
    }

    private void SetItemCallback(IStock stock, InteractCallType callbackType, string callbackArg)
    {
        switch (callbackType)
        {
            case InteractCallType.DIALOG:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            case InteractCallType.BUBBLE:
                {
                    stock.SetInteractCallback(ShowBubble, callbackArg);
                    break;
                }
            case InteractCallType.CLOSEUP:
                {
                    stock.SetInteractCallback(ShowCloseUp, callbackArg);
                    break;
                }
            case InteractCallType.GETCARDS:
                {
                    stock.SetInteractCallback(GetCards, callbackArg);
                    break;
                }
            case InteractCallType.TIMELINE:
                {
                    stock.SetInteractCallback(ShowTimeline, callbackArg);
                    break;
                }
            case InteractCallType.TIPS:
                {
                    stock.SetInteractCallback(ShowTip, callbackArg);
                    break;
                }
            default:
                {
                    stock.SetInteractCallback(ShowNone, callbackArg);
                    break;
                }
        }
    }

    private void ShowDialog(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Dialog. Param: {0}", callbackArg);
    }

    private void ShowTip(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Tip. Param: {0}", callbackArg);
    }

    private void ShowCloseUp(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show CloseUp. Param: {0}", callbackArg);
    }

    private void ShowBubble(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Bubble. Param: {0}", callbackArg);
    }

    private void ShowTimeline(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Timeline. Param: {0}", callbackArg);
    }

    private void GetCards(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Get Cards. Param: {0}", callbackArg);
    }

    private void ShowNone(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: None");
    }

}

