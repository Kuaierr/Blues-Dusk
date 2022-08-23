namespace GameKit.Inventory
{
    public interface IInventory
    {
        int Id { get; }
        int Size { get; }
        int Count { get; }
        int OverlapCount { get; }
        string Name { get; }
        IStock[] StockMap { get; }
        IStock CreateStock<T>(int id, string name, T data = default(T)) where T : class;
        bool HasFull(IStock stock, bool useCache = true);
        bool AddStock(IStock stock);
        bool AddStock(IStock stock, int count);

        bool HasStock(string name, bool useCache = true);
        IStock GetStock(string name);
        bool RemoveStock(string name);
        bool SetStock(string name, IStock stock);
        bool ModifyStock<T>(string name, InventoryCallback<T> callback) where T : class;

        bool HasStock(int id, string name, bool useCache = true);
        IStock GetStock(int id, string name);
        bool RemoveStock(int id, string name);
        bool SetStock(int id, string name, IStock stock);
        bool ModifyStock<T>(int id, string name, InventoryCallback<T> callback) where T : class;

        bool HasStock(int index, bool useCache = true);
        IStock GetStock(int index);
        bool RemoveStock(int index);
        bool SetStock(int index, IStock stock);
        bool ModifyStock<T>(int index, InventoryCallback<T> callback) where T : class;
        void Clear();
        void SetHelper(IInventoryHelper helper);
    }
}

