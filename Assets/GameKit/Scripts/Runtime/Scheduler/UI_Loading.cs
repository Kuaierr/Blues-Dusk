using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
	public Text loadingPrecent;

	public void UpdateLoadingPrecent(float precent)
	{
		loadingPrecent.text = precent.ToString("0.00") + "%";
	}
}
