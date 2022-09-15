using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.Inventory;
using UnityEngine;
using UnityGameKit.Runtime;

public class DiceInventory : MonoSingletonBase<DiceInventory>
{
	private const string DiceInventoryName = "Dice Inventory";
	private IInventory m_DiceInventory;

	public string Name => DiceInventoryName;
	
	private void Start()
	{
		m_DiceInventory = GameKitCenter.Inventory.GetOrCreateInventory(DiceInventoryName, 60);
		GameKitCenter.Event.Subscribe(OnOpenDiceInventoryEventArgs.EventId, OnOpenDiceInventory);
		
		var testData1 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("麦穗");
		var testData2 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("天秤");
		var testData3 = GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)].GetData<UI_DiceData_SO>("二元辩证法");
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 001, testData1.name, testData1);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 002, testData2.name, testData2);
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 003, testData3.name, testData3);
	}

	private void OnOpenDiceInventory(object sender, GameKit.Event.GameEventArgs e)
	{
		GameKitCenter.UI.TryOpenUIForm("UI_DiceInventory",this);
	}

	private void OnDiceInventoryUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
	{
		//TODO 初始化背包数据
	}

	private void OnDiceInventoryUIOpenFailure(object sender, GameKit.Event.GameEventArgs e)
	{
		
	}
}
