using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using GameKit.Inventory;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Inventory Component")]
    public class InventoryComponent : GameKitComponent
    {
        [SerializeField] private string m_InventoryHelperTypeName = "DefaultInventoryHelper";
        [SerializeField] private InventoryHelperBase m_CustomInventoryHelper = null;
        private InventoryHelperBase helper;

        protected override void Awake()
        {
            base.Awake();
            helper = Helper.CreateHelper(m_InventoryHelperTypeName, m_CustomInventoryHelper);
            helper.gameObject.transform.SetParent(this.gameObject.transform);
            helper.name = "InventoryHelper";
        }

        public IStock GetStockFromInventory(string inventoryName, string stockName)
        {
            return InventoryManager.instance.GetStockFromInventory(inventoryName, stockName);
        }

        public T GetFromInventory<T>(string inventoryName, string stockName) where T : class
        {
            return InventoryManager.instance.GetFromInventory<T>(inventoryName, stockName);
        }

        public IStock GetStockFromInventory(string inventoryName, int id, string stockName)
        {
            return InventoryManager.instance.GetStockFromInventory(inventoryName, id, stockName);
        }

        public T GetFromInventory<T>(string inventoryName, int index) where T : class
        {
            return InventoryManager.instance.GetFromInventory<T>(inventoryName, index);
        }

        public IStock GetStockFromInventory(string inventoryName, int index)
        {
            return InventoryManager.instance.GetStockFromInventory(inventoryName, index);
        }


        public T GetFromInventory<T>(string inventoryName, int id, string stockName) where T : class
        {
            return InventoryManager.instance.GetFromInventory<T>(inventoryName, id, stockName);
        }


        public bool AddToInventory<T>(string inventoryName, int id, string stockName, T data) where T : class
        {
            return InventoryManager.instance.AddToInventory<T>(inventoryName, id, stockName, data);
        }
        public IInventory GetInventory(string inventoryName)
        {
            return InventoryManager.instance.GetInventory(inventoryName);
        }

        public bool CreateInventory(string inventoryName, int size)
        {
            return InventoryManager.instance.CreateInventory(inventoryName, size, helper);
        }

        public bool RemoveInventory(string inventoryName)
        {
            return InventoryManager.instance.RemoveInventory(inventoryName);
        }

        public bool HasInventory(string inventoryName)
        {
            return InventoryManager.instance.HasInventory(inventoryName);
        }

        public IInventory GetOrCreateInventory(string inventoryName, int size)
        {
            if (!HasInventory(inventoryName))
                InventoryManager.instance.CreateInventory(inventoryName, size, helper);
            return InventoryManager.instance.GetInventory(inventoryName);
        }
    }

}

