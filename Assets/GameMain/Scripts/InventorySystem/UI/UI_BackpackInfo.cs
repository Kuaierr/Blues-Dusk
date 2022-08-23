using UnityEngine;
using UnityEngine.UI;
using GameKit;
using TMPro;
using LubanConfig.DataTable;
using UnityEngine.Events;
using GameKit.QuickCode;
using GameKit.Inventory;

public class UI_BackpackInfo : UIPanel
{
    public Image closeUp;
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockType;
    public TextMeshProUGUI stockPrice;
    public TextMeshProUGUI stockDesc;
    public Button interactButton;
    private Item cachedData;
    private IStock m_CachedStock;

    protected override void OnStart()
    {
        base.OnStart();
        Hide();
    }

    public void UpdateInfo(IStock stock)
    {
        cachedData = (Item)stock.Data;
        m_CachedStock = stock;
        ResourceManager.instance.GetAsset<Sprite>("Assets" + cachedData.CloseUp, (Sprite sprite) =>
        {
            closeUp.sprite = sprite;
        });
        stockName.text = cachedData.ZhName;
        stockType.text = cachedData.Type.ToString();
        stockPrice.text = cachedData.Price.ToString();
        stockDesc.text = cachedData.Desc.ToString();
        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(Interact);
    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
    }

    private void Interact()
    {
        m_CachedStock?.OnInteract();
    }
}