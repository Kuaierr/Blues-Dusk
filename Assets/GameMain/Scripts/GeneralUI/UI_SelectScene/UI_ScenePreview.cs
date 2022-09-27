using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_ScenePreview : UIFormChildBase
{
    public Image Frame;
    public Image Picture;
    public TextMeshProUGUI Title;
    [SerializeField] private Sprite Sprite;
    [Scene] public string SceneAssetName;
    public string SceneName;
    [TextArea]
    public string SceneDesc;

    public void Show()
    {
        SetActive(true);
        Picture.sprite = Sprite;
        Title.text = SceneName;
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