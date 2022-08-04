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
    bool HasStock(string name, bool useCache = true);
    bool HasStock(int index, bool useCache = true);
    bool AddStock(IStock stock);
    bool AddStock(IStock stock, int count);
    IStock GetStock(string name);
    bool RemoveStock(string name);
    bool SetStock(string name, IStock stock);
    bool ModifyStock<T>(string name, InventoryCallback<T> callback) where T : class;
    IStock GetStock(int index);
    bool RemoveStock(int index);
    bool SetStock(int index, IStock stock);
    bool ModifyStock<T>(int index, InventoryCallback<T> callback) where T : class;
    void Clear();
    void SetHelper(IInventoryHelper helper);
}