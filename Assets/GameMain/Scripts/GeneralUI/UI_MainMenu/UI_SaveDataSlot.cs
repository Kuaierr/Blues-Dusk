using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SaveDataSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int Index { get; private set; } = -1;
    public TMP_Text index;
    public TMP_Text detail;

    public Button deleteButton;
    
    private UnityAction<int> onClicked;

    public void OnInit(int index, UnityAction<int> onClickCallback)
    {
        Index = index;
        this.index.text = index.ToString("00");
        onClicked += onClickCallback;

        //TODO 检测是否存在对应存档数据
        PlayerPrefs.SetInt(UI_MainMenuSystem.CurrentSaveDataKey,index);
        if (GameKitCenter.Setting.Load())
        {
            detail.text = "Has Data";
            Debug.Log(GameKitCenter.Setting.Count);
            
            deleteButton.onClick.AddListener(DeleteCurrentData);
        }
        else
        {
            detail.text = "No Data";
        }
    }

    public void OnConfirmed()
    {
        onClicked?.Invoke(Index);
    }

    private void DeleteCurrentData()
    {
        GameKitCenter.Setting.RemoveAllSettings();
    }

    public void OnPointerEnter(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData) { OnConfirmed(); }

    public void OnPointerExit(PointerEventData eventData) { }
}