using TMPro;
using UnityEngine.UI;
using GameKit;
using System.Collections.Generic;

public class UI_DialogSystem : UIGroup
{
    public Image avatar;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI contents;
    public UI_DialogResponse uI_DialogResponse;
    private void Update()
    {
        
    }
    public void UpdateOptions(List<Option> options)
    {
        uI_DialogResponse.UpdateOptions(options);
    }

    public void ShowResponse()
    {
        uI_DialogResponse.gameObject.SetActive(true);
        uI_DialogResponse.isActive = true;
        uI_DialogResponse.Show();
    }

    public void HideResponse()
    {
        uI_DialogResponse.Hide();
        uI_DialogResponse.isActive = true;
        uI_DialogResponse.gameObject.SetActive(false);
    }

    public int GetSelection()
    {
        return uI_DialogResponse.CurIndex;
    }
}
