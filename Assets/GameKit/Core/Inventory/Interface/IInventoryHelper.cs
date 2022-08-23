namespace GameKit.Inventory
{
    public interface IInventoryHelper
    {
        IStock InitStock(IStock stock, object data);
    }
}