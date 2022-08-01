using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using cfg.item;
using SimpleJSON;
using System.IO;

public class LubanTesting : MonoBehaviour
{
    private void Start()
    {
        Tables tables = new Tables(Load);
        Item item = tables.TbItem.Get(10001);
        Debug.Log(item.Name);
    }

    private JSONNode Load(string fileName)
    {
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/LubanGen/Datas/json/" + fileName + ".json"));
    }
}
