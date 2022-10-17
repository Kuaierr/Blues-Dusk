using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;

namespace GameKit.QuickCode
{
    public interface IEventInfo { }
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction<T>;
            }
        }
    }

    public class EventInfo<T0, T1> : IEventInfo
    {
        public UnityAction<T0, T1> actions;
        public EventInfo(UnityAction<T0, T1> action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction<T0, T1>;
            }
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction;
            }
        }
    }
    public class EventManager : SingletonBase<EventManager>
    {

        private Dictionary<string, IEventInfo> m_CachedNameEvents = new Dictionary<string, IEventInfo>();
        private Dictionary<int, IEventInfo> m_CachedIdEvents = new Dictionary<int, IEventInfo>();

        public void AddEventListener<T>(int id, UnityAction<T> action)
        {
            if (m_CachedIdEvents.ContainsKey(id))
            {
                (m_CachedIdEvents[id] as EventInfo<T>).actions += action;
            }
            else
            {
                m_CachedIdEvents.Add(id, new EventInfo<T>(action));
            }
        }

        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T>).actions += action;
            }
            else
            {
                m_CachedNameEvents.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T0, T1>).actions += action;
            }
            else
            {
                m_CachedNameEvents.Add(name, new EventInfo<T0, T1>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo).actions += action;
            }
            else
            {
                m_CachedNameEvents.Add(name, new EventInfo(action));
            }
        }


        public void EventTrigger<T>(string name, T info)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                if ((m_CachedNameEvents[name] as EventInfo<T>).actions != null)
                {
                    (m_CachedNameEvents[name] as EventInfo<T>).actions?.Invoke(info);
                }
            }
        }

        public void EventTrigger<T>(int id, T info)
        {
            if (m_CachedIdEvents.ContainsKey(id))
            {
                if ((m_CachedIdEvents[id] as EventInfo<T>).actions != null)
                {
                    (m_CachedIdEvents[id] as EventInfo<T>).actions?.Invoke(info);
                }
            }
        }

        public void EventTrigger<T0, T1>(string name, T0 info1, T1 info2)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                if ((m_CachedNameEvents[name] as EventInfo<T0, T1>).actions != null)
                {
                    (m_CachedNameEvents[name] as EventInfo<T0, T1>).actions?.Invoke(info1, info2);
                }
            }
        }

        public void EventTrigger(string name)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                if ((m_CachedNameEvents[name] as EventInfo).actions != null)
                {
                    (m_CachedNameEvents[name] as EventInfo).actions.Invoke();
                }
            }
        }

        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T>).actions -= action;
            }
        }

        public void RemoveEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T0, T1>).actions -= action;
            }
        }

        public void RemoveEventListener(string name, UnityAction action)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo).actions -= action;
            }
        }

        public void ClearEventListener(string name)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo).Clear();
            }
        }

        public void ClearEventListener<T>(string name)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T>).Clear();
            }
        }

        public void ClearEventListener<T0, T1>(string name)
        {
            if (m_CachedNameEvents.ContainsKey(name))
            {
                (m_CachedNameEvents[name] as EventInfo<T0, T1>).Clear();
            }
        }
        public override void Clear()
        {
            m_CachedNameEvents.Clear();
        }

        public Dictionary<string, IEventInfo> GetEvents()
        {
            return m_CachedNameEvents;
        }

    }
}