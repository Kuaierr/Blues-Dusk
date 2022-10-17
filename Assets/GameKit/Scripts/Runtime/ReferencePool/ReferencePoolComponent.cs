using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit ReferencePool Component")]
    public sealed class ReferencePoolComponent : GameKitComponent
    {
        [SerializeField]
        private ReferenceStrictCheckType m_EnableStrictCheck = ReferenceStrictCheckType.AlwaysEnable;

        public bool EnableStrictCheck
        {
            get
            {
                return ReferencePool.EnableStrictCheck;
            }
            set
            {
                ReferencePool.EnableStrictCheck = value;
                if (value)
                {
                    Log.Info("Strict checking is enabled for the Reference Pool. It will drastically affect the performance.");
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            switch (m_EnableStrictCheck)
            {
                case ReferenceStrictCheckType.AlwaysEnable:
                    EnableStrictCheck = true;
                    break;

                case ReferenceStrictCheckType.OnlyEnableWhenDevelopment:
                    EnableStrictCheck = Debug.isDebugBuild;
                    break;

                case ReferenceStrictCheckType.OnlyEnableInEditor:
                    EnableStrictCheck = Application.isEditor;
                    break;

                default:
                    EnableStrictCheck = false;
                    break;
            }
        }
    }
}
