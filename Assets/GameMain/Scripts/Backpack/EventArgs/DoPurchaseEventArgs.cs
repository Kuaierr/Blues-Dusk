using System.Text;
using GameKit;
using GameKit.Event;
using GameKit.Inventory;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    public sealed class DoPurchaseEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DoPurchaseEventArgs).GetHashCode();

        public DoPurchaseEventArgs()
        {
            Stock = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public IStock Stock
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static DoPurchaseEventArgs Create(IStock stock, object userData)
        {
            DoPurchaseEventArgs doPurchaseEventArgs = ReferencePool.Acquire<DoPurchaseEventArgs>();
            doPurchaseEventArgs.Stock = stock;
            doPurchaseEventArgs.UserData = userData;
            return doPurchaseEventArgs;
        }

        public override void Clear()
        {
            Stock = null;
            UserData = null;
        }
    }
}
