using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.Inventory;
using UnityEngine;
using UnityGameKit.Runtime;

public class DiceInventory : MonoSingletonBase<DiceInventory>
{
	private const string DiceInventoryName = "DiceInventory";
	private IInventory m_DiceInventory;

	public string Name => DiceInventoryName;

	private int m_CachedUiId;
	
	private void Start()
	{
		m_DiceInventory = GameKitCenter.Inventory.GetOrCreateInventory(DiceInventoryName, 60);
		GameKitCenter.Event.Subscribe(OnOpenDiceInventoryEventArgs.EventId, OnOpenDiceInventory);
		GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId,OnDiceInventoryUIOpenSuccess);
		
		//Info TestData
		var testData1 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("麦穗");
		var testData2 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("天秤");
		var testData3 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("二元辩证法");
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 001, testData1.name, testData1);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 002, testData2.name, testData2);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 003, testData3.name, testData3);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 001, testData1.name, testData1);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 002, testData2.name, testData2);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 003, testData3.name, testData3);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 001, testData1.name, testData1);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 002, testData2.name, testData2);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 003, testData3.name, testData3);
	}

	private void OnOpenDiceInventory(object sender, GameKit.Event.GameEventArgs e)
	{
		if(GameKitCenter.UI.GetUIForm(m_CachedUiId))
		{
			GameKitCenter.UI.GetUIForm(m_CachedUiId).OnResume();
		}
		else
		{
			var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_DiceInventory", this);
			if (uiForm != null)
				m_CachedUiId = (int)uiForm;
		}
	}

	private void OnDiceInventoryUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
	{
		OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
		if (args.UserData == null || args.UserData.GetType() != typeof(DiceInventory))
			return;

		if (args.UIForm == null)
		{
			Log.Fail("Loaded DiceInventory UI Form  is null");
			return;
		}

		UI_DiceInventorySystem ui = (UI_DiceInventorySystem)args.UIForm.Logic;
		ui.SetChangeDisplayKeyCode(KeyCode.Escape);
	}

	private void OnDiceInventoryUIOpenFailure(object sender, GameKit.Event.GameEventArgs e)
	{
		
	}
}
