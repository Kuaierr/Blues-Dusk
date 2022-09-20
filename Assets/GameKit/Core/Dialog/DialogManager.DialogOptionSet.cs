using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameKit;
using GameKit.DataNode;
using GameKit.DataStructure;
using GameKit.Inventory;
using UnityEngine;


namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        public sealed partial class DialogOptionSet : IDialogOptionSet, IReference
        {
            private List<IDialogOption> m_Options;
            private int m_CurrentIndex = 0;
            public List<IDialogOption> Options
            {
                get
                {
                    return m_Options;
                }
            }

            public DialogOptionSet()
            {
                m_CurrentIndex = 0;
                m_Options = new List<IDialogOption>();
            }

            public static DialogOptionSet Create(List<IDataNode> nodes)
            {
                DialogOptionSet dialogOptions = ReferencePool.Acquire<DialogOptionSet>();
                for (int i = 0; i < nodes.Count; i++)
                {
                    IDataNode dialogNode = nodes[i] as IDataNode;
                    DialogDataNodeVariable data = dialogNode.GetData<DialogDataNodeVariable>();
                    string contents = data.Contents;
                    
                    if (data.IsDiceCheckOption)
                        dialogOptions.CreateOption(i, contents, true, data.DiceConditions);
                    else if(data.IsDiceDefaultOption)
                        dialogOptions.CreateOption(i, contents, false);
                    else if(data.IsInventoryCheckOption)
                    {
                        bool clear = true;
                        if(data.CachedInventoryName == "DiceInventory") // 骰检
                        {
                            foreach (string condition in data.CachedStockConditions)
                            {
                                if (InventoryManager.instance.GetStockFromInventory(data.CachedInventoryName,
                                    condition) == null)
                                {
                                    clear = false;
                                    break;
                                }
                            }

                            dialogOptions.CreateOption(i, contents, clear);
                        }
                        else //仓检
                        {
                            Dictionary<string, int> result = new Dictionary<string, int>();
                            Debug.Log("Inventory Check");
                            for (int j = 0; i < data.CachedStockConditions.Count; i++)
                            {
                                if (InventoryManager.instance.GetStockFromInventory(data.CachedInventoryName,
                                    data.CachedStockConditions[i]) == null)
                                {
                                    clear = false;
                                    break;
                                }
                                
                                result.Add("result", clear? 1:0);
                                /*Debug.Log("InventoryName: " + data.CachedInventoryName +"\n" +
                                          "TargetItemName: " + data.CachedStockConditions[i] +"\n" +
                                          "Result: " + clear);*/
                            }
                            
                            dialogOptions.CreateOption(i, contents, true, result);
                        }
                    }
                    else
                        dialogOptions.CreateOption(i, contents, true);
                }
                return dialogOptions;
            }

            public static DialogOptionSet Create(IDataNode[] nodes)
            {
                DialogOptionSet dialogOptions = ReferencePool.Acquire<DialogOptionSet>();
                for (int i = 0; i < nodes.Length; i++)
                {
                    IDataNode dialogNode = nodes[i] as IDataNode;
                    DialogDataNodeVariable data = dialogNode.GetData<DialogDataNodeVariable>();
                    string contents = data.Contents;
                    
                    if (data.IsDiceCheckOption)
                        dialogOptions.CreateOption(i, contents, true, data.DiceConditions);
                    else if(data.IsDiceDefaultOption)
                        dialogOptions.CreateOption(i, contents, false);
                    else if(data.IsInventoryCheckOption)
                    {
                        bool clear = true;
                        if(data.CachedInventoryName == "DiceInventory") // 骰检
                        {
                            foreach (string condition in data.CachedStockConditions)
                            {
                                if (InventoryManager.instance.GetStockFromInventory(data.CachedInventoryName,
                                    condition) == null)
                                {
                                    clear = false;
                                    break;
                                }
                            }

                            dialogOptions.CreateOption(i, contents, clear);
                        }
                        else //仓检
                        {
                            Dictionary<string, int> result = new Dictionary<string, int>();
                            for (int j = 0; j < data.CachedStockConditions.Count; j++)
                            {
                                if (InventoryManager.instance.GetStockFromInventory(data.CachedInventoryName,
                                    data.CachedStockConditions[j]) == null)
                                {
                                    clear = false;
                                    break;
                                }
                            }
                            result.Add("result", clear? 1:0);
                            dialogOptions.CreateOption(i, contents, true, result);
                        }
                    }
                    else
                        dialogOptions.CreateOption(i, contents, true);
                }
                return dialogOptions;
            }

            private void CreateOption(int index, string text, bool canShow, Dictionary<string, int> diceConditions = null)
            {
                DialogOption newOption = DialogOption.Create(index, text, canShow, diceConditions);
                m_Options.Add(newOption);
            }

            public void Clear()
            {
                m_Options.Clear();
                m_CurrentIndex = 0;
            }

            public void Release()
            {
                for (int i = 0; i < m_Options.Count; i++)
                {
                    ReferencePool.Release(m_Options[i] as DialogOption);
                }
                ReferencePool.Release(this);
            }
        }
    }
}
