using UnityEngine;
using GameKit;
using GameKit.Element;

namespace UnityGameKit.Runtime
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
            element.SetManager(elementManager);
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
