using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;

public class WorldSpaceUISystem : MonoSingletonBase<WorldSpaceUISystem>
{
    [SerializeField] private UI_BubbleDialog _prefab;
    private Queue<UI_BubbleDialog> _bubbles = new Queue<UI_BubbleDialog>();

    public UI_BubbleDialog ShowBubbleUI(string content, Transform target)
    {
        var bubble = GetBubble();
        bubble.Show(content, target);
        return bubble;
    }

    private UI_BubbleDialog GetBubble()
    {
        if(_bubbles.Count == 0)
        {
            _bubbles.Enqueue(Instantiate(_prefab, this.transform).OnInit(ReturnBubble));
        }

        var bubble = _bubbles.Dequeue();
        bubble.gameObject.SetActive(true);
        return bubble;
    }

    private void ReturnBubble(UI_BubbleDialog bubble)
    {
        _bubbles.Enqueue(bubble);
        bubble.gameObject.SetActive(false);
    }
}
