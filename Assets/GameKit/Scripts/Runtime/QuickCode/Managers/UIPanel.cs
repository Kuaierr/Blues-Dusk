using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityGameKit.Runtime;

namespace GameKit.QuickCode
{
    [RequireComponent(typeof(CanvasGroup), typeof(Animator))]
    public class UIPanel : UIBehaviour
    {
        private Dictionary<string, List<UIBehaviour>> uiComponet = new Dictionary<string, List<UIBehaviour>>();
        protected CanvasGroup panelCanvasGroup;
        protected Animator animator;
        protected bool m_isShown;
        public bool IsShown
        {
            get
            {
                return m_isShown;
            }
            protected set
            {
                m_isShown = value;
            }
        }
        protected override void Awake()
        {
            UIManager.instance.RegisterUI(this as UIPanel);
            FindChildrenByType<Button>();
            FindChildrenByType<Image>();
            FindChildrenByType<Text>();
            FindChildrenByType<Toggle>();
            FindChildrenByType<Slider>();
            FindChildrenByType<UIData>();
            FindChildrenByType<UIPanel>();
            FindChildrenByType<LayoutGroup>();
            animator = GetComponent<Animator>();
            panelCanvasGroup = GetComponent<CanvasGroup>();
        }
        protected override void Start()
        {
            OnStart();
        }

        protected virtual void OnStart() { }
        public virtual void Show(UnityAction callback = null)
        {
            if (animator != null && animator.runtimeAnimatorController != null)
            {
                animator.SetTrigger("FadeIn");
                animator.OnComplete(1f, () =>
                {
                    callback?.Invoke();
                    m_isShown = true;
                });
                return;
            }
            panelCanvasGroup.alpha = 1;
            m_isShown = true;
            callback?.Invoke();
        }

        public virtual void Hide(UnityAction callback = null)
        {
            if (animator.runtimeAnimatorController != null)
            {
                animator.SetTrigger("FadeOut");
                animator.OnComplete(1f, () =>
                {
                    callback?.Invoke();
                    m_isShown = false;
                });
                return;
            }
            panelCanvasGroup.alpha = 0;
            m_isShown = false;
            callback?.Invoke();
        }

        public T GetUIComponent<T>(string name) where T : UIBehaviour
        {
            if (uiComponet.ContainsKey(name))
            {
                for (int i = 0; i < uiComponet[name].Count; ++i)
                {
                    if (uiComponet[name][i] is T)
                    {
                        return uiComponet[name][i] as T;
                    }
                }
            }
            return null;
        }

        protected void FindChildrenByType<T>() where T : UIBehaviour
        {
            T[] components = this.GetComponentsInChildren<T>(true);
            for (int i = 0; i < components.Length; ++i)
            {
                if (components[i].transform.parent != this)
                    continue;

                if (components[i].GetType() == typeof(UIData))
                {
                    (components[i] as UIData).Group = this;
                }
                string objName = components[i].gameObject.name;
                if (uiComponet.ContainsKey(objName))
                    uiComponet[objName].Add(components[i]);
                else
                    uiComponet.Add(objName, new List<UIBehaviour>() { components[i] });
            }
        }

        protected override void OnDestroy()
        {
            UIManager.instance.RemoveUI(this);
        }

        public virtual void ChangeDisplay(KeyCode keyCode)
        {
            if (InputManager.instance.GetUiKeyDown(keyCode))
            {
                if (IsShown)
                    Hide();
                else
                    Show();
            }
        }
    }

}

