using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using LitJson;
using GameKit;
using UnityEngine.UI;
using DG.Tweening;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Log.Info(GameKit.Utility.Assembly.GetAssemblies().Length);
        // foreach (var item in GameKit.Utility.Assembly.GetAssemblies())
        // {
            
        // }
    }
}

