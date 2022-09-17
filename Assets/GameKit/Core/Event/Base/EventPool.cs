using System;
using System.Collections.Generic;
using GameKit.DataStructure;


namespace GameKit
{
    internal sealed partial class EventPool<T> where T : BaseEventArgs
    {
        private readonly GameKitMultiDictionary<int, EventHandler<T>> m_EventHandlers;
        private readonly Queue<Event> m_Events;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> m_CachedNodes;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> m_TempNodes;
        private readonly EventPoolMode m_EventPoolMode;
        private EventHandler<T> m_DefaultHandler;

        public EventPool(EventPoolMode mode)
        {
            m_EventHandlers = new GameKitMultiDictionary<int, EventHandler<T>>();
            m_Events = new Queue<Event>();
            m_CachedNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            m_TempNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            m_EventPoolMode = mode;
            m_DefaultHandler = null;
        }

        public int EventHandlerCount
        {
            get
            {
                return m_EventHandlers.Count;
            }
        }

        public int EventCount
        {
            get
            {
                return m_Events.Count;
            }
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            lock (m_Events)
            {
                while (m_Events.Count > 0)
                {
                    Event eventNode = m_Events.Dequeue();
                    HandleEvent(eventNode.Sender, eventNode.EventArgs);
                    ReferencePool.Release(eventNode);
                }
            }
        }

        public void Shutdown()
        {
            Clear();
            m_EventHandlers.Clear();
            m_CachedNodes.Clear();
            m_TempNodes.Clear();
            m_DefaultHandler = null;
        }

        public void Clear()
        {
            lock (m_Events)
            {
                m_Events.Clear();
            }
        }

        public int Count(int id)
        {
            GameKitLinkedListRange<EventHandler<T>> range = default(GameKitLinkedListRange<EventHandler<T>>);
            if (m_EventHandlers.TryGetValue(id, out range))
            {
                return range.Count;
            }

            return 0;
        }

        public bool Check(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameKitException("Event handler is invalid.");
            }

            return m_EventHandlers.Contains(id, handler);
        }

        public void Subscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameKitException("Event handler is invalid.");
            }

            if (!m_EventHandlers.Contains(id))
            {
                m_EventHandlers.Add(id, handler);
            }
            else if ((m_EventPoolMode & EventPoolMode.AllowMultiHandler) != EventPoolMode.AllowMultiHandler)
            {
                throw new GameKitException(Utility.Text.Format("Event '{0}' not allow multi handler.", id));
            }
            else if ((m_EventPoolMode & EventPoolMode.AllowDuplicateHandler) != EventPoolMode.AllowDuplicateHandler && Check(id, handler))
            {
                throw new GameKitException(Utility.Text.Format("Event '{0}' not allow duplicate handler.", id));
            }
            else
            {
                m_EventHandlers.Add(id, handler);
            }
        }

        public void Unsubscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameKitException("Event handler is invalid.");
            }

            if (m_CachedNodes.Count > 0)
            {
                foreach (KeyValuePair<object, LinkedListNode<EventHandler<T>>> cachedNode in m_CachedNodes)
                {
                    if (cachedNode.Value != null && cachedNode.Value.Value == handler)
                    {
                        m_TempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
                    }
                }

                if (m_TempNodes.Count > 0)
                {
                    foreach (KeyValuePair<object, LinkedListNode<EventHandler<T>>> cachedNode in m_TempNodes)
                    {
                        m_CachedNodes[cachedNode.Key] = cachedNode.Value;
                    }

                    m_TempNodes.Clear();
                }
            }

            if (!m_EventHandlers.Remove(id, handler))
            {
                throw new GameKitException(Utility.Text.Format("Event '{0}' not exists specified handler.", id));
            }
        }

        public void SetDefaultHandler(EventHandler<T> handler)
        {
            m_DefaultHandler = handler;
        }

        public void Fire(object sender, T e)
        {
            if (e == null)
            {
                throw new GameKitException("Event is invalid.");
            }

            Event eventNode = Event.Create(sender, e);
            lock (m_Events)
            {
                m_Events.Enqueue(eventNode);
            }
        }

        public void FireNow(object sender, T e)
        {
            if (e == null)
            {
                throw new GameKitException("Event is invalid.");
            }

            HandleEvent(sender, e);
        }

        private void HandleEvent(object sender, T e)
        {
            bool noHandlerException = false;
            GameKitLinkedListRange<EventHandler<T>> range = default(GameKitLinkedListRange<EventHandler<T>>);
            if (m_EventHandlers.TryGetValue(e.Id, out range))
            {
                LinkedListNode<EventHandler<T>> current = range.First;
                while (current != null && current != range.Terminal)
                {
                    m_CachedNodes[e] = current.Next != range.Terminal ? current.Next : null;
                    current.Value.Invoke(sender, e);
                    current = m_CachedNodes[e];
                }

                m_CachedNodes.Remove(e);
            }
            else if (m_DefaultHandler != null)
            {
                m_DefaultHandler(sender, e);
            }
            else if ((m_EventPoolMode & EventPoolMode.AllowNoHandler) == 0)
            {
                noHandlerException = true;
            }
            if (!e.IsManuallyRelease)
                ReferencePool.Release(e);

            if (noHandlerException)
            {
                throw new GameKitException(Utility.Text.Format("Event '{0}' not allow no handler.", e.Id));
            }
        }
    }
}
