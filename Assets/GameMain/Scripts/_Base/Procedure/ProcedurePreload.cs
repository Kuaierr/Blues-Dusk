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

        GameKitCenter.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
        GameKitCenter.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
        GameKitCenter.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameKitCenter.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
        GameKitCenter.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
        GameKitCenter.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

        m_LoadedFlag.Clear();

        PreloadResources();
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameKitCenter.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
        GameKitCenter.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
        GameKitCenter.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameKitCenter.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
        GameKitCenter.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
        GameKitCenter.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

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

        // procedureOwner.SetData<VarInt32>("NextSceneId", GameKitCenter.Config.GetInt("Scene.Menu"));
        procedureOwner.SetData<VarString>("NextSceneName", "SceneMenu");
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

    private void PreloadResources()
    {
        // Preload configs
        LoadConfig("DefaultConfig");

        // Preload data tables
        // foreach (string dataTableName in DataTableNames)
        // {
        //     LoadDataTable(dataTableName);
        // }

        // Preload dictionaries
        LoadDictionary("Default");

        // Preload fonts
        LoadFont("MainFont");
    }

    private void LoadConfig(string configName)
    {
        string configAssetName = AssetUtility.GetConfigAsset(configName, false);
        m_LoadedFlag.Add(configAssetName, false);
        GameKitCenter.Config.ReadData(configAssetName, this);
    }

    private void LoadDataTable(string dataTableName)
    {
        // string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
        // m_LoadedFlag.Add(dataTableAssetName, false);
        // GameKitCenter.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
    }

    private void LoadDictionary(string dictionaryName)
    {
        string dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
        m_LoadedFlag.Add(dictionaryAssetName, false);
        // GameKitCenter.Localization.ReadData(dictionaryAssetName, this);
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

    private void OnLoadConfigSuccess(object sender, GameEventArgs e)
    {
        LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        m_LoadedFlag[ne.ConfigAssetName] = true;
        Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
    }

    private void OnLoadConfigFailure(object sender, GameEventArgs e)
    {
        LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName, ne.ConfigAssetName, ne.ErrorMessage);
    }

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        m_LoadedFlag[ne.DataTableAssetName] = true;
        Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
    }

    private void OnLoadDataTableFailure(object sender, GameEventArgs e)
    {
        LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
    }

    private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
    {
        LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        m_LoadedFlag[ne.DictionaryAssetName] = true;
        Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
    }

    private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
    {
        LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName, ne.DictionaryAssetName, ne.ErrorMessage);
    }
}

