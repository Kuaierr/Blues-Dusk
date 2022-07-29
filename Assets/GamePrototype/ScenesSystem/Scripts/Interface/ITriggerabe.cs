public interface ITriggerabe
{
    void OnActivate();
    void OnEnterActivate();
    void OnExitActivate();
    void Trigger();
    void UnTrigger();
    void BeTrigger(object entity);
    void BeUnTrigger(object entity);
    bool CanTrigger();
}