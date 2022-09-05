using UnityEngine;
using UnityGameKit.Runtime;
using GameKit.Element;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Collect Object")]
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

    public override void OnInit()
    {
        base.OnInit();
        m_configData = GameKitCenter.Data.ItemTable.Get(m_DataId);
        if (m_configData == null)
        {
            Log.Fail("Incorrect Data Id for {0}", gameObject.name);
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
            // Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}