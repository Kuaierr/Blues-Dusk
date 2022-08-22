using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;
using GameKit.QuickCode;

public class UI_DialogSystem : UIPanel
{
    public UI_Character character;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI contents;
    public TextAnimatorPlayer textAnimatorPlayer;
    public UI_DialogResponse uI_DialogResponse;
    public GameObject indicator;
    public Animator speakerAnimator;
    public Animator edgeAnimator;

    protected override void OnStart()
    {
        base.OnStart();
        // panelCanvasGroup.alpha = 0;
        // this.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void UpdateOptions(List<Option> options)
    {
        uI_DialogResponse.UpdateOptions(options);
    }

    public void ShowResponse(UnityAction callback = null)
    {
        uI_DialogResponse.isActive = true;
        uI_DialogResponse.gameObject.SetActive(true);
        uI_DialogResponse.Show(callback);
    }

    public void HideResponse(UnityAction callback = null)
    {
        UnityAction hideCallback = CloseResponseUI;
        hideCallback += callback;
        uI_DialogResponse.Hide(hideCallback);
    }

    public int GetSelection()
    {
        return uI_DialogResponse.CurIndex;
    }

    public override void Hide(UnityAction callback = null)
    {
        // panelCanvasGroup.alpha = 0;
        // this.gameObject.SetActive(false);
        animator.SetTrigger("FadeOut");
        edgeAnimator.SetTrigger("FadeOut");
        speakerAnimator.SetTrigger("FadeOut");
    }

    public override void Show(UnityAction callback = null)
    {
        // panelCanvasGroup.alpha = 1;
        // this.gameObject.SetActive(true);
        animator.SetTrigger("FadeIn");
        edgeAnimator.SetTrigger("FadeIn");
    }

    private void CloseResponseUI()
    {
        uI_DialogResponse.isActive = false;
        uI_DialogResponse.gameObject.SetActive(false);
    }
}
