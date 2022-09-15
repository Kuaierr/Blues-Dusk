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
    public TextMeshProUGUI buttonText;
    private Item cachedData;
    private IStock m_CachedStock;

    public void Init(int parentDepth)
    {
        base.OnInit(parentDepth);
        this.OnHide();
    }

    public void Show(UnityAction callback = null)
    {
        base.OnShow(callback);
    }

    public void Hide(UnityAction callback = null)
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

    public void SetInventoryType(UI_BackpackType type)
    {
        if (type == UI_BackpackType.Backpack)
        {
            buttonText.text = "购买";
            interactButton.onClick.AddListener(Purchase);
        }
        else if (type == UI_BackpackType.Store)
        {
            buttonText.text = "交互";
            interactButton.onClick.AddListener(Interact);
        }
    }

    public void Purchase()
    {
        if (m_CachedStock != null)
            GameKitCenter.Event.Fire(DoPurchaseEventArgs.EventId, DoPurchaseEventArgs.Create(m_CachedStock, this));
        else
            Log.Fail("Can not purchase null item.");
    }

    private void Interact()
    {
        m_CachedStock?.OnInteract();
    }
}