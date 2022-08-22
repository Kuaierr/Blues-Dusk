using System;

namespace GameKit.Event
{
    public interface IEventManager
    {
        int EventHandlerCount
        {
            get;
        }

        int EventCount
        {
            get;
        }

        int Count(int id);

        bool Check(int id, EventHandler<GameEventArgs> handler);

        void Subscribe(int id, EventHandler<GameEventArgs> handler);

        void Unsubscribe(int id, EventHandler<GameEventArgs> handler);

        void SetDefaultHandler(EventHandler<GameEventArgs> handler);

        void Fire(object sender, GameEventArgs e);

        void FireNow(object sender, GameEventArgs e);
    }
}
