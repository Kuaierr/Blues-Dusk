public interface IInventoryChunk
{
    int Index { get; }
    bool IsEmpty { get; }
    bool HasFull(IStock stock);
    IStock GetStock();
    void SetStock(IStock stock);
    bool ModifyStock<T>(InventoryCallback<T> callback) where T: class;
    void Clear();
}