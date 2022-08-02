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
    void OnInit(string name, object data);
    void OnChunkSlot(int index);
    void AddOverlap(int count = 1);
    void OnInteract();
}