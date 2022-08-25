using GameKit.Event;
using GameKit;
using System;

namespace UnityGameKit.Runtime
{
    public sealed class EntityShowFailEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(EntityShowFailEventArgs).GetHashCode();

        public EntityShowFailEventArgs()
        {
            EntityId = 0;
            EntityLogicType = null;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
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

        public Type EntityLogicType
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

        public static EntityShowFailEventArgs Create(GameKit.Entity.EntityShowFailEventArgs e)
        {
            ShowEntityInfo showEntityInfo = (ShowEntityInfo)e.UserData;
            EntityShowFailEventArgs entityShowFailEventArgs = ReferencePool.Acquire<EntityShowFailEventArgs>();
            entityShowFailEventArgs.EntityId = e.EntityId;
            entityShowFailEventArgs.EntityLogicType = showEntityInfo.EntityLogicType;
            entityShowFailEventArgs.EntityAssetName = e.EntityAssetName;
            entityShowFailEventArgs.EntityGroupName = e.EntityGroupName;
            entityShowFailEventArgs.ErrorMessage = e.ErrorMessage;
            entityShowFailEventArgs.UserData = showEntityInfo.UserData;
            ReferencePool.Release(showEntityInfo);
            return entityShowFailEventArgs;
        }

        public override void Clear()
        {
            EntityId = 0;
            EntityLogicType = null;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
