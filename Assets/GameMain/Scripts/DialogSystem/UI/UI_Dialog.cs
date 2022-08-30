using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using GameKit.UI;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;

public class UI_Dialog : UIFormBase
{
    public UI_Character character;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI contents;
    public TextAnimatorPlayer textAnimatorPlayer;
    public UI_DialogResponse uI_DialogResponse;
    public GameObject indicator;
    public Animator speakerAnimator;
    public Animator edgeAnimator;
    private bool isTextShowing = false;

    // protected override void OnStart()
    // {
    //     base.OnStart();
    //     // panelCanvasGroup.alpha = 0;
    //     // this.gameObject.SetActive(false);
    //     animator = GetComponent<Animator>();
    // }

    // public void UpdateOptions(List<Option> options)
    // {
    //     uI_DialogResponse.UpdateOptions(options);
    // }

    // public void ShowResponse(UnityAction callback = null)
    // {
    //     uI_DialogResponse.isActive = true;
    //     uI_DialogResponse.gameObject.SetActive(true);
    //     uI_DialogResponse.Show(callback);
    // }

    // public void HideResponse(UnityAction callback = null)
    // {
    //     UnityAction hideCallback = CloseResponseUI;
    //     hideCallback += callback;
    //     uI_DialogResponse.Hide(hideCallback);
    // }

    // public int GetSelection()
    // {
    //     return uI_DialogResponse.CurIndex;
    // }

    // public override void Hide(UnityAction callback = null)
    // {
    //     // panelCanvasGroup.alpha = 0;
    //     // this.gameObject.SetActive(false);
    //     animator.SetTrigger("FadeOut");
    //     edgeAnimator.SetTrigger("FadeOut");
    //     speakerAnimator.SetTrigger("FadeOut");
    // }

    // public override void Show(UnityAction callback = null)
    // {
    //     // panelCanvasGroup.alpha = 1;
    //     // this.gameObject.SetActive(true);
    //     animator.SetTrigger("FadeIn");
    //     edgeAnimator.SetTrigger("FadeIn");
    // }

    // private void CloseResponseUI()
    // {
    //     uI_DialogResponse.isActive = false;
    //     uI_DialogResponse.gameObject.SetActive(false);
    // }

    // private void AddTyperWriterListener()
    // {
    //     textAnimatorPlayer.onTypewriterStart.AddListener(() =>
    //     {
    //         isTextShowing = true;
    //     });
    //     textAnimatorPlayer.onTextShowed.AddListener(() =>
    //     {
    //         isTextShowing = false;
    //     });
    // }


    // protected override void OnInit(object userData)
    // {
    //     base.OnInit(userData);
    //     InitUIInfo initUIInfo = (InitUIInfo)userData;
    //     uI_Backpack.SetStockInfoUI(uI_StockInfo);
    //     uI_Backpack.SetInventory((IInventory)initUIInfo.UserData);
    //     ChangeDisplayKeyCode = initUIInfo.ChangeDisplayKeyCode;
    //     ReferencePool.Release(initUIInfo);
    // }

    // protected override void OnOpen(object userData)
    // {
    //     base.OnOpen(userData);
    //     uI_Backpack.OnShow();
    //     CursorSystem.current.Disable();
    // }

    // protected override void OnClose(bool isShutdown, object userData)
    // {
    //     base.OnClose(isShutdown, userData);
    //     uI_Backpack.OnHide();
    //     CursorSystem.current.Enable();
    // }

    // protected override void OnPause()
    // {
    //     base.OnPause();
    //     uI_Backpack.OnHide();
    //     CursorSystem.current.Enable();
    // }

    // protected override void OnResume()
    // {
    //     base.OnResume();
    //     uI_Backpack.OnShow();
    //     CursorSystem.current.Disable();
    // }

    // protected override void OnRecycle()
    // {
    //     base.OnRecycle();
    // }

    // protected override void OnRefocus(object userData)
    // {
    //     base.OnRefocus(userData);
    // }

    // protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    // {
    //     base.OnUpdate(elapseSeconds, realElapseSeconds);
    //     ChangeDisplayUpdate(ChangeDisplayKeyCode);
    // }

}
