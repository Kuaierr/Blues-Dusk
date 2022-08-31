using UnityEngine;
using GameKit;
using GameKit.Element;

namespace UnityGameKit.Runtime
{
    public abstract class ElementBase : MonoBehaviour, IElement
    {
        [SerializeField] protected int m_DataId = 0;
        private IElementManager elementManager;
        public int DataId
        {
            get
            {
                return m_DataId;
            }
        }
        protected abstract void Start();

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
