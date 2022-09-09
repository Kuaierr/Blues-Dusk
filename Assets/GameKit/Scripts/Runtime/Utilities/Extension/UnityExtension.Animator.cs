using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace UnityGameKit.Runtime
{
    public static partial class UnityExtension
    {
        private static Animator s_animator;
        private static UnityAction onFinish;
        private static float normalizedTime;
        public static void OnComplete(this Animator animator, float checkTime = 0.9f, UnityAction callback = null)
        {
            s_animator = animator;
            onFinish = callback;
            normalizedTime = checkTime;
            MonoManager.instance.AddUpdateListener(CheckAnimatorComplete);
        }

        private static void CheckAnimatorComplete()
        {
            AnimatorStateInfo info = s_animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= normalizedTime)
            {
                onFinish?.Invoke();
                MonoManager.instance.RemoveUpdateListener(CheckAnimatorComplete);
            }
        }

        public static bool IsComplete(this Animator animator)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            // Log.Info(info.normalizedTime);
            return info.normalizedTime >= 0.9f;
        }

        public static void SetTriggerOneTime(this Animator animator, string triggerName)
        {
            animator.SetTrigger(triggerName);
            MonoManager.instance.StartCoroutine(ResetTrigger(animator, triggerName));
        }

        private static IEnumerator ResetTrigger(Animator animator, string triggerName)
        {
            yield return null;
            animator.ResetTrigger(triggerName);
        }
    }
}
