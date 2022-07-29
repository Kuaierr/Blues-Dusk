public interface IObservable
{
    void OnObserve();
    void OnObserveEnter();
    void OnObserveExit();
    void AfterObserved();
}