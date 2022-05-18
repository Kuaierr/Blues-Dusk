namespace GameKit.DataStructure
{
    public abstract class NodeType
    {
        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnFinish();
        public abstract void OnWait();
        public abstract void OnExit();
    }
}

