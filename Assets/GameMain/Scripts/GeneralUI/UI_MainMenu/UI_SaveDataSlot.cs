using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_SaveDataSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int Index { get; private set; } = -1;
    public TMP_Text index;
    public TMP_Text detail;

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

    public void DeleteSaveData()
    {
        Debug.Log("Delete SaveData");
    }

    public void OnPointerEnter(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData) { OnConfirmed(); }

    public void OnPointerExit(PointerEventData eventData) { }
}