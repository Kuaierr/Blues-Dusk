using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventTest : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("OnPointerExit");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("OnPointerClick");
	}
}
