using UnityEngine;

namespace GameKit
{
    public abstract class ElementBase : MonoBehaviour, IElement
    {
        [SerializeField] protected int m_DataId = 0;
        private IElementManager elementManager;
        public int DataId
        {
            get
            {
                if (m_DataId == 0)
                    Utility.Debugger.LogFail("Element {0} has not set correct data id.", this.gameObject.name);
                return m_DataId;
            }
        }

        public void OnDestory()
        {
            if (elementManager != null)
                elementManager.RemoveElement(this);
            else
                Utility.Debugger.LogFail("ElementManage is Null");
            Destroy(this.gameObject);
        }

        public void SetManager(IElementManager manager)
        {
            elementManager = manager;
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void OnRefresh()
        {

        }

        public virtual void OnHighlightEnter()
        {

        }

        public virtual void OnHighlightExit()
        {

        }

        public virtual void OnInteract()
        {

        }
    }
}
