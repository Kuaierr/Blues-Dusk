using UnityEngine;

namespace GameKit
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit Element Component")]
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
