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

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Debug: " + other.name + " Enter " + this.name);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Debug.Log("Debug: " + other.name + " Exit " + this.name);
    }
}