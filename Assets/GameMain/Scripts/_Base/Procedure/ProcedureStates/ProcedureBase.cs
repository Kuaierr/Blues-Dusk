using GameKit.Fsm;
using GameKit.Procedure;
using UnityGameKit.Runtime;
using UnityEngine;

public abstract class ProcedureBase : GameKit.Procedure.ProcedureBase
{
    public abstract bool UseNativeDialog
    {
        get;
    }
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        Log.Info($"Enter Procedure State: " + this.GetType().Name);
    }
}

