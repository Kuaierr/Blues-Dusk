using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LubanConfig.DataTable;
using GameKit;

[System.Serializable]
public class CubeEntityData : EntityData
{
    [SerializeField] private string m_cubeEntityData;
    private Item m_configData;
    public CubeEntityData(int entityId, int typeId) : base(entityId, typeId)
    {
        m_configData = TableManager.instance.ItemTable.Get(typeId);
        m_cubeEntityData = "This Is Cube Entity Data";
    }

    public Item EntityConfigData
    {
        get
        {
            return m_configData;
        }
    }
}
