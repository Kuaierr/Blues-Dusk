using System.Transactions;
using UnityEngine;
using System.Collections;
using GameKit.Event;
using GameKit;
using GameKit.Element;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;


public class ProcedureChangeScene : ProcedureBase
{
    private const int MenuSceneId = 1;
    private bool m_ChangeToMenu = false;
    private bool m_IsChangeSceneComplete = false;
    private int m_BackgroundMusicId = 0;
    private Player m_Prototyper;
    private bool m_IsScenePreloaded;
    private ProcedureOwner m_CachedOwner;

    public override bool UseNativeDialog
    {
        get
        {
            return false;
        }
    }

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_CachedOwner = procedureOwner;
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        
        m_IsChangeSceneComplete = false;
        
        if(procedureOwner.HasData(ProcedureStateUtility.LOAD_MAIN_MENU))
            m_ChangeToMenu = procedureOwner.GetData<VarBoolean>(ProcedureStateUtility.LOAD_MAIN_MENU);
        else 
            m_ChangeToMenu = false;
        
        // QuickCinemachineCamera.Clear();
        GameKitCenter.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        GameKitCenter.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        // 还原游戏速度
        GameKitCenter.Core.ResetNormalGameSpeed();
        

        string sceneName = procedureOwner.GetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME);
        m_IsScenePreloaded = procedureOwner.GetData<VarBoolean>(ProcedureStateUtility.IS_SCENE_PRELOADED);
        // 如果提前加载了场景（只在编辑器下可能发生）
        if (!m_IsScenePreloaded)
        {
            GameKitCenter.Event.FireNow(this, SaveSettingsEventArgs.Create(null));
            GameKitCenter.Setting.Save();
            if (GameKitCenter.Scheduler.MultiScene)
            {
                GameKitCenter.Scheduler.DoTransition(AssetUtility.GetSceneAsset(sceneName));
            }
            else
                GameKitCenter.Scheduler.LoadSceneAsyn(AssetUtility.GetSceneAsset(sceneName), onSuccess: OnSceneLoad);
        }
        else
        {
            GameKitCenter.Scheduler.AddPreloadedScene(AssetUtility.GetSceneAsset(sceneName));
            procedureOwner.SetData<VarBoolean>(ProcedureStateUtility.IS_SCENE_PRELOADED, false);
            OnSceneLoad();
        }
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameKitCenter.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        GameKitCenter.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        base.OnExit(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        
        if (!m_IsChangeSceneComplete)
        {
            return;
        }

        if (m_ChangeToMenu)
        {
            ChangeState<ProcedureMenu>(procedureOwner);
        }
        else
        {
            ChangeState<ProcedureMain>(procedureOwner);
        }
    }

    public Transform GetEnterTransform()
    {
        DoorElement element = (DoorElement)GameKitCenter.Element.GetElement(GameKitCenter.Procedure.CachedDoorName);
        if (element == null)
            return null;
        return element.EnterTranform;
    }

    public Transform GetDefaultTransform()
    {
        GameObject target = GameObject.Find("DefaultSpawnPoint");
        return target ? target.transform : null;
    }

    private void OnSceneLoad()
    {
        GameKitCenter.Element.ResetCache();
        // 加载Setting文件
        GameKitCenter.Setting.Load();
        // 广播加载事件，各实体自定义加载内容
        GameKitCenter.Event.Fire(this, LoadSettingsEventArgs.Create(null));
        // 手动加载场景配置，根据：当前天数、当天阶段，当前场景

        

        // 尝试寻找进入新场景的可用坐标，如果没找到，使用默认坐标
        Transform targetTrans = GetEnterTransform();
        if (targetTrans == null)
            targetTrans = GetDefaultTransform();

        if (targetTrans == null)
        {
            m_IsChangeSceneComplete = true;
            return;
        }
        else
        {
            // 加载玩家对象
            AddressableManager.instance.GetAssetAsyn(AssetUtility.GetElementAsset("Player_Ethan"), (GameObject obj) =>
            {
                // 注意：包含Navmesh Agent的实体必须在实例化时指定位置、方位和父对象
                GameSettings.current.LoadElementConfig(GameCenter.current.CurrentDay, GameCenter.current.CurrentStage, UnityEngine.SceneManagement.SceneManager.GetSceneAt(1).name);
                GameObject realObj = GameObject.Instantiate(obj, targetTrans.position.IgnoreY(), targetTrans.rotation, GameKitCenter.Procedure.DynamicParent);
                m_Prototyper = realObj.GetComponent<Player>();
                // 镜头跟随玩家
                QuickCinemachineCamera.current.SetFollowPlayer(m_Prototyper.CameraFollower);
                m_IsChangeSceneComplete = true;
            });
        }
    }
    private void OnLoadSceneSuccess(object sender, BaseEventArgs e)
    {
        LoadSceneSuccessEventArgs args = (LoadSceneSuccessEventArgs)e;
        if (args.UserData == null)
        {
            Log.Warning("DoTransitionCompleteEventArgs is null, procedure phase the load without transition.");
            OnSceneLoad();
            return;
        }

        DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
        if (transitionArgs.UserData == null)
        {
            Log.Warning("The transition is not fired by {0} when loading {1}", typeof(SchedulerComponent).Name, args.SceneAssetName);
            return;
        }

        if (transitionArgs.UserData.GetType() == typeof(SchedulerComponent))
        {
            if (transitionArgs.TargetCount == 0)
            {
                OnSceneLoad();
            }
        }
    }

    private void OnLoadSceneFailure(object sender, BaseEventArgs e)
    {
        LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
    }


    private void OnUnloadSceneSuccess(object sender, BaseEventArgs e)
    {
        UnloadSceneSuccessEventArgs ne = (UnloadSceneSuccessEventArgs)e;
        Log.Success("Load Scene '{0}' By Event.", ne.SceneAssetName);
    }

    private void OnUnloadSceneFailure(object sender, BaseEventArgs e)
    {
        UnloadSceneFailureEventArgs ne = (UnloadSceneFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.SceneAssetName);
    }
}

