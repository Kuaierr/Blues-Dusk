using UnityEngine;

namespace GameKit
{
    public class ElementComponent : GameKitComponent
    {
        private IElementManager elementManager;
        private void Start()
        {
            elementManager = GameKitModuleCenter.GetModule<IElementManager>();   
        }

        public void RegisterElement(IElement element)
        {
            elementManager.RegisterElement(element);
        }

        public void RemoveElement(IElement element)
        {
            elementManager.RemoveElement(element);
        }

        public void HighLightAll()
        {

        }

        public void HideHightLightAll()
        {
            
        }
    }
}
