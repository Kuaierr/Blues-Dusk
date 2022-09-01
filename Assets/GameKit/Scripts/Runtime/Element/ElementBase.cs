using UnityEngine;
using GameKit.Setting;
using GameKit.Element;

namespace UnityGameKit.Runtime
{
    [System.Serializable]
    public abstract class ElementBase : MonoBehaviour, IElement
    {
        [SerializeField] protected int m_DataId = 0;
        public string Name
        {
            get
            {
                return string.Format("{0}-{1}", gameObject.name, gameObject.GetInstanceID());
            }
        }
        public int DataId
        {
            get
            {
                return m_DataId;
            }
        }
        public abstract void OnInit();
        public abstract void OnLoad();
        public abstract void OnSave();

        private void Start()
        {
            GameKitCenter.Element.RegisterElement(this);
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
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
