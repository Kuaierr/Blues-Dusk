using UnityEngine;
using UnityGameKit.Runtime;

public partial class GameKitCenter : MonoBehaviour
{
    public static GameKitCoreComponent Core { get; private set; }
    public static FsmComponent Fsm { get; private set; }
    public static ObjectPoolComponent ObjectPool { get; private set; }
    public static ProcedureComponent Procedure { get; private set; }
    public static EntityComponent Entity { get; private set; }
    public static UIComponent UI { get; private set; }
    public static ElementComponent Element { get; private set; }
    public static EventComponent Event { get; private set; }
    public static SettingComponent Setting { get; private set; }
    public static SceneComponent Scene { get; private set; }

    private static void InitComponents()
    {
        Core = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
        Fsm = GameKitComponentCenter.GetComponent<FsmComponent>();
        ObjectPool = GameKitComponentCenter.GetComponent<ObjectPoolComponent>();
        Procedure = GameKitComponentCenter.GetComponent<ProcedureComponent>();
        Entity = GameKitComponentCenter.GetComponent<EntityComponent>();
        UI = GameKitComponentCenter.GetComponent<UIComponent>();
        Element = GameKitComponentCenter.GetComponent<ElementComponent>();
        Event = GameKitComponentCenter.GetComponent<EventComponent>();
        Setting = GameKitComponentCenter.GetComponent<SettingComponent>();
        Scene = GameKitComponentCenter.GetComponent<SceneComponent>();
    }
}


