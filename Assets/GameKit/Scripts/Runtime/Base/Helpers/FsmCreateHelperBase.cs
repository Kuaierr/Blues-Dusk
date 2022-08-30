using GameKit.Fsm;
using UnityEngine;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    public abstract class FsmCreateHelperBase<T> : MonoBehaviour, IFsmCreateHelper<T> where T : class
    {
        public abstract void CreateFsm(ref IFsm<T> fsm, List<FsmState<T>> stateList, string fsmName, T fsmOwner);
        public abstract void StartFsm(ref IFsm<T> fsm);
        public abstract void DestroyFsm(ref IFsm<T> fsm, List<FsmState<T>> stateList);
    }
}
