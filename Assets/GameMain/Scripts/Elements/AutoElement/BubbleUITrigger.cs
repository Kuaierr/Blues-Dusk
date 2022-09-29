using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleUITrigger : AutoElementBase
{
    private UI_BubbleDialog _currentBubble = null;
    public string content = "Dialog Bubble";

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player"))
            _currentBubble = WorldSpaceUISystem.current.ShowBubbleUI(content, this.transform);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.CompareTag("Player"))
        {
            _currentBubble.Hide();
            _currentBubble = null;
        }    
    }
}