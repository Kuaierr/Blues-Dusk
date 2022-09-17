using System.Collections.Generic;
using GameKit.DataStructure;


namespace GameKit.UI
{
    internal sealed partial class UIManager : GameKitModule, IUIManager
    {
        private sealed partial class UIGroup : IUIGroup
        {
            private readonly string m_Name;
            private int m_Depth;
            private bool m_Pause;
            private readonly IUIGroupHelper m_UIGroupHelper;
            private readonly GameKitLinkedList<UIFormInfo> m_UIFormInfos;
            private LinkedListNode<UIFormInfo> m_CachedNode;

            public UIGroup(string name, int depth, IUIGroupHelper uiGroupHelper)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameKitException("UI group name is invalid.");
                }

                if (uiGroupHelper == null)
                {
                    throw new GameKitException("UI group helper is invalid.");
                }

                m_Name = name;
                m_Pause = false;
                m_UIGroupHelper = uiGroupHelper;
                m_UIFormInfos = new GameKitLinkedList<UIFormInfo>();
                m_CachedNode = null;
                Depth = depth;
            }

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public int Depth
            {
                get
                {
                    return m_Depth;
                }
                set
                {
                    if (m_Depth == value)
                    {
                        return;
                    }

                    m_Depth = value;
                    m_UIGroupHelper.SetDepth(m_Depth);
                    Refresh();
                }
            }

            public bool Pause
            {
                get
                {
                    return m_Pause;
                }
                set
                {
                    if (m_Pause == value)
                    {
                        return;
                    }

                    m_Pause = value;
                    Refresh();
                }
            }

            public int UIFormCount
            {
                get
                {
                    return m_UIFormInfos.Count;
                }
            }

            public IUIForm CurrentUIForm
            {
                get
                {
                    return m_UIFormInfos.First != null ? m_UIFormInfos.First.Value.UIForm : null;
                }
            }

