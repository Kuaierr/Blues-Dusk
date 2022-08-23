using UnityEngine;

namespace UnityGameKit.Runtime
{
    public class GameKitCenter : MonoBehaviour
    {
        public static GameKitCoreComponent Core { get; private set; }
        public static FsmComponent Fsm { get; private set; }

        private void Start()
        {
            InitComponents();
        }

        private static void InitComponents()
        {
            Core = UnityGameKit.Runtime.GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            Fsm = UnityGameKit.Runtime.GameKitComponentCenter.GetComponent<FsmComponent>();
        }
    }
}

