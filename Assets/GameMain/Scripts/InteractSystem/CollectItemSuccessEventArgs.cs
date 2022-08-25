using GameKit;
using GameKit.Event;
using GameKit.Entity;
using LubanConfig.DataTable;

public class CollectItemSuccessEventArgs : GameEventArgs
{
    public static int EventId = typeof(CollectItemSuccessEventArgs).GetHashCode();
    public CollectItemSuccessEventArgs()
    {
        Data = null;
    }

    public override int Id
    {
        get
        {
            return EventId;
        }
    }

    public Item Data
    {
        get;
        private set;
    }



    public static CollectItemSuccessEventArgs Create(Item itemData)
    {
        CollectItemSuccessEventArgs collectItemSuccessEventArgs = ReferencePool.Acquire<CollectItemSuccessEventArgs>();
        collectItemSuccessEventArgs.Data = itemData;
        return collectItemSuccessEventArgs;
    }

    public override void Clear()
    {
        Data = null;
    }

}