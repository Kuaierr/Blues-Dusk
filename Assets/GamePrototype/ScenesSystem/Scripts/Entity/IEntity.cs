using UnityEngine;

public interface IEntity
{
    int Id { get; }
    string AssetName { get; }
    object Instance { get; }
    IEntityManager Manager { get; }
    void OnInit(int entityId, string name, IEntityManager manager, object userData);
    void OnShow(object userData);
    void OnHide(object userData);
    void OnFlush(object userData);
    void OnAttached(IEntity childEntity, object userData);
    void OnDetached(IEntity childEntity, object userData);
    void OnAttachTo(IEntity parentEntity, object userData);
    void OnDetachFrom(IEntity parentEntity, object userData);
    void OnUpdate(float elapseSeconds, float realElapseSeconds);
    void OnRecycle();
    void ShutDown();
}