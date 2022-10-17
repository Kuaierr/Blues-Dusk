

namespace GameKit.UI
{
    internal sealed partial class UIManager : GameKitModule, IUIManager
    {
        private sealed partial class UIGroup : IUIGroup
        {
            private sealed class UIFormInfo : IReference
            {
                private IUIForm m_UIForm;
                private bool m_Paused;
                private bool m_Covered;

                public UIFormInfo()
                {
                    m_UIForm = null;
                    m_Paused = false;
                    m_Covered = false;
                }

                public IUIForm UIForm
                {
                    get
                    {
                        return m_UIForm;
                    }
                }

                public bool Paused
                {
                    get
                    {
                        return m_Paused;
                    }
                    set
                    {
                        m_Paused = value;
                        /*UnityEngine.Debug.Log("Debug: \n " +
                                              "Name: " + m_UIForm.UIFormAssetName +
                                              "\n Covered: " + m_Paused);*/
                    }
                }

                public bool Covered
                {
                    get
                    {
                        return m_Covered;
                    }
                    set
                    {
                        m_Covered = value;
                        /*UnityEngine.Debug.Log("Debug: \n " +
                                              "Name: " + m_UIForm.UIFormAssetName +
                                              "\n Covered: " + m_Covered);*/
                    }
                }

                public static UIFormInfo Create(IUIForm uiForm)
                {
                    if (uiForm == null)
                    {
                        throw new GameKitException("UI form is invalid.");
                    }

                    UIFormInfo uiFormInfo = ReferencePool.Acquire<UIFormInfo>();
                    uiFormInfo.m_UIForm = uiForm;
                    uiFormInfo.m_Paused = false;
                    uiFormInfo.m_Covered = true;
                    return uiFormInfo;
                }

                public void Clear()
                {
                    m_UIForm = null;
                    m_Paused = false;
                    m_Covered = false;
                }

                public override string ToString()
                {
                    return "UI_Form: " + m_UIForm.UIFormAssetName + "\n" +
                           "Paused:" + Paused + "\n" +
                           "Covered: " + m_Covered;
                }
            }
        }
    }
}
