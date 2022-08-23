using GameKit;
using GameKit.Event;
using System;

namespace UnityGameKit.Runtime
{
    public sealed class EntityShowSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(EntityShowSuccessEventArgs).GetHashCode();

        public EntityShowSuccessEventArgs()
        {
            EntityLogicType = null;
            Entity = null;
            Duration = 0f;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public Type EntityLogicType
        {
            get;
            private set;
        }

        public Entity Entity
        {
            get;
            private set;
        }

        public float Duration
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static EntityShowSuccessEventArgs Create(GameKit.EntityModule.EntityShowSuccessEventArgs e)
        {
            ShowEntityInfo showEntityInfo = (ShowEntityInfo)e.UserData;
            EntityShowSuccessEventArgs entityShowSuccessEventArgs = ReferencePool.Acquire<EntityShowSuccessEventArgs>();
            entityShowSuccessEventArgs.EntityLogicType = showEntityInfo.EntityLogicType;
            entityShowSuccessEventArgs.Entity = (Entity)e.Entity;
            entityShowSuccessEventArgs.Duration = e.Duration;
            entityShowSuccessEventArgs.UserData = showEntityInfo.UserData;
            ReferencePool.Release(showEntityInfo);
            return entityShowSuccessEventArgs;
        }

        public override void Clear()
        {
            EntityLogicType = null;
            Entity = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
