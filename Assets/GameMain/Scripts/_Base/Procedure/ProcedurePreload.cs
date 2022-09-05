using GameKit;
using GameKit.Event;
// using GameKit.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;


public class ProcedurePreload : ProcedureBase
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

    public override bool UseNativeDialog
    {
        get
        {
            return true;
        }
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        m_LoadedFlag.Clear();
        PreloadResources();
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnExit(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
        {
            if (!loadedFlag.Value)
            {
                return;
            }
        }
        procedureOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, "GameMenu");
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

    private void PreloadResources()
    {
        LoadFont("MainFont");
        if(GameKitCenter.Data.DataTables == null)
            Log.Fail("Preload resrouce fail.");
    }

    private void LoadFont(string fontName)
    {
        // m_LoadedFlag.Add(Utility.Text.Format("Font.{0}", fontName), false);
        // GameKitCenter.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
        //     (assetName, asset, duration, userData) =>
        //     {
        //         m_LoadedFlag[Utility.Text.Format("Font.{0}", fontName)] = true;
        //         UGuiForm.SetMainFont((Font)asset);
        //         Log.Info("Load font '{0}' OK.", fontName);
        //     },

        //     (assetName, status, errorMessage, userData) =>
        //     {
        //         Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
        //     }));
    }
}

