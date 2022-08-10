using System;
using System.Runtime.InteropServices;

namespace GameKit
{
    [StructLayout(LayoutKind.Auto)]
    public struct ReferencePoolInfo
    {
        private readonly Type m_Type;
        private readonly int m_UnusedReferenceCount;
        private readonly int m_UsingReferenceCount;
        private readonly int m_AcquireReferenceCount;
        private readonly int m_ReleaseReferenceCount;
        private readonly int m_AddReferenceCount;
        private readonly int m_RemoveReferenceCount;

        public ReferencePoolInfo(Type type, int unusedReferenceCount, int usingReferenceCount, int acquireReferenceCount, int releaseReferenceCount, int addReferenceCount, int removeReferenceCount)
        {
            m_Type = type;
            m_UnusedReferenceCount = unusedReferenceCount;
            m_UsingReferenceCount = usingReferenceCount;
            m_AcquireReferenceCount = acquireReferenceCount;
            m_ReleaseReferenceCount = releaseReferenceCount;
            m_AddReferenceCount = addReferenceCount;
            m_RemoveReferenceCount = removeReferenceCount;
        }

        public Type Type
        {
            get
            {
                return m_Type;
            }
        }

        public int UnusedReferenceCount
        {
            get
            {
                return m_UnusedReferenceCount;
            }
        }

        public int UsingReferenceCount
        {
            get
            {
                return m_UsingReferenceCount;
            }
        }

        public int AcquireReferenceCount
        {
            get
            {
                return m_AcquireReferenceCount;
            }
        }

        public int ReleaseReferenceCount
        {
            get
            {
                return m_ReleaseReferenceCount;
            }
        }

        public int AddReferenceCount
        {
            get
            {
                return m_AddReferenceCount;
            }
        }

        public int RemoveReferenceCount
        {
            get
            {
                return m_RemoveReferenceCount;
            }
        }
    }
}
