using UnityEngine;
using UnityEngine.UI;
using GameKit;
using LubanConfig.DataTable;

public class UI_InventoryChunk : UIForm
{
    private int m_index;
    private Animator animator;
    private Button button;
    private Item itemData;
    public RawImage icon;
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
    public void SetData(object data)
    {
        itemData = (Item)data;
        ResourceManager.instance.TryGetAsset<Texture>("Assets" + itemData.Icon, (Texture sprite) =>
        {
            icon.texture = sprite;
        });
    }

    public void OnClick()
    {

    }
}