using UnityEngine;
using GameKit;
using GameKit.ObjectPool;

namespace UnityGameKit.Demo
{
    public class BaseObject : ObjectBase
    {
        public static BaseObject Create(object target)
        {
            BaseObject baseObject = ReferencePool.Acquire<BaseObject>();
            baseObject.Initialize(target);
            return baseObject;
        }

        protected override void Release(bool isShutdown)
        {
            MonoObject monoObject = (MonoObject)Target;
            if (monoObject == null)
                return;
            Object.Destroy(monoObject.gameObject);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            MonoObject monoObject = (MonoObject)Target;
            monoObject.gameObject.SetActive(true);
        }

        protected override void OnUnspawn()
        {
            base.OnUnspawn();
            MonoObject monoObject = (MonoObject)Target;
            monoObject.gameObject.SetActive(false);
        }
    }

}

