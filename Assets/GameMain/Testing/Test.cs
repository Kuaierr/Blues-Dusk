using System.Linq;
using UnityEngine;
using System.Collections;
using UnityGameKit.Runtime;
using UnityGameKit.Editor;
using LitJson;
using GameKit;
using GameKit.Event;
using UnityEngine.UI;
using DG.Tweening;

public class Test : MonoBehaviour
{
    [Dialog] public string TestData;
    private void Start()
    {
        // GameKitCenter.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnReadConfigSuccess);
        // GameKitCenter.Config.ReadData(AssetUtility.GetConfigAsset("datatable_tbgameconfig"));
        // StartCoroutine(DOIT());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Log.Info(">>>>!! " + GameSettings.current.GetBool("IsDavidDead"));
            Debug.Log(TestData);
        }
    }

    // private void OnReadConfigSuccess(object sender, BaseEventArgs e)
    // {

    //     LoadConfigSuccessEventArgs args = (LoadConfigSuccessEventArgs)e;
    //     // Log.Warning(args.ConfigAssetName);
    // }

    // IEnumerator DOIT()
    // {
    //     yield return null;
    //     Log.Warning(GameKitCenter.Config.GetInt("current_day"));
    // }
}

