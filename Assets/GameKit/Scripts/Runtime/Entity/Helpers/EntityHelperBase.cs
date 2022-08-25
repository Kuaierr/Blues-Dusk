using UnityEngine;
using GameKit.Entity;

namespace UnityGameKit.Runtime
{
    public abstract class EntityHelperBase : MonoBehaviour, IEntityHelper
    {
        public abstract object InstantiateEntity(object entityAsset);
        public abstract IEntity CreateEntity(object entityInstance, IEntityGroup entityGroup, object userData);
        public abstract void ReleaseEntity(object entityAsset, object entityInstance);
    }
}