            public IUIGroupHelper Helper
            {
                get
                {
                    return m_UIGroupHelper;
                }
            }

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                LinkedListNode<UIFormInfo> current = m_UIFormInfos.First;
                while (current != null)
                {
                    if (current.Value.Paused)
                    {
                        break;
                    }

                    m_CachedNode = current.Next;
                    current.Value.UIForm.OnUpdate(elapseSeconds, realElapseSeconds);
                    current = m_CachedNode;
                    m_CachedNode = null;
                }
            }

            public bool HasUIForm(int serialId)
            {
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.SerialId == serialId)
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool HasUIForm(string uiFormAssetName)
            {
                if (string.IsNullOrEmpty(uiFormAssetName))
                {
                    throw new GameKitException("UI form asset name is invalid.");
                }

                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
                    {
                        return true;
                    }
                }

                return false;
            }

            public IUIForm GetUIForm(int serialId)
            {
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.SerialId == serialId)
                    {
                        return uiFormInfo.UIForm;
                    }
                }

                return null;
            }

            public IUIForm GetUIForm(string uiFormAssetName)
            {
                if (string.IsNullOrEmpty(uiFormAssetName))
                {
                    throw new GameKitException("UI form asset name is invalid.");
                }

                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
                    {
                        return uiFormInfo.UIForm;
                    }
                }

                return null;
            }

            public IUIForm[] GetUIForms(string uiFormAssetName)
            {
                if (string.IsNullOrEmpty(uiFormAssetName))
                {
                    throw new GameKitException("UI form asset name is invalid.");
                }

                List<IUIForm> results = new List<IUIForm>();
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
                    {
                        results.Add(uiFormInfo.UIForm);
                    }
                }

                return results.ToArray();
            }

            public void GetUIForms(string uiFormAssetName, List<IUIForm> results)
            {
                if (string.IsNullOrEmpty(uiFormAssetName))
                {
                    throw new GameKitException("UI form asset name is invalid.");
                }

                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
                    {
                        results.Add(uiFormInfo.UIForm);
                    }
                }
            }

            public IUIForm[] GetAllUIForms()
            {
                List<IUIForm> results = new List<IUIForm>();
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    results.Add(uiFormInfo.UIForm);
                }

                return results.ToArray();
            }

            public void GetAllUIForms(List<IUIForm> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    results.Add(uiFormInfo.UIForm);
                }
            }

            public void AddUIForm(IUIForm uiForm)
            {
                m_UIFormInfos.AddFirst(UIFormInfo.Create(uiForm));
            }

            public void RemoveUIForm(IUIForm uiForm)
            {
                UIFormInfo uiFormInfo = GetUIFormInfo(uiForm);
                if (uiFormInfo == null)
                {
                    throw new GameKitException(Utility.Text.Format("Can not find UI form info for serial id '{0}', UI form asset name is '{1}'.", uiForm.SerialId, uiForm.UIFormAssetName));
                }

                if (!uiFormInfo.Covered)
                {
                    uiFormInfo.Covered = true;
                    uiForm.OnCover();
                }

                if (!uiFormInfo.Paused)
                {
                    uiFormInfo.Paused = true;
                    uiForm.OnPause();
                }

                if (m_CachedNode != null && m_CachedNode.Value.UIForm == uiForm)
                {
                    m_CachedNode = m_CachedNode.Next;
                }

                if (!m_UIFormInfos.Remove(uiFormInfo))
                {
                    throw new GameKitException(Utility.Text.Format("UI group '{0}' not exists specified UI form '[{1}]{2}'.", m_Name, uiForm.SerialId, uiForm.UIFormAssetName));
                }

                ReferencePool.Release(uiFormInfo);
            }

            public void RefocusUIForm(IUIForm uiForm, object userData)
            {
                UIFormInfo uiFormInfo = GetUIFormInfo(uiForm);
                if (uiFormInfo == null)
                {
                    throw new GameKitException("Can not find UI form info.");
                }

                m_UIFormInfos.Remove(uiFormInfo);
                m_UIFormInfos.AddFirst(uiFormInfo);
            }

            public void Refresh()
            {
                LinkedListNode<UIFormInfo> current = m_UIFormInfos.First;
                bool pause = m_Pause;
                bool cover = false;
                int depth = UIFormCount;
                while (current != null && current.Value != null)
                {
                    LinkedListNode<UIFormInfo> next = current.Next;
                    current.Value.UIForm.OnDepthChanged(Depth, depth--);
                    if (current.Value == null)
                    {
                        return;
                    }

                    if (pause)
                    {
                        if (!current.Value.Covered)
                        {
                            current.Value.Covered = true;
                            current.Value.UIForm.OnCover();
                            if (current.Value == null)
                            {
                                return;
                            }
                        }

                        if (!current.Value.Paused)
                        {
                            current.Value.Paused = true;
                            current.Value.UIForm.OnPause();
                            if (current.Value == null)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (current.Value.Paused)
                        {
                            current.Value.Paused = false;
                            current.Value.UIForm.OnResume();
                            if (current.Value == null)
                            {
                                return;
                            }
                        }

                        if (current.Value.UIForm.PauseCoveredUIForm)
                        {
                            pause = true;
                        }

                        if (cover)
                        {
                            if (!current.Value.Covered)
                            {
                                current.Value.Covered = true;
                                current.Value.UIForm.OnCover();
                                if (current.Value == null)
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (current.Value.Covered)
                            {
                                current.Value.Covered = false;
                                current.Value.UIForm.OnReveal();
                                if (current.Value == null)
                                {
                                    return;
                                }
                            }

                            cover = true;
                        }
                    }

                    current = next;
                }
            }

            internal void InternalGetUIForms(string uiFormAssetName, List<IUIForm> results)
            {
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
                    {
                        results.Add(uiFormInfo.UIForm);
                    }
                }
            }

            internal void InternalGetAllUIForms(List<IUIForm> results)
            {
                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    results.Add(uiFormInfo.UIForm);
                }
            }

            private UIFormInfo GetUIFormInfo(IUIForm uiForm)
            {
                if (uiForm == null)
                {
                    throw new GameKitException("UI form is invalid.");
                }

                foreach (UIFormInfo uiFormInfo in m_UIFormInfos)
                {
                    if (uiFormInfo.UIForm == uiForm)
                    {
                        return uiFormInfo;
                    }
                }

                return null;
            }
        }
    }
}
