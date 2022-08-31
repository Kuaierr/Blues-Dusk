using UnityEngine;
using GameKit.Element;
using UnityGameKit.Runtime;
public class GameRegulator : Regulator
{
    private IInteractive m_CachedInteractiveElement;
    private void Update()
    {
        if (CursorSystem.current == null)
            return;
            
        IInteractive interactive = CursorSystem.current.GetHitComponent<IInteractive>(1 << LayerMask.NameToLayer("Interactive"));
        if (interactive != null)
        {
            interactive.ShowOutline();
            if (m_CachedInteractiveElement != interactive)
                m_CachedInteractiveElement = interactive;
        }
        else
        {
            if (m_CachedInteractiveElement != null)
            {
                m_CachedInteractiveElement.HideOutline();
                m_CachedInteractiveElement = null;
            }
        }
    }
}