using LubanConfig.DataTable;
using InteractCallType = LubanConfig.ItemEnum.InteractCallbackType;
using GameKit;
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
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            case InteractCallType.CLOSEUP:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            case InteractCallType.GETCARDS:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            case InteractCallType.TIMELINE:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            case InteractCallType.TIPS:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
            default:
                {
                    stock.SetInteractCallback(ShowDialog, callbackArg);
                    break;
                }
        }
    }

    private void ShowDialog(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Dialog");
    }

    private void ShowTip(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Tip");
    }

    private void ShowCloseUp(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show CloseUp");
    }

    private void ShowBubble(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Bubble");
    }

    private void ShowTimeline(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Show Timeline");
    }

    private void GetCards(string callbackArg)
    {
        Utility.Debugger.LogSuccess("Interact Callback: Get Cards");
    }

}

