using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using GameKit.UI;
using GameKit.Event;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;
using UnityGameKit.Runtime;


public class UI_Tip : UIFormBase
{
    public Button Confirm;
    public Button Cancel;
    public TextMeshProUGUI ConfirmText;
    public TextMeshProUGUI CancelText;
    public TextMeshProUGUI ContentText;

    public void UpdateTip(string content, UnityAction confirmCallback, UnityAction cancelCallback, string confirm, string cancel)
    {
        Confirm.onClick.RemoveAllListeners();
        Cancel.onClick.RemoveAllListeners();
        Confirm.onClick.AddListener(Hide);
        Cancel.onClick.AddListener(Hide);
        
        if (confirm != "<None>")
            ConfirmText.text = confirm;
        if (cancel != "<None>")
            CancelText.text = cancel;
        if (content != "<None>")
            ContentText.text = content;
        if (confirmCallback != null)
            Confirm.onClick.AddListener(confirmCallback);
        if (cancelCallback != null)
            Cancel.onClick.AddListener(cancelCallback);
    }

    public void Hide()
    {
        OnPause();
    }

    public void Show()
    {
        OnResume();
    }
}
