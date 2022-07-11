using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;

public class UI_DialogSystem : UIGroup
{
    public UI_Character character;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI contents;
    public TextAnimatorPlayer textAnimatorPlayer;
    public UI_DialogResponse uI_DialogResponse;
    public GameObject indicator;
    protected override void OnStart()
    {
        base.OnStart();
        this.Hide();
    }

    public void UpdateOptions(List<Option> options)
    {
        uI_DialogResponse.UpdateOptions(options);
    }

    public void ShowResponse()
    {
        uI_DialogResponse.gameObject.SetActive(true);
        uI_DialogResponse.isActive = true;
        uI_DialogResponse.Show();
    }

    public void HideResponse()
    {
        uI_DialogResponse.Hide();
        uI_DialogResponse.isActive = false;
        uI_DialogResponse.gameObject.SetActive(false);
    }

    public int GetSelection()
    {
        return uI_DialogResponse.CurIndex;
    }

    public override void Hide(UnityAction callback = null)
    {
        panelCanvasGroup.alpha = 0;
        this.gameObject.SetActive(false);
    }

    public override void Show(UnityAction callback = null)
    {
        panelCanvasGroup.alpha = 1;
        this.gameObject.SetActive(true);
    }
}
