using GameKit;
using GameKit.Event;
// using GameKit.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

// 加载预制资源
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

        GameKitCenter.Scheduler.SetStartScene();
        procedureOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, GameKitCenter.Scheduler.StartScene);
        if (GameKitCenter.Scheduler.MultiScene)
        {
            procedureOwner.SetData<VarBoolean>(ProcedureStateUtility.IS_SCENE_PRELOADED, true);
            AddressableManager.instance.CachedStartScene = GameKitCenter.Scheduler.StartScene;
        }
        else
            procedureOwner.SetData<VarBoolean>(ProcedureStateUtility.IS_SCENE_PRELOADED, false);
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

    private void PreloadResources()
    {
        LoadFont("MainFont");
        LoadDialog();
        Febucci.UI.Core.TAnimBuilder.InitializeGlobalDatabase();
        if (GameKitCenter.Data.DataTables == null)
            Log.Fail("Preload resrouce fail.");
    }

    private void LoadFont(string fontName)
    {
        
    }

    private void LoadDialog()
    {
        AddressableManager.instance.GetAssetsAsyn<TextAsset>(new List<string> { "DialogPack" }, callback: (IList<TextAsset> assets) =>
        {
            for (int i = 0; i < assets.Count; i++)
            {
                string path = AssetUtility.GetDialogAsset(assets[i].name);
                GameKitCenter.Dialog.PreloadDialogAsset(assets[i].name, assets[i].text);
            }
        });
    }
}

