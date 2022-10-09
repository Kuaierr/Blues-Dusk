

using System.Collections.Generic;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;


public class ProcedureMain : ProcedureBase
{
    private ProcedureOwner m_CachedOwner;

    public override bool UseNativeDialog
    {
        get
        {
            return false;
        }
    }

    public bool GoToMenu
    {
        get;
        set;
    }

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_CachedOwner = procedureOwner;
        GoToMenu = false;
    }

    protected override void OnDestroy(ProcedureOwner procedureOwner)
    {
        base.OnDestroy(procedureOwner);
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        
        GameMenuSystem.current.OpenGameMenuUI();
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnExit(procedureOwner, isShutdown);
        
        //TODO 这里需要关闭所有游戏UI 可不可以通过Group来统一关闭呢
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        if (GoToMenu)
        {
            procedureOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, Constant.Scene.Menu);
            ChangeState<ProcedureChangeScene>(procedureOwner);  
        }
    }

    public bool SetNextSceneName(string sceneName)
    {
        if (m_CachedOwner != null)
        {
            m_CachedOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, sceneName);
            return true;
        }
        return false;
    }

    public void ExternalChangeState<T>() where T : ProcedureBase
    {
        ChangeState<T>(m_CachedOwner);
    }
}

