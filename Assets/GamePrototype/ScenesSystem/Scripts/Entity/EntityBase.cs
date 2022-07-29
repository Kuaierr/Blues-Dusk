using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private int m_id;
    private string m_AssetName;
    private object m_Instance;
    private IEntityManager m_manager;
    private EntityLogic m_EntityLogic;

    public int Id
    {
        get
        {
            return m_id;
        }
    }

    public string AssetName
    {
        get
        {
            return m_AssetName;
        }
    }

    public object Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public IEntityManager Manager
    {
        get
        {
            return m_manager;
        }
    }

    public virtual void OnInit(int entityId, string name, IEntityManager manager, object userData)
    {
        this.m_id = entityId;
        this.m_AssetName = name;
        this.m_Instance = userData;
        this.m_manager = manager;
    }

    public abstract void OnShow(object userData);
    public abstract void OnHide(object userData);
    public abstract void OnFlush(object userData);
    public abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);
    public virtual void OnRecycle()
    {

    }
    public virtual void ShutDown()
    {
        // m_manager
    }

}