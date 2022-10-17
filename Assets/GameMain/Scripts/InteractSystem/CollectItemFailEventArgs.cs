using GameKit;
using LubanConfig.DataTable;
using GameKit.Event;

public class CollectItemFailEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(CollectItemFailEventArgs).GetHashCode();
    public CollectItemFailEventArgs()
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



    public static CollectItemFailEventArgs Create(Item itemData)
    {
        CollectItemFailEventArgs collectItemFailEventArgs = ReferencePool.Acquire<CollectItemFailEventArgs>();
        collectItemFailEventArgs.Data = itemData;
        return collectItemFailEventArgs;
    }

    public override void Clear()
    {
        Data = null;
    }

}