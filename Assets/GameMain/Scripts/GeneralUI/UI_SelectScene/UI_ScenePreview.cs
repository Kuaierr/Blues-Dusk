using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
public class UI_ScenePreview : UIFormChildBase
{
    public Image Frame;
    public Image Picture;
    public Sprite Sprite;
    public string SceneAssetName;
    public string SceneName;
    [TextArea]
    public string SceneDesc;

    public void Show()
    {
        SetActive(true);
        Picture.sprite = Sprite;
    }

    public void Hide()
    {
        SetActive(false);
    }

    public void Selected()
    {
        SetEnable(true);
    }

    public void UnSelected()
    {
        SetEnable(false);
    }

    public void ForceSelected()
    {
        SetEnable(true, isForce: true);
    }

    public void ForceUnSelected()
    {
        SetEnable(false, isForce: true);
    }
}