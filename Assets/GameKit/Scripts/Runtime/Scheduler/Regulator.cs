using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GameKit.QuickCode;
using UnityGameKit.Runtime;


[DisallowMultipleComponent]
[AddComponentMenu("GameKit/GameKit Regulator")]
public class Regulator : MonoSingletonBase<Regulator>
{
    public UIPanel GetUI(string name) => UIManager.instance.GetUI(name);
    public void ShowUI(string name) => GetUI(name).Show();
    public void HideUI(string name) => GetUI(name).Hide();
    // public void SwitchSceneByDefault(string name) => Scheduler.current.SwitchSceneByDefault(name);
    // public void SwitchSceneBySwipe(string name) => Scheduler.current.SwitchSceneBySwipe(name);
    // public void SwitchSceneImmediately(string name) => Scheduler.current.SwitchScene(name);
    // public void ReloadCurrentSceneSwipe() => Scheduler.current.ReloadCurrentSceneSwipe();
    protected IEnumerator DelayedExcute(UnityAction action, float t)
    {
        yield return new WaitForSeconds(t);
        action?.Invoke();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
