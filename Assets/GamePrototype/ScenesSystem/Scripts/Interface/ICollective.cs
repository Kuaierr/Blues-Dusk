public interface ICollective
{
    void OnCollect();
    void OnCollectEnter();
    void OnCollectExit();
    void Collect();
    bool CanCollect();
}