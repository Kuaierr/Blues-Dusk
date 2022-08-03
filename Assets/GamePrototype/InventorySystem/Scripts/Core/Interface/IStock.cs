public interface IStock
{
    int Id { get; }
    int SerialId { get; }
    int SlotIndex { get; }
    string Name { get; }
    bool IsAvaliable { get; }
    object Data { get; }
    IInventory Inventory { get; }
    int Overlap { get; }
    int MaxOverlap { get; }
    void OnChunkSlot(int index);
    void OnHelperInit(int overlap, int maxOverlap);
    void AddOverlap(int count = 1);
    void OnInteract();
}