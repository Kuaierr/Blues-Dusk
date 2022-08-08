using UnityEngine;
using UnityEngine.UI;
using GameKit;
using TMPro;
using LubanConfig.DataTable;

public class UI_BackpackInfo : UIGroup
{
    public Image closeUp;
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockType;
    public TextMeshProUGUI stockPrice;
    public TextMeshProUGUI stockDesc;
    public Button interactButton;
    private Object cachedData;

    public void Interact()
    {

    }

    public void UpdateUI(Item data)
    {
        
    }
}