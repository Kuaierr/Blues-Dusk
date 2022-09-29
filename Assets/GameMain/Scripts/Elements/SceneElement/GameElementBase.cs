using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using GameKit.Setting;
using GameKit.Event;
using UnityGameKit.Runtime;


public abstract class GameElementBase : ElementBase
{
    public UnityEvent OnInteractBegin;
    public UnityEvent OnInteractAfter;
    public Transform InteractTrans;
    public Vector3 InteractPosition
    {
        get
        {
            return InteractTrans.position;
        }
    }

    public override void OnInit()
    {
        InteractTrans = transform.Find("Destination");
        gameObject.layer = LayerMask.NameToLayer("Interactive");
        GameKitCenter.Event.Subscribe(SaveSettingsEventArgs.EventId, OnSave);
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    private void OnDestroy()
    {
        GameKitCenter.Element.RemoveElement(this);
        GameKitCenter.Event.Unsubscribe(SaveSettingsEventArgs.EventId, OnSave);
        GameKitCenter.Event.Unsubscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    public virtual void OnLoad(object sender, GameEventArgs e)
    {
        bool b_active = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "Is Active"), true);
        gameObject.SetActive(b_active);
    }

    public virtual void OnSave(object sender, GameEventArgs e)
    {
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "Is Active"), gameObject.activeSelf);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        // Log.Info("Interact");
        OnInteractBegin?.Invoke();
    }

    public void EnableSetting(string settingName)
    {
        GameSettings.current.SetBool(settingName, true);
    }

    public void DisableSetting(string settingName)
    {
        GameSettings.current.SetBool(settingName, false);
    }

    public void SwitchSetting(string settingName)
    {
        bool origin = GameSettings.current.GetBool(settingName);
        GameSettings.current.SetBool(settingName, !origin);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnValidate()
    {
#if UNITY_EDITOR
        if (this.transform.Find("Destination") == null)
        {
            Debug.Log("Generate Destination");
            Object destination = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/GameMain/Elements/Utility/Destination.prefab");
            destination = UnityEditor.PrefabUtility.InstantiatePrefab(destination, this.transform);
            destination.name = "Destination";
            InteractTrans = ((GameObject)destination).transform;
        }
        else
        {
            InteractTrans = this.transform.Find("Destination");
        }
#endif
    }
}