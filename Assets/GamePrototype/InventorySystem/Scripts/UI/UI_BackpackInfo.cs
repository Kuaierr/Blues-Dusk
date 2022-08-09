using UnityEngine;
using UnityEngine.UI;
using GameKit;
using TMPro;
using LubanConfig.DataTable;
using UnityEngine.Events;

public class UI_BackpackInfo : UIGroup
{
    public RawImage closeUp;
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockType;
    public TextMeshProUGUI stockPrice;
    public TextMeshProUGUI stockDesc;
    public Button interactButton;
    private Item cachedData;

    protected override void OnStart()
    {
        base.OnStart();
        Hide();
    }

    public void UpdateInfo(IStock stock)
    {
        cachedData = (Item)stock.Data;
        ResourceManager.instance.TryGetAsset<Texture>("Assets" + cachedData.CloseUp, (Texture texture) =>
        {
            closeUp.texture = texture;
        });
        stockName.text = cachedData.Name;
        stockType.text = cachedData.Type.ToString();
        stockPrice.text = cachedData.Price.ToString();
        stockDesc.text = cachedData.Price.ToString();
        interactButton.onClick.AddListener(stock.OnInteract);
    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
    }
}