using UnityEngine;
using UnityEngine.Events;
using GameKit.QuickCode;
using UnityGameKit.Runtime;
using GameKit.Inventory;
using GameKit;

public class UI_BackpackSystem : UIFormBase
{
    public UI_Backpack uI_Backpack;
    public UI_BackpackInfo uI_StockInfo;
    private KeyCode ChangeDisplayKeyCode = KeyCode.None;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        InitUIInfo initUIInfo = (InitUIInfo)userData;
        uI_Backpack.SetStockInfoUI(uI_StockInfo);
        uI_Backpack.SetInventory((IInventory)initUIInfo.UserData);
        ChangeDisplayKeyCode = initUIInfo.ChangeDisplayKeyCode;
        ReferencePool.Release(initUIInfo);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        uI_Backpack.OnShow();
        CursorSystem.current.Disable();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        uI_Backpack.OnHide();
        CursorSystem.current.Enable();
    }

    protected override void OnPause()
    {
        base.OnPause();
        uI_Backpack.OnHide();
        CursorSystem.current.Enable();
    }

    protected override void OnResume()
    {
        base.OnResume();
        uI_Backpack.OnShow();
        CursorSystem.current.Disable();
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
        ChangeDisplayUpdate(ChangeDisplayKeyCode);
    }
}