using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

public abstract class GameElementBase : ElementBase, IInteractive
{
    public float OutlineWidth = 3f;
    public UnityEvent InteractCallback;
    private Outline m_Outline;
    private Transform m_InteractTrans;
    public Vector3 InteractPosition
    {
        get
        {
            return m_InteractTrans.position;
        }
    }
    public bool IsOutlineShown
    {
        get
        {
            return m_Outline.enabled;
        }
    }

    protected override void Start()
    {
        m_InteractTrans = transform.Find("Destination");
        m_Outline = this.gameObject.GetOrAddComponent<Outline>();
        m_Outline.OutlineWidth = OutlineWidth;
        m_Outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
    }

    public override void OnInteract()
    {
        base.OnInteract();
        InteractCallback?.Invoke();
    }

    public void ShowOutline()
    {
        if (!IsOutlineShown)
            m_Outline.enabled = true;
    }

    public void HideOutline()
    {
        if (IsOutlineShown)
            m_Outline.enabled = false;
    }
}