using GameKit.EntityModule;
using GameKit.Event;
using GameKit;


namespace UnityGameKit.Runtime
{
    public sealed class EntityHideCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(EntityHideCompleteEventArgs).GetHashCode();

        public EntityHideCompleteEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroup = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
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

        public IEntityGroup EntityGroup
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static EntityHideCompleteEventArgs Create(GameKit.EntityModule.EntityHideCompleteEventArgs e)
        {
            EntityHideCompleteEventArgs entityHideCompleteEventArgs = ReferencePool.Acquire<EntityHideCompleteEventArgs>();
            entityHideCompleteEventArgs.EntityId = e.EntityId;
            entityHideCompleteEventArgs.EntityAssetName = e.EntityAssetName;
            entityHideCompleteEventArgs.EntityGroup = e.EntityGroup;
            entityHideCompleteEventArgs.UserData = e.UserData;
            return entityHideCompleteEventArgs;
        }

        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroup = null;
            UserData = null;
        }
    }
}
