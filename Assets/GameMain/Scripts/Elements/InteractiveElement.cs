using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

public class InteractiveElement : ElementBase
{
    public bool CanCollect;
    private Item m_configData;
    private Outline outline;
    private Transform interactTrans;
    public Vector3 InteractPosition
    {
        get
        {
            return interactTrans.position;
        }
    }
    public bool IsOutlineShown
    {
        get
        {
            return outline.enabled;
        }
    }

    public Item Data
    {
        get
        {
            return m_configData;
        }
    }
    
    private void Start()
    {
        interactTrans = transform.Find("Destination");
        m_configData = DataTable.instance.ItemTable.Get(m_DataId);
        outline = this.gameObject.GetOrAddComponent<Outline>();
        outline.OutlineWidth = 5f;
        outline.enabled = false;
        this.gameObject.layer = LayerMask.NameToLayer("Interactive");
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    public void ShowOutline()
    {
        if (!IsOutlineShown)
            outline.enabled = true;
    }

    public void HideOutline()
    {
        if (IsOutlineShown)
            outline.enabled = false;
    }
}