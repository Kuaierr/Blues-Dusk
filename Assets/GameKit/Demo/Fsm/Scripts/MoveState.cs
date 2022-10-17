using UnityEngine;
using GameKit.Fsm;
using GameKit;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Demo.FsmPlayer>;

namespace UnityGameKit.Demo
{
    public class MoveState : FsmState<FsmPlayer>, IReference
    {
        private FsmPlayer user;
        public void Clear()
        {

        }

        protected override void OnEnter(FsmInterface fsmOwner)
        {
            base.OnEnter(fsmOwner);
            Debug.Log("Enter Move State.");
            user = fsmOwner.User;
        }

        protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<IdleState>(fsmOwner);
            }
        }
    }
}
