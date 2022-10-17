using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoElementBase : GameElementBase
{
    public override void OnInit()
    {
        base.OnInit();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble"))
        {
            OnBubbleTriggetEnter(other.transform);
            //Debug.Log("Debug: " + other.name + " Enter " + this.name);
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bubble"))
        {
            OnBubbleTriggetExit();
            //Debug.Log("Debug: " + other.name + " Exit " + this.name);
        }
    }

    protected abstract void OnBubbleTriggetEnter(Transform target);
    protected abstract void OnBubbleTriggetExit();
}