using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameKit;
using System.Collections.Generic;
using Febucci.UI;

public class UI_Character : MonoBehaviour
{
    public Animator animator;
    public Image avatar;
    private void Start()
    {
        animator = GetComponent<Animator>();
        avatar = GetComponent<Image>();
    }
    public void SetSpeaker(Image avatar, Animator animator)
    {
        this.avatar = avatar;
        this.animator = animator;
    }
}