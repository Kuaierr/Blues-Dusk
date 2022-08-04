using UnityEngine;
using System.IO;
using GameKit;
using SimpleJSON;
using LubanConfig.DataTable;
using LubanConfig;
public class TableManager
{
    private static Tables m_Tables;
    public static Tables Tables
    {
        get
        {
            if (m_Tables == null)
                m_Tables = new LubanConfig.Tables(LoadByteBuf);
            return m_Tables;
        }
    }

    private static JSONNode LoadByteBuf(string fileName)
    {
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/LubanGen/Datas/json/" + fileName + ".json", System.Text.Encoding.UTF8));
    }
}