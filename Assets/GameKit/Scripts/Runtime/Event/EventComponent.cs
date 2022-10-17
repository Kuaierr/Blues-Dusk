using GameKit;
using GameKit.Event;
using System;
using UnityEngine;


namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Event Component")]
    public sealed class EventComponent : GameKitComponent
    {
        private IEventManager m_EventManager = null;

        public int EventHandlerCount
        {
            get
            {
                return m_EventManager.EventHandlerCount;
            }
        }

        public int EventCount
        {
            get
            {
                return m_EventManager.EventCount;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_EventManager = GameKitModuleCenter.GetModule<IEventManager>();
            if (m_EventManager == null)
            {
                Utility.Debugger.LogFail("Event manager is invalid.");
                return;
            }
        }

        private void Start()
        {
        }

        public int Count(int id)
        {
            return m_EventManager.Count(id);
        }

        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return m_EventManager.Check(id, handler);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventManager.Subscribe(id, handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventManager.Unsubscribe(id, handler);
        }

        public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
        {
            m_EventManager.SetDefaultHandler(handler);
        }

        public void Fire(object sender, GameEventArgs e)
        {
            m_EventManager.Fire(sender, e);
        }

        public void FireNow(object sender, GameEventArgs e)
        {
            m_EventManager.FireNow(sender, e);
        }
    }
}
