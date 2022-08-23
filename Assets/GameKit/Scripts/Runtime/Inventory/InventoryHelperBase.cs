
using UnityEngine;
using GameKit.Inventory;

namespace UnityGameKit.Runtime
{
    public abstract class InventoryHelperBase : MonoBehaviour, IInventoryHelper
    {
        public abstract IStock InitStock(IStock stock, object data);
    }
}
