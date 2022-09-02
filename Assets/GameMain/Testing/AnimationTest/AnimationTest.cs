using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;

[RequireComponent(typeof(Animator))]
public class AnimationTest : MonoBehaviour
{
    // Start is called before the first frame update
    public float animatorNormalizedTime;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

    }

    // Update is called once per frame
    void Update()
    {
        animatorNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Show");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Hide");
        }
    }

    public void LogNormalizedTime()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Log.Info(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}
