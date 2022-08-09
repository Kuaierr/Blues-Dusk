using UnityEngine;
using GameKit;
using LubanConfig.DataTable;

public class CollectItemSuccessEventArgs : GameKitEventArgs
{
    private const string m_id = "PLAYER_COLLECT_ITEM";
    public static string EventId = m_id;
    public CollectItemSuccessEventArgs()
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