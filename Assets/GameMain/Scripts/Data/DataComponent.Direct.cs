using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using LubanConfig;
using SimpleJSON;
using GameKit;
using LubanConfig.DataTable;
using UnityGameKit.Runtime;

public partial class DataComponent : GameKitComponent
{
    private TbItem m_ItemTable;
    private TbCard m_CardTable;
    private TbUIConfig m_UIConfigTable;
    private TbGameConfig m_GameConfigTable;
    private TbSceneConfig m_SceneConfigTable;
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

    public TbUIConfig UIConfigTable
    {
        get
        {
            if (m_UIConfigTable == null)
                m_UIConfigTable = DataTables.TbUIConfig;
            return m_UIConfigTable;
        }
    }

    public TbGameConfig GameConfigTable
    {
        get
        {
            if (m_GameConfigTable == null)
                m_GameConfigTable = DataTables.TbGameConfig;
            return m_GameConfigTable;
        }
    }

    public TbSceneConfig SceneConfigTable
    {
        get
        {
            if (m_SceneConfigTable == null)
                m_SceneConfigTable = DataTables.TbSceneConfig;
            return m_SceneConfigTable;
        }
    }
}