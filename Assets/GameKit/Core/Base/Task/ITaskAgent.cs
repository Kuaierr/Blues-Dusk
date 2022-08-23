namespace GameKit
{
    public interface ITaskAgent<T> where T : TaskBase
    {
        T Task
        {
            get;
        }

        void Initialize();

        void Update(float elapseSeconds, float realElapseSeconds);

        void Shutdown();

        TaskStartStatus Start(T task);

        void Reset();
    }
}
