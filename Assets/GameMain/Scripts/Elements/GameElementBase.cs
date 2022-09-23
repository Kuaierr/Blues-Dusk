using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using GameKit.Setting;
using GameKit.Event;
using UnityGameKit.Runtime;


public abstract class GameElementBase : ElementBase
{
    public UnityEvent InteractCallback;
    private Transform m_InteractTrans;
    public Vector3 InteractPosition
    {
        get
        {
            return m_InteractTrans.position;
        }
    }

    public override void OnInit()
    {
        m_InteractTrans = transform.Find("Destination");
        gameObject.layer = LayerMask.NameToLayer("Interactive");
        GameKitCenter.Event.Subscribe(SaveSettingsEventArgs.EventId, OnSave);
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    public virtual void OnLoad(object sender, GameEventArgs e)
    {
        if (this == null)
            return;
        bool b_active = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "Is Active"), true);
        gameObject.SetActive(b_active);
    }

    public virtual void OnSave(object sender, GameEventArgs e)
    {
        if (this == null)
            return;
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "Is Active"), gameObject.activeSelf);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        // Log.Info("Interact");
        InteractCallback?.Invoke();
    }

    protected virtual void OnValidate() 
    {
        if(this.transform.Find("Destination") == null)
        {
            Debug.Log("Generate Destination");
            Object destination = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/GameMain/Elements/Utility/Destination.prefab");
            destination = UnityEditor.PrefabUtility.InstantiatePrefab(destination, this.transform);
            destination.name = "Destination";
        }
    }
}