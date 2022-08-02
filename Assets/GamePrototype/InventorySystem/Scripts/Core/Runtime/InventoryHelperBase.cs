public abstract class InventoryHelperBase : IInventoryHelper
{
    public abstract IStock InitStock(IStock stock, object data);
}