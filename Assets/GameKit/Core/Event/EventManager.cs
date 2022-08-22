using System;

namespace GameKit.Event
{
    internal sealed class EventManager : GameKitModule, IEventManager
    {
        private readonly EventPool<GameEventArgs> m_EventPool;

        public EventManager()
        {
            m_EventPool = new EventPool<GameEventArgs>(EventPoolMode.AllowNoHandler | EventPoolMode.AllowMultiHandler);
        }

        public int EventHandlerCount
        {
            get
            {
                return m_EventPool.EventHandlerCount;
            }
        }

        public int EventCount
        {
            get
            {
                return m_EventPool.EventCount;
            }
        }

        internal override int Priority
        {
            get
            {
                return 7;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_EventPool.Update(elapseSeconds, realElapseSeconds);
        }

        internal override void Shutdown()
        {
            m_EventPool.Shutdown();
        }

        public int Count(int id)
        {
            return m_EventPool.Count(id);
        }

        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return m_EventPool.Check(id, handler);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Subscribe(id, handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Unsubscribe(id, handler);
        }

        public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
        {
            m_EventPool.SetDefaultHandler(handler);
        }

        public void Fire(object sender, GameEventArgs e)
        {
            m_EventPool.Fire(sender, e);
        }

        public void FireNow(object sender, GameEventArgs e)
        {
            m_EventPool.FireNow(sender, e);
        }
    }
}
