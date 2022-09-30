using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_BubbleDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text _content;
    private UnityAction<UI_BubbleDialog> onHide;
    
    [Space]
    public Camera worldCamera;
    public Camera canvasCamera;
    public RectTransform canvasRectTransform;

    public bool Used { get; private set; } = false;
    public Transform Target { get; private set; }
    
    public UI_BubbleDialog OnInit(UnityAction<UI_BubbleDialog> callback)
    {
        _content.text = "";
        Used = false;

        onHide += callback;

        return this;
    }

    public void Show(string content,Transform target)
    {
        _content.text = content;
        Target = target;
        Used = true;

        //transform.rotation = Camera.main.transform.rotation;
        
        StartCoroutine("Follow");
    }

    private IEnumerator Follow()
    {
        while (true)
        {
            //transform.LookAt(Camera.main.transform);
            this.transform.position = WorldToScreenSpaceCamera(Target.position);
            yield return 0;
        }

        Hide();
    }

    public void Hide()
    {
        //Info 从体验上来说，关闭可以延迟一些时间而非立刻关闭
        _content.text = "";
        Used = false;
        Target = null;
        
        StopCoroutine("Follow");
        onHide?.Invoke(this);
    }
    
    private Vector3 WorldToScreenSpaceCamera(Vector3 worldPosition)
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPosition);
        /*RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, canvasCamera,
            out var localPoint);*/
        return ScreenPointToUIPoint(canvasRectTransform, screenPoint);
    }

    public Vector3 ScreenPointToUIPoint(RectTransform rt, Vector2 screenPoint)
    {
        Vector3 uiPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPoint, canvasCamera, out uiPosition);
        return uiPosition;
    }
}
