using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameKit;
using System.Linq;
using UnityEngine.Events;
public class UI_DiceRollPack : UIGroup
{
    public UI_Selector selector;
    public List<UI_Dice> ui_dices = new List<UI_Dice>();
    private HorizontalLayoutGroup horizontalLayoutGroup;
    private Sequence selectorSeq;
    private RectTransform contentRectTransform;
    private Vector2 selectorInitPos = Vector2.zero;
    private Vector2 contentInitPos = Vector2.zero;
    private Vector2 contentEndPos = Vector2.zero;
    private int currentIndex = 0;
    private float animDistance = 0;
    private int offset = 0;

    public const int MaxDisplayDiceCount = 5;

    protected override void Start()
    {
        selectorSeq = DOTween.Sequence();
        ui_dices = GetComponentsInChildren<UI_Dice>(true).ToList();
        horizontalLayoutGroup = selector.transform.parent.GetComponentInChildren<HorizontalLayoutGroup>();
        // Debug.Log(ui_dices.First().rectTransform.anchoredPosition);
        selectorInitPos = selector.rectTransform.anchoredPosition = ui_dices.First().rectTransform.anchoredPosition;
        contentRectTransform = selector.transform.parent.GetComponent<RectTransform>();
        contentInitPos = contentRectTransform.anchoredPosition;
        contentEndPos = new Vector2(animDistance * MaxDisplayDiceCount, contentInitPos.y);
        animDistance = horizontalLayoutGroup.spacing + ui_dices.First().rectTransform.sizeDelta.x;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < ui_dices.Count - 1)
            {
                if (currentIndex - offset >= MaxDisplayDiceCount - 1)
                {
                    MoveContent(-1);
                    offset++;
                }
                currentIndex = currentIndex + 1;
                MoveSelector(currentIndex);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndex > 0)
            {
                if (currentIndex - offset < 1)
                {
                    MoveContent(1);
                    offset--;
                }
                currentIndex = currentIndex - 1;
                MoveSelector(currentIndex);
            }
        }
    }

    private void MoveSelector(int index)
    {
        selectorSeq.Kill();
        selectorSeq.Append(selector.rectTransform.DOAnchorPosX(selectorInitPos.x + animDistance * index, 0.1f));
    }

    private void MoveContent(int direction = 1)
    {
        selectorSeq.Kill();
        selectorSeq.Append(contentRectTransform.DOAnchorPosX(contentRectTransform.anchoredPosition.x + (animDistance * direction), 0.1f));
    }

    private void MoveContent(float posX)
    {
        selectorSeq.Kill();
        selectorSeq.Append(contentRectTransform.DOAnchorPosX(posX, 0.1f));
    }

    public void ChooseCurrent()
    {

    }

}