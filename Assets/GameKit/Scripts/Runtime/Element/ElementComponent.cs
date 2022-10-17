using System.Collections.Generic;
using System;
using System.Net.Sockets;
using UnityEngine;
using GameKit;
using GameKit.Setting;
using GameKit.Element;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit Element Component")]
    public class ElementComponent : GameKitComponent
    {
        private IElement m_CachedInteractiveElement;
        private IElementManager m_ElementManager;
        private ISettingManager m_SettingManager;
        public IElement CurrentElement
        {
            get
            {
                return m_CachedInteractiveElement;
            }
        }

        public int ElementCount
        {
            get
            {
                return m_ElementManager.ElementCount;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_ElementManager = GameKitModuleCenter.GetModule<IElementManager>();
            m_SettingManager = GameKitModuleCenter.GetModule<ISettingManager>();
            if (m_ElementManager == null)
            {
                Utility.Debugger.LogFail("Element Manager is invalid.");
                return;
            }
            if (m_SettingManager == null)
            {
                Utility.Debugger.LogFail("Setting Manager is invalid.");
                return;
            }
        }

        public void RegisterElement(IElement element)
        {
            m_ElementManager.RegisterElement(element);
            element.OnInit();
        }

        public void RemoveElement(IElement element)
        {
            m_ElementManager.RemoveElement(element);
        }

        public IElement GetElement(string name)
        {
            return m_ElementManager.GetElement(name);
        }

        public IElement[] GetAllElements()
        {
            return m_ElementManager.GetAllElements();
        }

        public void HighLightAll()
        {
            m_ElementManager.HighlightAll();
        }

        public void StopHightLightAll()
        {
            m_ElementManager.StopHighlightAll();
        }

        public void LoadAll()
        {
            m_SettingManager.Load();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Q))
            //     LoadAll();
            // if (Input.GetKeyDown(KeyCode.E))
            //     SaveAll();

            if (Input.GetMouseButton(1))
                HighLightAll();
            else if (Input.GetMouseButtonUp(1))
                StopHightLightAll();

            if (CursorSystem.current == null)
                return;

            IElement interactive = CursorSystem.current.GetHitComponent<IElement>(1 << LayerMask.NameToLayer("Interactive"));
            if (interactive != null)
            {
                interactive?.OnHighlightEnter();
                if (m_CachedInteractiveElement != interactive)
                    m_CachedInteractiveElement = interactive;
            }
            else
            {
                if (m_CachedInteractiveElement != null)
                {
                    m_CachedInteractiveElement?.OnHighlightExit();
                    m_CachedInteractiveElement = null;
                }
            }
        }

        public void ResetCache()
        {
            m_CachedInteractiveElement = null;
        }
    }
}
