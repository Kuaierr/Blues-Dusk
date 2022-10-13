

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

    //Info 按理来说Menu中也需要同样的方法，但是此处直接设置了静态类中的常量，具有通用性，因此从Menu的加载不需要额外处理
    public bool SetNextSceneInfo(string sceneName, SceneTransitionType type)
    {
        if (m_CachedOwner != null)
        {
            m_CachedOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, sceneName);
            ProcedureStateUtility.CurrentLoadingType = type;
            return true;
        }
        return false;
    }
    
    //Info 同上
    public void ExternalChangeState<T>() where T : ProcedureBase
    {
        ChangeState<T>(m_CachedOwner);
    }
}

