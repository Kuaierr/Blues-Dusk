
using UnityEngine;
public abstract class InventoryHelperBase : MonoBehaviour, IInventoryHelper
{
    public abstract IStock InitStock(IStock stock, object data);
}