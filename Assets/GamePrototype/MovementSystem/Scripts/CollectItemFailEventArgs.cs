using UnityEngine;
using GameKit;
using LubanConfig.DataTable;

public class CollectItemFailEventArgs : GameKitEventArgs
{
    private const string m_id = "PLAYER_COLLECT_ITEM";
    public static readonly string EventId = typeof(CollectItemFailEventArgs).GetHashCode().ToString();
    public CollectItemFailEventArgs()
    {
        Data = null;
    }

    public override string Id
    {
        get
        {
            return m_id;
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