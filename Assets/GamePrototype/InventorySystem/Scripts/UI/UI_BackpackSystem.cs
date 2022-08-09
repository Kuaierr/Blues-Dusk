using UnityEngine;
using GameKit;
using UnityEngine.Events;

public class UI_BackpackSystem : UIGroup
{
    public UI_Backpack uI_Backpack;
    public UI_BackpackInfo uI_StockInfo;

    protected override void OnStart()
    {
        base.OnStart();
        Hide();
        if (uI_Backpack != null)
            uI_Backpack.SetStockInfoUI(uI_StockInfo);
    }


    public override void Show(UnityAction callback = null)
    {
        Debug.Log($"Show BackpackUI");
        base.Show(callback);
        uI_Backpack.Show();
    }

    public override void Hide(UnityAction callback = null)
    {
        Debug.Log($"Hide BackpackUI");
        base.Hide(callback);
        uI_Backpack.Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeDisplay(KeyCode.I);
        }
    }
}