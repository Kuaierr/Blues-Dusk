using UnityEngine;
using UnityGameKit.Runtime;
using GameKit.Element;
using LubanConfig.DataTable;

public class CollectElement : GameElementBase, ICollective
{
    public bool CanCollect = true;
    private Item m_configData;
    public Item Data
    {
        get
        {
            return m_configData;
        }
    }

    protected override void Start()
    {
        base.Start();
        m_configData = DataTable.instance.ItemTable.Get(m_DataId);
        if (m_configData == null)
        {
            Log.Warning("Incorrect Data Id for {0}", gameObject.name);
            CanCollect = false;
            return;
        }
        gameObject.name = m_configData.Name;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        OnCollect();
    }

    public void OnCollect()
    {
        if (CanCollect)
        {
            BackpackSystem.current.AddToBackpack(m_configData);
            Destroy(this.gameObject);
        }
    }
}