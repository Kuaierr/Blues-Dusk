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

        protected override void OnEnter(FsmInterface ifsm)
        {
            base.OnEnter(ifsm);
            Debug.Log("Enter Move State.");
            user = ifsm.User;
        }

        protected override void OnUpdate(FsmInterface ifsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(ifsm, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<IdleState>(ifsm);
            }
        }
    }
}
