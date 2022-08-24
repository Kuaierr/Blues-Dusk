using GameKit.DataTable;
using GameKit.Event;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;


public class ProcedureChangeScene : ProcedureBase
{
    private const int MenuSceneId = 1;

    private bool m_ChangeToMenu = false;
    private bool m_IsChangeSceneComplete = false;
    private int m_BackgroundMusicId = 0;

    public override bool UseNativeDialog
    {
        get
        {
            return false;
        }
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // m_IsChangeSceneComplete = false;

        // GameKitCenter.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        // GameKitCenter.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);

        // // 停止所有声音
        // GameKitCenter.Sound.StopAllLoadingSounds();
        // GameKitCenter.Sound.StopAllLoadedSounds();

        // // 隐藏所有实体
        // GameKitCenter.Entity.HideAllLoadingEntities();
        // GameKitCenter.Entity.HideAllLoadedEntities();

        // // 卸载所有场景
        // string[] loadedSceneAssetNames = GameKitCenter.Scene.GetLoadedSceneAssetNames();
        // for (int i = 0; i < loadedSceneAssetNames.Length; i++)
        // {
        //     GameKitCenter.Scene.UnloadScene(loadedSceneAssetNames[i]);
        // }

        // // 还原游戏速度
        // GameKitCenter.Base.ResetNormalGameSpeed();

        // int sceneId = procedureOwner.GetData<VarInt32>("NextSceneId");
        // m_ChangeToMenu = sceneId == MenuSceneId;
        // IDataTable<DRScene> dtScene = GameKitCenter.DataTable.GetDataTable<DRScene>();
        // DRScene drScene = dtScene.GetDataRow(sceneId);
        // if (drScene == null)
        // {
        //     Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
        //     return;
        // }

        // GameKitCenter.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, this);
        // m_BackgroundMusicId = drScene.BackgroundMusicId;
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        // GameKitCenter.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        // GameKitCenter.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        // base.OnExit(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        // base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // if (!m_IsChangeSceneComplete)
        // {
        //     return;
        // }

        // if (m_ChangeToMenu)
        // {
        //     ChangeState<ProcedureMenu>(procedureOwner);
        // }
        // else
        // {
        //     ChangeState<ProcedureMain>(procedureOwner);
        // }
    }

    private void OnLoadSceneSuccess(object sender, GameEventArgs e)
    {
        // LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
        // if (ne.UserData != this)
        // {
        //     return;
        // }

        // Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

        // if (m_BackgroundMusicId > 0)
        // {
        //     GameKitCenter.Sound.PlayMusic(m_BackgroundMusicId);
        // }

        // m_IsChangeSceneComplete = true;
    }

    private void OnLoadSceneFailure(object sender, GameEventArgs e)
    {
        // LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
        // if (ne.UserData != this)
        // {
        //     return;
        // }

        // Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
    }
}

