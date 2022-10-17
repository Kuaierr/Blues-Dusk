using GameKit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

public static class ProcedureExtension
{ 
    public static void ChangeSceneByDoor(this ProcedureComponent procedureComponent, string sceneName, string doorName, SceneTransitionType type = SceneTransitionType.Fade)
    {
        procedureComponent.SetData<VarBoolean>(ProcedureStateUtility.LOAD_MAIN_MENU,sceneName=="S_Menu_Main");
        
        ProcedureMain changeScene = (ProcedureMain)procedureComponent.GetProcedure<ProcedureMain>();
        changeScene.SetNextSceneInfo(sceneName,type);
        procedureComponent.CachedDoorName = doorName;
        changeScene.ExternalChangeState<ProcedureChangeScene>();
    }

    public static void ChangeSceneBySelect(this ProcedureComponent procedureComponent, string sceneName, SceneTransitionType type = SceneTransitionType.LoadingScene)
    {
        procedureComponent.SetData<VarBoolean>(ProcedureStateUtility.LOAD_MAIN_MENU,sceneName=="S_Menu_Main");
        
        ProcedureMain changeScene = (ProcedureMain)procedureComponent.GetProcedure<ProcedureMain>();
        changeScene.SetNextSceneInfo(sceneName,type);
        changeScene.ExternalChangeState<ProcedureChangeScene>();
    }
}