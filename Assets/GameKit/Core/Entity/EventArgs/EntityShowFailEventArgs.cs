using GameKit.Event;

namespace GameKit.EntityModule
{
    public class EntityShowFailEventArgs : GameEventArgs
    {
        public static int EventId = typeof(EntityShowFailEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public EntityShowFailEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public int EntityId
        {
            get;
            private set;
        }

        public string EntityAssetName
        {
            get;
            private set;
        }

        public string EntityGroupName
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static EntityShowFailEventArgs Create(int entityId, string entityAssetName, string entityGroupName, string errorMessage, object userData)
        {
            EntityShowFailEventArgs entityShowFailureEventArgs = ReferencePool.Acquire<EntityShowFailEventArgs>();
            entityShowFailureEventArgs.EntityId = entityId;
            entityShowFailureEventArgs.EntityAssetName = entityAssetName;
            entityShowFailureEventArgs.EntityGroupName = entityGroupName;
            entityShowFailureEventArgs.ErrorMessage = errorMessage;
            entityShowFailureEventArgs.UserData = userData;
            return entityShowFailureEventArgs;
        }

        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}

