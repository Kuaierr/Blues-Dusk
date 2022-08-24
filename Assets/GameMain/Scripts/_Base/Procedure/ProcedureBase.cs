using GameKit.Fsm;
using GameKit.Procedure;
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
        Debug.Log($"Enter Procedure State: " + this.GetType().Name);
    }
}

