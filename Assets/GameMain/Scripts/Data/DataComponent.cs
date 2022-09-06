using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using LubanConfig;
using SimpleJSON;
using GameKit;
using UnityGameKit.Runtime;

[DisallowMultipleComponent]
[AddComponentMenu("GameKit/Data Component")]
public partial class DataComponent : GameKitComponent
{
    public List<PoolBase_SO> ScriptablePools;
    private Dictionary<Type, PoolBase_SO> m_ScriptablePoolDics;
    private Tables m_DataTables;
    private VarString[] m_CachedStringParams;
    private VarInt32[] m_CachedIntParams;
    public Dictionary<Type, PoolBase_SO> Data_So
    {
        get
        {
            if (m_ScriptablePoolDics == null)
            {
                m_ScriptablePoolDics = new Dictionary<Type, PoolBase_SO>();
                for (int i = 0; i < ScriptablePools.Count; i++)
                    m_ScriptablePoolDics.Add(ScriptablePools[i].GetType(), ScriptablePools[i]);
            }
            return m_ScriptablePoolDics;

        }
    }
    public Dictionary<string, object> Data
    {
        get
        {
            return DataTables.Table;
        }
    }

    public Tables DataTables
    {
        get
        {
            if (m_DataTables == null)
                m_DataTables = new LubanConfig.Tables(LoadByJson);
            return m_DataTables;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        m_CachedStringParams = new VarString[1];
        m_CachedIntParams = new VarInt32[1];

        if (m_ScriptablePoolDics == null)
        {
            m_ScriptablePoolDics = new Dictionary<Type, PoolBase_SO>();
            for (int i = 0; i < ScriptablePools.Count; i++)
                m_ScriptablePoolDics.Add(ScriptablePools[i].GetType(), ScriptablePools[i]);
        }
    }

    public T GetDataSO<T>() where T : PoolBase_SO
    {
        if (m_ScriptablePoolDics.ContainsKey(typeof(T)))
            return m_ScriptablePoolDics[typeof(T)] as T;
        return null;
    }

    public T GetDataTable<T>() where T : class
    {
        Type type = typeof(T);
        if (Data.ContainsKey(type.Name))
            return Data[type.Name] as T;
        Log.Fatal("Can not find datatable {0}", type.Name);
        return null;
    }

    public object GetDataTable(string tableName)
    {
        if (Data.ContainsKey(tableName))
            return Data[tableName];
        Log.Fatal("Can not find datatable {0}", tableName);
        return null;
    }

    private JSONNode LoadByJson(string fileName)
    {
        string runtimeJsonData = "";
        AddressableManager.instance.GetAsset<TextAsset>("Assets/LubanGen/Datas/json/" + fileName + ".json", (TextAsset data) =>
        {
            runtimeJsonData = data.text;
        });
        return JSON.Parse(runtimeJsonData);
    }

}