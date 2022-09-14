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

	private void Start()
	{
		m_DiceInventory = GameKitCenter.Inventory.GetOrCreateInventory(DiceInventoryName, 60);
		//var testData = GameKitCenter.Data.Data_So[typeof(UI_DiceData_SO)].GetData<UI_DiceData_SO>("麦穗");
		//GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>(DiceInventoryName, 001, testData.name, testData);
		
		GameKitCenter.Event.Subscribe(OnOpenDiceInventoryEventArgs.EventId, OnOpenDiceInventory);
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
