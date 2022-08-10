using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameKit.DataStructure
{
    public sealed class MultiDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, LinkedListRange<TValue>>>, IEnumerable
    {
        private readonly CachedLinkedList<TValue> m_LinkedList;
        private readonly Dictionary<TKey, LinkedListRange<TValue>> m_Dictionary;

        public MultiDictionary()
        {
            m_LinkedList = new CachedLinkedList<TValue>();
            m_Dictionary = new Dictionary<TKey, LinkedListRange<TValue>>();
        }

        public int Count
        {
            get
            {
                return m_Dictionary.Count;
            }
        }

        public LinkedListRange<TValue> this[TKey key]
        {
            get
            {
                LinkedListRange<TValue> range = default(LinkedListRange<TValue>);
                m_Dictionary.TryGetValue(key, out range);
                return range;
            }
        }

        public void Clear()
        {
            m_Dictionary.Clear();
            m_LinkedList.Clear();
        }

        public bool Contains(TKey key)
        {
            return m_Dictionary.ContainsKey(key);
        }

        public bool Contains(TKey key, TValue value)
        {
            LinkedListRange<TValue> range = default(LinkedListRange<TValue>);
            if (m_Dictionary.TryGetValue(key, out range))
            {
                return range.Contains(value);
            }

            return false;
        }

        public bool TryGetValue(TKey key, out LinkedListRange<TValue> range)
        {
            return m_Dictionary.TryGetValue(key, out range);
        }

        public void Add(TKey key, TValue value)
        {
            LinkedListRange<TValue> range = default(LinkedListRange<TValue>);
            if (m_Dictionary.TryGetValue(key, out range))
            {
                m_LinkedList.AddBefore(range.Terminal, value);
            }
            else
            {
                LinkedListNode<TValue> first = m_LinkedList.AddLast(value);
                LinkedListNode<TValue> terminal = m_LinkedList.AddLast(default(TValue));
                m_Dictionary.Add(key, new LinkedListRange<TValue>(first, terminal));
            }
        }

        public bool Remove(TKey key, TValue value)
        {
            LinkedListRange<TValue> range = default(LinkedListRange<TValue>);
            if (m_Dictionary.TryGetValue(key, out range))
            {
                for (LinkedListNode<TValue> current = range.First; current != null && current != range.Terminal; current = current.Next)
                {
                    if (current.Value.Equals(value))
                    {
                        if (current == range.First)
                        {
                            LinkedListNode<TValue> next = current.Next;
                            if (next == range.Terminal)
                            {
                                m_LinkedList.Remove(next);
                                m_Dictionary.Remove(key);
                            }
                            else
                            {
                                m_Dictionary[key] = new LinkedListRange<TValue>(next, range.Terminal);
                            }
                        }

                        m_LinkedList.Remove(current);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool RemoveAll(TKey key)
        {
            LinkedListRange<TValue> range = default(LinkedListRange<TValue>);
            if (m_Dictionary.TryGetValue(key, out range))
            {
                m_Dictionary.Remove(key);

                LinkedListNode<TValue> current = range.First;
                while (current != null)
                {
                    LinkedListNode<TValue> next = current != range.Terminal ? current.Next : null;
                    m_LinkedList.Remove(current);
                    current = next;
                }

                return true;
            }

            return false;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_Dictionary);
        }

        IEnumerator<KeyValuePair<TKey, LinkedListRange<TValue>>> IEnumerable<KeyValuePair<TKey, LinkedListRange<TValue>>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, LinkedListRange<TValue>>>, IEnumerator
        {
            private Dictionary<TKey, LinkedListRange<TValue>>.Enumerator m_Enumerator;

            internal Enumerator(Dictionary<TKey, LinkedListRange<TValue>> dictionary)
            {
                if (dictionary == null)
                {
                    throw new GameKitException("Dictionary is invalid.");
                }

                m_Enumerator = dictionary.GetEnumerator();
            }

   
   
   
            public KeyValuePair<TKey, LinkedListRange<TValue>> Current
            {
                get
                {
                    return m_Enumerator.Current;
                }
            }

   
   
   
            object IEnumerator.Current
            {
                get
                {
                    return m_Enumerator.Current;
                }
            }

   
   
   
            public void Dispose()
            {
                m_Enumerator.Dispose();
            }

   
   
   
   
            public bool MoveNext()
            {
                return m_Enumerator.MoveNext();
            }

   
   
   
            void IEnumerator.Reset()
            {
                ((IEnumerator<KeyValuePair<TKey, LinkedListRange<TValue>>>)m_Enumerator).Reset();
            }
        }
    }
}
