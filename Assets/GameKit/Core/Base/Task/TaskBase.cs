namespace GameKit
{
    public abstract class TaskBase : IReference
    {
        public const int DefaultPriority = 0;

        private int m_SerialId;
        private string m_Tag;
        private int m_Priority;
        private object m_UserData;

        private bool m_Done;

        public TaskBase()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_Done = false;
            m_UserData = null;
        }

        public int SerialId
        {
            get
            {
                return m_SerialId;
            }
        }

        public string Tag
        {
            get
            {
                return m_Tag;
            }
        }

        public int Priority
        {
            get
            {
                return m_Priority;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        public bool Done
        {
            get
            {
                return m_Done;
            }
            set
            {
                m_Done = value;
            }
        }

        public virtual string Description
        {
            get
            {
                return null;
            }
        }

        internal void Initialize(int serialId, string tag, int priority, object userData)
        {
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Done = false;
        }

        public virtual void Clear()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_UserData = null;
            m_Done = false;
        }
    }
}
