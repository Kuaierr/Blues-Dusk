using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameKit.DataStructure
{
    [StructLayout(LayoutKind.Auto)]
    public struct LinkedListRange<T> : IEnumerable<T>, IEnumerable
    {
        private readonly LinkedListNode<T> m_First;
        private readonly LinkedListNode<T> m_Terminal;
        public LinkedListRange(LinkedListNode<T> first, LinkedListNode<T> terminal)
        {
            if (first == null || terminal == null || first == terminal)
            {
                throw new GameKitException("Range is invalid.");
            }

            m_First = first;
            m_Terminal = terminal;
        }
        public bool IsValid
        {
            get
            {
                return m_First != null && m_Terminal != null && m_First != m_Terminal;
            }
        }
        public LinkedListNode<T> First
        {
            get
            {
                return m_First;
            }
        }
        public LinkedListNode<T> Terminal
        {
            get
            {
                return m_Terminal;
            }
        }
        public int Count
        {
            get
            {
                if (!IsValid)
                {
                    return 0;
                }

                int count = 0;
                for (LinkedListNode<T> current = m_First; current != null && current != m_Terminal; current = current.Next)
                {
                    count++;
                }

                return count;
            }
        }
        public bool Contains(T value)
        {
            for (LinkedListNode<T> current = m_First; current != null && current != m_Terminal; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly LinkedListRange<T> m_LinkedListRange;
            private LinkedListNode<T> m_Current;
            private T m_CurrentValue;
            internal Enumerator(LinkedListRange<T> range)
            {
                if (!range.IsValid)
                {
                    throw new GameKitException("Range is invalid.");
                }

                m_LinkedListRange = range;
                m_Current = m_LinkedListRange.m_First;
                m_CurrentValue = default(T);
            }
            public T Current
            {
                get
                {
                    return m_CurrentValue;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return m_CurrentValue;
                }
            }

            public void Dispose()
            {
            }
            public bool MoveNext()
            {
                if (m_Current == null || m_Current == m_LinkedListRange.m_Terminal)
                {
                    return false;
                }

                m_CurrentValue = m_Current.Value;
                m_Current = m_Current.Next;
                return true;
            }
            void IEnumerator.Reset()
            {
                m_Current = m_LinkedListRange.m_First;
                m_CurrentValue = default(T);
            }
        }
    }
}
