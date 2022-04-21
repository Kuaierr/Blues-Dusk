using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameKit;
public class UI_DialogOption : UIForm 
{
    public TextMeshProUGUI content;
    public RectTransform rectTransform;
    public Button button;

    public override void OnStart()
    {
        content = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        button.onClick.AddListener(ConfirmOption);
    }

    private void ConfirmOption()
    {
        
    }
}