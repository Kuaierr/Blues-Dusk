//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

using GameKit.Event;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;


public class ProcedureMenu : ProcedureBase
{
    private bool m_StartGame = false;
    // private MenuForm m_MenuForm = null;

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

        GameKitCenter.Event.Subscribe(ShowUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        m_StartGame = false;
        // GameKitCenter.UI.OpenUIForm(UIFormId.MenuForm, this);
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnExit(procedureOwner, isShutdown);

        GameKitCenter.Event.Unsubscribe(ShowUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

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
            procedureOwner.SetData<VarInt32>("NextSceneId", GameKitCenter.Config.GetInt("Scene.Main"));
            // procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
        ShowUIFormSuccessEventArgs ne = (ShowUIFormSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        // m_MenuForm = (MenuForm)ne.UIForm.Logic;
    }
}

