using GameKit.Fsm;
using ProcedureFsm = GameKit.Fsm.IFsm<GameKit.IProcedureManager>;

namespace GameKit
{
    public abstract class ProcedureBase : FsmState<IProcedureManager>
    {
        protected internal override void OnInit(ProcedureFsm ProcedureFsm)
        {
            base.OnInit(ProcedureFsm);
        }

        protected internal override void OnEnter(ProcedureFsm ProcedureFsm)
        {
            base.OnEnter(ProcedureFsm);
        }

        protected internal override void OnUpdate(ProcedureFsm ProcedureFsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(ProcedureFsm, elapseSeconds, realElapseSeconds);
        }

        protected internal override void OnExit(ProcedureFsm ProcedureFsm, bool isShutdown)
        {
            base.OnExit(ProcedureFsm, isShutdown);
        }

        protected internal override void OnDestroy(ProcedureFsm ProcedureFsm)
        {
            base.OnDestroy(ProcedureFsm);
        }
    }
}
