// using GameKit.Resource;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

public class ProcedureSplash : ProcedureBase
{
    public override bool UseNativeDialog
    {
        get
        {
            return true;
        }
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // TODO: 这里可以播放一个 Splash 动画
        // ...

        if (GameKitCenter.Core.EditorResourceMode)
        {
            // 编辑器模式
            Log.Info("Editor resource mode detected.");
            ChangeState<ProcedurePreload>(procedureOwner);
        }
        // else if (GameKitCenter.Resource.ResourceMode == ResourceMode.Package)
        // {
        //     // 单机模式
        //     Log.Info("Package resource mode detected.");
        //     ChangeState<ProcedureInitResources>(procedureOwner);
        // }
        // else
        // {
        //     // 可更新模式
        //     Log.Info("Updatable resource mode detected.");
        //     ChangeState<ProcedureCheckVersion>(procedureOwner);
        // }
    }
}

