
namespace GameKit.EntityModule
{
    public sealed class EntityHideCompleteEventArgs : GameKitEventArgs
    {
        public EntityHideCompleteEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroup = null;
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

        public static EntityHideCompleteEventArgs Create(int entityId, string entityAssetName, IEntityGroup entityGroup, object userData)
        {
            EntityHideCompleteEventArgs hideEntityCompleteEventArgs = ReferencePool.Acquire<EntityHideCompleteEventArgs>();
            hideEntityCompleteEventArgs.EntityId = entityId;
            hideEntityCompleteEventArgs.EntityAssetName = entityAssetName;
            hideEntityCompleteEventArgs.EntityGroup = entityGroup;
            hideEntityCompleteEventArgs.UserData = userData;
            return hideEntityCompleteEventArgs;
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
