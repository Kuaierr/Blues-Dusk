using UnityEngine;
using GameKit;
using SimpleJSON;
using LubanConfig.DataTable;
using LubanConfig;

public class DataTable : SingletonBase<DataTable>
{
    private Tables m_DataTables;
    private TbItem m_ItemTable;
    private TbCard m_CardTable;
    private TbDice m_DiceTable;
    private TbUIConfig m_UIConfigTable;
    private TbEntityConfig m_EntityConfigTable;
    private Tables DataTables
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

    public TbUIConfig UIConfigTable
    {
        get
        {
            if (m_UIConfigTable == null)
                m_UIConfigTable = DataTables.TbUIConfig;
            return m_UIConfigTable;
        }
    }

    public TbEntityConfig EntityConfigTable
    {
        get
        {
            if (m_EntityConfigTable == null)
                m_EntityConfigTable = DataTables.TbEntityConfig;
            return m_EntityConfigTable;
        }
    }

    private JSONNode LoadByJson(string fileName)
    {
        // string jsonData = File.ReadAllText(Application.dataPath + "/LubanGen/Datas/json/" + fileName + ".json", System.Text.Encoding.UTF8);
        string runtimeJsonData = "";
        AddressableManager.instance.GetAsset<TextAsset>("Assets/LubanGen/Datas/json/" + fileName + ".json", (TextAsset data) =>
        {
            runtimeJsonData = data.text;
        });
        return JSON.Parse(runtimeJsonData);
        // m_DataTables.TbDefaultSetting.
    }

}