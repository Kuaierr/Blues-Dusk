
using UnityEngine;
public abstract class InventoryHelperBase : MonoBehaviour, IInventoryHelper
{
    public abstract IStock InitStock<T>(IStock stock, T data) where T : class;
}