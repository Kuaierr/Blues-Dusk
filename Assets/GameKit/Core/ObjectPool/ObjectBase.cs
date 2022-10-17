using System;


namespace GameKit.ObjectPool
{
    public abstract class ObjectBase : IReference
    {
        private string m_Name;
        private object m_Target;
        private bool m_Locked;
        private int m_Priority;
        private DateTime m_LastUseTime;

        public ObjectBase()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public object Target
        {
            get
            {
                return m_Target;
            }
        }

        public bool Locked
        {
            get
            {
                return m_Locked;
            }
            set
            {
                m_Locked = value;
            }
        }

        public int Priority
        {
            get
            {
                return m_Priority;
            }
            set
            {
                m_Priority = value;
            }
        }

        public virtual bool CustomCanReleaseFlag
        {
            get
            {
                return true;
            }
        }

        public DateTime LastUseTime
        {
            get
            {
                return m_LastUseTime;
            }
            internal set
            {
                m_LastUseTime = value;
            }
        }

        protected void Initialize(object target)
        {
            Initialize(null, target, false, 0);
        }

        protected void Initialize(string name, object target)
        {
            Initialize(name, target, false, 0);
        }

        protected void Initialize(string name, object target, bool locked)
        {
            Initialize(name, target, locked, 0);
        }

        protected void Initialize(string name, object target, int priority)
        {
            Initialize(name, target, false, priority);
        }

        protected void Initialize(string name, object target, bool locked, int priority)
        {
            if (target == null)
            {
                throw new GameKitException(Utility.Text.Format("Target '{0}' is invalid.", name));
            }

            m_Name = name ?? string.Empty;
            m_Target = target;
            m_Locked = locked;
            m_Priority = priority;
            m_LastUseTime = DateTime.UtcNow;
        }

        public virtual void Clear()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        protected internal virtual void OnSpawn()
        {
        }

        protected internal virtual void OnUnspawn()
        {
        }

        protected internal abstract void Release(bool isShutdown);
    }
}
