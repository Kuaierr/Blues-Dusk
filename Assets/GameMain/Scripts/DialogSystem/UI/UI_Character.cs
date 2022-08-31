using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameKit;
using System.Collections.Generic;
using Febucci.UI;
using GameKit.QuickCode;

public class UI_Character : UIFormChildBase
{
    public Animator animator;
    public Image avatar;
    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        animator = GetComponent<Animator>();
        avatar = GetComponent<Image>();
    }
    public void SetSpeaker(Image avatar, Animator animator)
    {
        this.avatar = avatar;
        this.animator = animator;
    }
}