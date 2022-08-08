using UnityEngine;
using System.IO;
using GameKit;
using SimpleJSON;
using LubanConfig.DataTable;
using LubanConfig;

public class TableManager : SingletonBase<TableManager>
{
    private Tables m_DataTables;
    private TbItem m_ItemTable;
    private TbCard m_CardTable;
    private TbDice m_DiceTable;
    public Tables DataTables
    {
        get
        {
            if (m_DataTables == null)
                m_DataTables = new LubanConfig.Tables(LoadByJson);
            return m_DataTables;
        }
    }
    public TbItem ItemTable
    {
        get
        {
            if (m_ItemTable == null)
                m_ItemTable = DataTables.TbItem;
            return m_ItemTable;
        }
    }

    public TbCard CardTable
    {
        get
        {
            if (m_CardTable == null)
                m_CardTable = DataTables.TbCard;
            return m_CardTable;
        }
    }

    public TbDice DiceTable
    {
        get
        {
            if (m_DiceTable == null)
                m_DiceTable = DataTables.TbDice;
            return m_DiceTable;
        }
    }

    private JSONNode LoadByJson(string fileName)
    {
        // string jsonData = File.ReadAllText(Application.dataPath + "/LubanGen/Datas/json/" + fileName + ".json", System.Text.Encoding.UTF8);
        string jsonData2 = "";
        ResourceManager.instance.TryGetAsset<TextAsset>("Assets/LubanGen/Datas/json/" + fileName + ".json", (TextAsset data) =>
        {
            jsonData2 = data.text;
        });
        return JSON.Parse(jsonData2);
    }
}