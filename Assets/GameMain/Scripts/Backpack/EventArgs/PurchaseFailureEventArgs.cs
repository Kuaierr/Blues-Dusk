using System.Text;
using GameKit;
using GameKit.Event;
using GameKit.Inventory;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    public sealed class PurchaseFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(PurchaseFailureEventArgs).GetHashCode();

        public PurchaseFailureEventArgs()
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

        public static PurchaseFailureEventArgs Create(IStock stock, object userData)
        {
            PurchaseFailureEventArgs purchaseFailureEventArgs = ReferencePool.Acquire<PurchaseFailureEventArgs>();
            purchaseFailureEventArgs.Stock = stock;
            purchaseFailureEventArgs.UserData = userData;
            return purchaseFailureEventArgs;
        }

        public override void Clear()
        {
            Stock = null;
            UserData = null;
        }
    }
}
