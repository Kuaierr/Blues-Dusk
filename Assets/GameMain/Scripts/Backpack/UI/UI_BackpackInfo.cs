using UnityEngine;
using UnityEngine.UI;
using GameKit;
using TMPro;
using LubanConfig.DataTable;
using UnityEngine.Events;
using GameKit.QuickCode;
using GameKit.Inventory;
using UnityGameKit.Runtime;

public class UI_BackpackInfo : UIFormChildBase
{
    public Image closeUp;
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockType;
    public TextMeshProUGUI stockPrice;
    public TextMeshProUGUI stockDesc;
    public Button interactButton;
    private Item cachedData;
    private IStock m_CachedStock;

    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        this.OnHide();
    }

    public override void OnShow(UnityAction callback = null)
    {
        base.OnShow(callback);
    }

    public override void OnHide(UnityAction callback = null)
    {
        base.OnHide(callback);
    }

    public void UpdateInfo(IStock stock)
    {
        cachedData = (Item)stock.Data;
        m_CachedStock = stock;
        AddressableManager.instance.GetAsset<Sprite>("Assets" + cachedData.CloseUp, (Sprite sprite) =>
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

    private void Interact()
    {
        m_CachedStock?.OnInteract();
    }
}