using UnityEngine;
using UnityGameKit.Runtime;

public partial class GameKitCenter : MonoBehaviour
{
    public static InventoryComponent Inventory { get; private set; }
    public static DialogComponent Dialog { get; private set; }
    public static DataComponent Data { get; private set; }
    public static SchedulerComponent Scheduler { get; private set; }

    private static void InitCustomComponents()
    {
        Inventory = GameKitComponentCenter.GetComponent<InventoryComponent>();
        Dialog = GameKitComponentCenter.GetComponent<DialogComponent>();
        Data = GameKitComponentCenter.GetComponent<DataComponent>();
        Scheduler = GameKitComponentCenter.GetComponent<SchedulerComponent>();
    }
}


