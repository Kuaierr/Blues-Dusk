using System.Text;
using GameKit;
using GameKit.Event;
using GameKit.Inventory;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    public sealed class PurchaseSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(PurchaseSuccessEventArgs).GetHashCode();

        public PurchaseSuccessEventArgs()
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

        public static PurchaseSuccessEventArgs Create(IStock stock, object userData)
        {
            PurchaseSuccessEventArgs purchaseSuccessEventArgs = ReferencePool.Acquire<PurchaseSuccessEventArgs>();
            purchaseSuccessEventArgs.Stock = stock;
            purchaseSuccessEventArgs.UserData = userData;
            return purchaseSuccessEventArgs;
        }

        public override void Clear()
        {
            Stock = null;
            UserData = null;
        }
    }
}
