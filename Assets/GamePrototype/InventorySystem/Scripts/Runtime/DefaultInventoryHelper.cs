namespace GameKit
{
    public sealed class DefaultInventoryHelper : InventoryHelperBase
    {
        public override IStock InitStock<T>(IStock stock, T data) where T : class
        {
            return stock;
        }
    }
}
