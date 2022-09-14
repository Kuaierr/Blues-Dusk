using GameKit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

public static class ProcedureExtension
{    
    public static void ChangeSceneByDoor(this ProcedureComponent procedureComponent, string sceneName, string doorName)
    {
        ProcedureMain changeScene = (ProcedureMain)procedureComponent.GetProcedure<ProcedureMain>();
        changeScene.SetNextSceneName(sceneName);
        procedureComponent.CachedDoorName = doorName;
        changeScene.ExternalChangeState<ProcedureChangeScene>();
    }

    public static void ChangeSceneBySelect(this ProcedureComponent procedureComponent, string sceneName)
    {
        ProcedureMain changeScene = (ProcedureMain)procedureComponent.GetProcedure<ProcedureMain>();
        changeScene.SetNextSceneName(sceneName);
        changeScene.ExternalChangeState<ProcedureChangeScene>();
    }
}