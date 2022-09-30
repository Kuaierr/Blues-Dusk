using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOnItemTrigger : AutoElementBase
{
    private UI_BubbleDialog _currentBubble = null;
    public string content = "Dialog Bubble";

    protected override void OnBubbleTriggetEnter(Transform target)
    {
        _currentBubble = WorldSpaceUISystem.current.ShowBubbleUI(content, this.transform);
    }

    protected override void OnBubbleTriggetExit()
    {
        _currentBubble.Hide();
        _currentBubble = null;
    }
}