using System.Runtime.InteropServices;

namespace GameKit
{
    [StructLayout(LayoutKind.Auto)]
    public struct TaskInfo
    {
        private readonly bool m_IsValid;
        private readonly int m_SerialId;
        private readonly string m_Tag;
        private readonly int m_Priority;
        private readonly object m_UserData;
        private readonly TaskStatus m_Status;
        private readonly string m_Description;

        public TaskInfo(int serialId, string tag, int priority, object userData, TaskStatus status, string description)
        {
            m_IsValid = true;
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Status = status;
            m_Description = description;
        }

        public bool IsValid
        {
            get
            {
                return m_IsValid;
            }
        }

        public int SerialId
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_SerialId;
            }
        }

        public string Tag
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_Tag;
            }
        }

        public int Priority
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_Priority;
            }
        }

        public object UserData
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_UserData;
            }
        }

        public TaskStatus Status
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_Status;
            }
        }

        public string Description
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameKitException("Data is invalid.");
                }

                return m_Description;
            }
        }
    }
}
