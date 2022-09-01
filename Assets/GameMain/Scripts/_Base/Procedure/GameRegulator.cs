using UnityEngine;
using GameKit.Element;
using UnityGameKit.Runtime;
public class GameRegulator : Regulator
{
    private IElement m_CachedInteractiveElement;
    private void Update()
    {
        if (CursorSystem.current == null)
            return;
            
        IElement interactive = CursorSystem.current.GetHitComponent<IElement>(1 << LayerMask.NameToLayer("Interactive"));
        if (interactive != null)
        {
            interactive.OnHighlightEnter();
            if (m_CachedInteractiveElement != interactive)
                m_CachedInteractiveElement = interactive;
        }
        else
        {
            if (m_CachedInteractiveElement != null)
            {
                m_CachedInteractiveElement.OnHighlightExit();
                m_CachedInteractiveElement = null;
            }
        }
    }
}