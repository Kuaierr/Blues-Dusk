using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

public class SphereEntity : EntityBase
{
    private SphereEntityData m_Data = null;
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        m_Data = userData as SphereEntityData;
        Debug.Log(m_Data.EntityConfigData.ZhName);
    }
}