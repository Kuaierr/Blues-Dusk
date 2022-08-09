using UnityEngine;
using UnityEngine.UI;
using GameKit;
using LubanConfig.DataTable;
using UnityEngine.Events;

public class UI_BackpackChunk : UIForm
{
    private int m_index;
    private Animator animator;
    private Button button;
    private IStock m_CachedStock;
    private UI_BackpackInfo uI_Info;
    public Image icon;
    public int Index
    {
        get
        {
            return m_index;
        }
    }
    public override void OnStart()
    {
        base.OnStart();
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
    }
    public void SetIndex(int index) => m_index = index;
    public void SetData(IStock stock)
    {
        m_CachedStock = stock;
        Item data = (Item)m_CachedStock.Data;
        ResourceManager.instance.TryGetAsset<Sprite>("Assets" + data.Icon, (Sprite sprite) =>
        {
            icon.sprite = sprite;
        });
        SetButtonListener(OnClick);
    }

    public void SetButtonListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void SetStockInfoUI(UI_BackpackInfo StockInfo)
    {
        uI_Info = StockInfo;
    }

    private void OnClick()
    {
        if (uI_Info == null)
        {
            Utility.Debugger.LogError("Ui_Info reference of chunk {0} is null.", Index);
            return;
        }

        if (m_CachedStock != null)
        {
            uI_Info.UpdateInfo(m_CachedStock);
            uI_Info.Show();
        }
    }
}