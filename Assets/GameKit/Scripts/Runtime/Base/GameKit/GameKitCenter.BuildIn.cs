using UnityEngine;

namespace UnityGameKit.Runtime
{
    public partial class GameKitCenter : MonoBehaviour
    {
        public static GameKitCoreComponent Core { get; private set; }
        public static FsmComponent Fsm { get; private set; }
        public static DataTableComponent DataTable { get; private set; }
        public static ObjectPoolComponent ObjectPool { get; private set; }
        public static ProcedureComponent Procedure { get; private set; }
        public static EntityComponent Entity { get; private set; }
        public static UIComponent UI { get; private set; }
        public static ElementComponent Element { get; private set; }
        public static EventComponent Event { get; private set; }

        private void Start()
        {
            InitComponents();
            InitCustomComponents();
        }

        private static void InitComponents()
        {
            Core = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            Fsm = GameKitComponentCenter.GetComponent<FsmComponent>();
            DataTable = GameKitComponentCenter.GetComponent<DataTableComponent>();
            ObjectPool = GameKitComponentCenter.GetComponent<ObjectPoolComponent>();
            Procedure = GameKitComponentCenter.GetComponent<ProcedureComponent>();
            Entity = GameKitComponentCenter.GetComponent<EntityComponent>();
            UI = GameKitComponentCenter.GetComponent<UIComponent>();
            Element = GameKitComponentCenter.GetComponent<ElementComponent>();
            Event = GameKitComponentCenter.GetComponent<EventComponent>();
        }
    }
}

