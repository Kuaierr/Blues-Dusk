using UnityEngine;

namespace UnityGameKit.Runtime
{
    public partial class GameKitCenter : MonoBehaviour
    {
        public static InventoryComponent Inventory { get; private set; }
        public static DialogComponent Dialog { get; private set; }

        private static void InitCustomComponents()
        {
            Inventory = GameKitComponentCenter.GetComponent<InventoryComponent>();
            Dialog = GameKitComponentCenter.GetComponent<DialogComponent>();
        }
    }
}

