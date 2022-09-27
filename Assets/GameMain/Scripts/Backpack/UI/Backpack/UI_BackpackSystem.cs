using UnityEngine;
using UnityEngine.Events;
using GameKit.QuickCode;
using UnityGameKit.Runtime;
using GameKit.Inventory;
using GameKit;

public class UI_BackpackSystem : UIFormBase
{
    public UI_Backpack BackpackUI;
    public UI_BackpackInfo StockInfoUI;
    private KeyCode m_ChangeDisplayKeyCode = KeyCode.None;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        BackpackUI.SetStockInfoUI(StockInfoUI);
    }

    public void SetInventory(IInventory inventory, UI_BackpackType type)
    {
        BackpackUI.SetInventory(inventory, type);
        StockInfoUI.SetInventoryType(type);
    }

    public void SetChangeKey(KeyCode keyCode)
    {
        m_ChangeDisplayKeyCode = keyCode;
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        BackpackUI.Show();
        CursorSystem.current.Disable();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        BackpackUI.Hide();
        CursorSystem.current.Enable();
    }

    protected override void OnResume()
    {
        base.OnResume();
        BackpackUI.Show();
        CursorSystem.current.Disable();
    }

    protected override void OnPause()
    {
        base.OnPause();
        BackpackUI.Hide();
        CursorSystem.current.Enable();
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
    }

    protected override void OnRefocus(object userData)
    {
        base.OnRefocus(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (Visible)
        {
            ReOpenGameMenu(m_ChangeDisplayKeyCode);
        }
    }
}