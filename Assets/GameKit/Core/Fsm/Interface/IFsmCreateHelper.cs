using System.Collections.Generic;
namespace GameKit.Fsm
{
    public interface IFsmCreateHelper<T> where T : class
    {
        void CreateFsm(ref IFsm<T> fsm, List<FsmState<T>> stateList, string fsmName, T fsmOwner);
        void StartFsm(ref IFsm<T> fsm);
        void DestroyFsm(ref IFsm<T> fsm, List<FsmState<T>> stateList);
    }
}