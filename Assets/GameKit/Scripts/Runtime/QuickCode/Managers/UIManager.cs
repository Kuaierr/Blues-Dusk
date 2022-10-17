using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GameKit.QuickCode
{
    public class UIManager : SingletonBase<UIManager>
    {
        private Dictionary<string, UIPanel> panels = new Dictionary<string, UIPanel>();
        public void RegisterUI(UIPanel panel)
        {
            if (panels == null)
                panels = new Dictionary<string, UIPanel>();

            Debug.Log("Register UI " + panel.ToString());
            if (!panels.ContainsKey(panel.gameObject.name))
                panels.Add(panel.gameObject.name, panel);
            else
                panels[panel.gameObject.name] = panel;
        }

        public void RemoveUI(UIPanel panel)
        {
            if (panels.ContainsKey(panel.gameObject.name))
                panels.Remove(panel.gameObject.name);
        }

        public override void Clear()
        {
            if (panels == null)
                return;
            if (panels.Count > 0)
                panels.Clear();
        }
        
        public void ShowUI(string uiName, UnityAction callback = null)
        {
            if (panels.ContainsKey(uiName))
                panels[uiName].Show();
            callback?.Invoke();
        }

        public void ShowUI<T>(string uiName, UnityAction callback = null) where T : UIPanel
        {
            if (panels.ContainsKey(uiName))
                (panels[uiName] as T).Show();
            callback?.Invoke();
        }

        public void HideUI(string uiName, UnityAction callback = null)
        {
            if (panels.ContainsKey(uiName))
                panels[uiName].Hide();
            callback?.Invoke();
        }

        public void HideUI<T>(string uiName, UnityAction callback = null) where T : UIPanel
        {
            if (panels.ContainsKey(uiName))
                (panels[uiName] as T).Hide();
            callback?.Invoke();
        }

        public UIPanel GetUI(string name)
        {
            if (panels.ContainsKey(name))
                return panels[name];
            return null;
        }
        
        public T GetUI<T>(string name) where T : UIPanel
        {
            if (panels.ContainsKey(name))
                return panels[name] as T;
            return null;
        }


    }
}

