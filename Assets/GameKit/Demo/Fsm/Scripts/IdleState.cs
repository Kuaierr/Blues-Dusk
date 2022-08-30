using UnityEngine;
using GameKit.Fsm;
using GameKit;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Demo.FsmPlayer>;

namespace UnityGameKit.Demo
{
    public class IdleState : FsmState<FsmPlayer>, IReference
    {
        private FsmPlayer user;
        public void Clear()
        {

        }

        protected override void OnEnter(FsmInterface updateFsm)
        {
            base.OnEnter(updateFsm);
            Debug.Log("Enter Idle State.");
            user = updateFsm.User;
        }

        protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<MoveState>(fsmOwner);
            }
        }
    }
}
