

using GameKit.Event;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

// 处于菜单
public class ProcedureMenu : ProcedureBase
{
    private bool m_StartGame = false;
    // private MenuForm m_MenuForm = null;
    private int m_CachedUid;

    public override bool UseNativeDialog
    {
        get
        {
            return false;
        }
    }

    public void StartGame()
    {
        m_StartGame = true;
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        m_StartGame = false;

        var uiFormInstance = GameKitCenter.UI.GetUIForm(m_CachedUid);
        if (uiFormInstance == null)
        {
            var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_MainMenu");
            if (uiForm != null)
                m_CachedUid = (int)uiForm;
        }
        else 
            uiFormInstance.gameObject.SetActive(true);
        
        GameMenuSystem.current.CloseGameMenuUI();
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnExit(procedureOwner, isShutdown);

        
        GameKitCenter.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        var uiFormInstance = GameKitCenter.UI.GetUIForm(m_CachedUid);
        if(uiFormInstance != null)
            uiFormInstance.gameObject.SetActive(false);
        // if (m_MenuForm != null)
        // {
        //     m_MenuForm.Close(isShutdown);
        //     m_MenuForm = null;
        // }
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_StartGame)
        {
            procedureOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, Constant.Scene.Menu);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
        OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        // m_MenuForm = (MenuForm)ne.UIForm.Logic;
    }
}

