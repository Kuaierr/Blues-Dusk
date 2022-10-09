using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_BubbleDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text _content;
    private UnityAction<UI_BubbleDialog> onHide;
    private CanvasGroup _canvasGroup;

    [Space]
    public Camera worldCamera;
    public Camera canvasCamera;
    public RectTransform canvasRectTransform;

    public bool Used { get; private set; } = false;
    public Transform Target { get; private set; }
    
    public UI_BubbleDialog OnInit(UnityAction<UI_BubbleDialog> callback)
    {
        Used = false;
        _content.text = "";
        _canvasGroup = GetComponent<CanvasGroup>();

        onHide += callback;

        return this;
    }

    public async void Show(string content,Transform target)
    {
        _canvasGroup.DOComplete();

        Target = target;
        Used = true;
        
        StartCoroutine("TapContent", content);
        StartCoroutine("Follow");
        
        await _canvasGroup.DOFade(1, 0.5f).AsyncWaitForCompletion();
    }

    private IEnumerator TapContent(string content)
    {
        WaitForSeconds interval = new WaitForSeconds(0.1f);
        StringBuilder result = new StringBuilder("");
        foreach (char c in content)
        {
            result.Append(c);
            _content.text = result.ToString();
            yield return interval;
        }
    }

    private IEnumerator Follow()
    {
        while (true)
        {
            //transform.LookAt(Camera.main.transform);
            this.transform.position = WorldToScreenSpaceCamera(Target.position);
            yield return 0;
        }

        //Hide();
    }

    public async void Hide()
    {
        _canvasGroup.DOKill();
        await _canvasGroup.DOFade(0, 0.5f).AsyncWaitForCompletion();
        
        //Info 从体验上来说，关闭可以延迟一些时间而非立刻关闭
        _content.text = "";
        Used = false;
        Target = null;
        
        StopCoroutine("Follow");
        StopCoroutine("TapContent");
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
