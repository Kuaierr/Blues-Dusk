using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

public class CylinderEntity : EntityBase
{
    private CylinderEntityData m_Data = null;
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        m_Data = userData as CylinderEntityData;
        Debug.Log(m_Data.EntityConfigData.ZhName);
    }
}