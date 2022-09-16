using UnityEngine;
using GameKit.Setting;
using GameKit.Element;
using UnityGameKit.Runtime;

[System.Serializable]
public abstract class ElementBase : MonoBehaviour, IElement
{
    public string Name
    {
        get
        {
            return string.Format("{0}", gameObject.name);
        }
    }

    public abstract void OnInit();
    private void Start()
    {
        GameKitCenter.Element.RegisterElement(this);
    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnHighlightEnter()
    {

    }

    public virtual void OnHighlightExit()
    {

    }

    public virtual void OnInteract()
    {

    }

    void OnDestroy()
    {
        GameKitCenter.Element.RemoveElement(this);
    }
}

