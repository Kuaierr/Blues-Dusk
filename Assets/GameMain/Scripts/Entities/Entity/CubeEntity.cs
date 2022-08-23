using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;

public class CubeEntity : EntityBase
{
    private CubeEntityData m_Data = null;
    private Outline outline;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        
    }
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        m_Data = userData as CubeEntityData;
        outline = this.gameObject.GetOrAddComponent<Outline>();
        outline.enabled = false;
        Debug.Log(m_Data.EntityConfigData.ZhName);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

    }
}