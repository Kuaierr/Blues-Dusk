using LubanConfig;
using LubanConfig.Stock;
namespace GameKit
{
    public sealed class DefaultInventoryHelper : InventoryHelperBase
    {
        public override IStock InitStock(IStock stock, object data)
        {
            if (data.GetType() == typeof(Item))
            {
                Item itemData = (Item)data;
                stock.OnHelperInit(1, itemData.MaxOverlap);
                return stock;
            }
            return stock;
        }
    }
}
