using GameKit.Event;

namespace GameKit.EntityModule
{
    public class EntityShowSuccessEventArgs : GameEventArgs
    {
        public static int EventId = typeof(EntityShowSuccessEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public EntityShowSuccessEventArgs()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }

        public IEntity Entity
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

        public static EntityShowSuccessEventArgs Create(IEntity entity, float duration, object userData)
        {
            EntityShowSuccessEventArgs entityShowSuccessEventArgs = ReferencePool.Acquire<EntityShowSuccessEventArgs>();
            entityShowSuccessEventArgs.Entity = entity;
            entityShowSuccessEventArgs.Duration = duration;
            entityShowSuccessEventArgs.UserData = userData;
            return entityShowSuccessEventArgs;
        }

        public override void Clear()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }
    }
}

