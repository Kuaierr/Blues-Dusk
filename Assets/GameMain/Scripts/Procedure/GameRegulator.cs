using UnityEngine;
using GameKit;
public class GameRegulator : Regulator
{
    private InteractiveElement m_CachedInteractiveElement;
    private void Update()
    {
        InteractiveElement interactive = CursorSystem.current.GetHitComponent<InteractiveElement>(1 << LayerMask.NameToLayer("Interactive"));
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