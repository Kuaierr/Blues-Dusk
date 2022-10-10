using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_ScenePreview : UIFormChildBase, IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public Image Frame;
    public Image Picture;
    public TextMeshProUGUI Title;
    [SerializeField] private Sprite Sprite;
    [Scene] public string SceneAssetName;
    public string SceneName;
    [TextArea]
    public string SceneDesc;

    public int Index { get; private set; } = -1;
    private UnityAction<int> onSelect;
    private UnityAction<string> OnClick;


    public UI_ScenePreview OnInit(int index, UnityAction<int> onSelectCallback, UnityAction<string> onClickCallback)
    {
        Index = index;

        onSelect += onSelectCallback;
        OnClick += onClickCallback;
        
        return this;
    }

    public void Show()
    {
        SetActive(true);
        Picture.sprite = Sprite;
        Title.text = SceneName;
    }

    public void Hide()
    {
        SetActive(false);
        onSelect = null;
        OnClick = null;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        onSelect?.Invoke(Index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(SceneAssetName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //为了和键盘同步，这里暂不设置逻辑
    }
}