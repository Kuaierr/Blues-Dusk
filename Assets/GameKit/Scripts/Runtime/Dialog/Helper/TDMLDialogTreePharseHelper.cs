using System.Text;
using GameKit.DataNode;
using System.Collections.Generic;
using GameKit.Dialog;
using GameKit;
using System.Text.RegularExpressions;
using System.Linq;
using GameKit.DataStructure;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public sealed partial class TDMLDialogTreePharseHelper : DialogTreePharseHelperBase
    {
        private Queue<CommandBase> m_LinkBuffer;
        private List<IDataNode> m_DeclaredNodes;
        private List<IDataNode> m_BranchNodes;
        private IDialogTree m_CachedDialogTree;
        private DialogComponent m_DialogComponent;
        private string m_LastTreeName;
        private List<string> semantics = new List<string>()
        {
            "name",
            "sbranch",
            "cbranch",
            "ebranch",
            "linkfrom",
            "linkto",
            "mood",
            "cdivider",
            "ccomplete",
            "dbranch",
            "doption",
            "ddoption",
            "idoption",
            "gdivider",
            "gcomplete"
        };
        private List<string> prioritizedSemantics = new List<string>() // 该语法集合不会自动上链
        {
            "ebranch",
            "linkfrom",
            "notuplink"
        };

        private List<string> declareSemantics = new List<string>() // 该语法集合声明节点名称
        {
            "name",
            "sbranch",
            "ebranch",
            "cbranch",
            "dbranch"
        };

        static Regex smallBracketRegex;
        private void Awake()
        {
            m_BranchNodes = new List<IDataNode>();
            m_DeclaredNodes = new List<IDataNode>();
            m_LinkBuffer = new Queue<CommandBase>();
            m_DialogComponent = GameKitComponentCenter.GetComponent<DialogComponent>();
            smallBracketRegex = new Regex(@"\(\S+\)", RegexOptions.IgnoreCase);
            m_LastTreeName = string.Empty;
        }

        private void Start()
        {

        }

        public void Clear()
        {
            m_DeclaredNodes.Clear();
            m_BranchNodes.Clear();
            m_LinkBuffer.Clear();
            m_CachedDialogTree = null;
            m_LastTreeName = string.Empty;
        }

        public override void PhaseAllDialogs(string dialogAssetName, string rawData)
        {
            string[] lines = rawData.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                string nodeInfo = Regex.Match(lines[i], @"(?i)(?<=\[)(.*)(?=\])").Value.Trim(); 
                string semanic = nodeInfo.Split('-').First().Correction();
                string value = nodeInfo.Split('-').Last().Correction();

                if (semanic == "title")
                {
                    if (m_LastTreeName != string.Empty)
                    {
                        string dialogText = stringBuilder.ToString().Correction();
                        if (dialogText.Length > 0 && dialogText != string.Empty)
                        {
                            Phase(dialogText, m_DialogComponent.CreateDialogTree(string.Format("({0}){1}", dialogAssetName, m_LastTreeName)));
                        }
                    }
                    m_LastTreeName = value;
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.Append(lines[i]);
                    stringBuilder.Append("\n");
                }

                if (i == lines.Length - 1)
                {
                    if (m_LastTreeName != string.Empty)
                    {
                        string dialogText = stringBuilder.ToString().Correction();
                        if (dialogText.Length > 0 && dialogText != string.Empty)
                        {
                            Phase(dialogText, m_DialogComponent.CreateDialogTree(string.Format("({0}){1}", dialogAssetName, m_LastTreeName)));
                        }
                    }
                    m_LastTreeName = string.Empty;
                    stringBuilder.Clear();
                }
            }
        }

        public override void Phase(string rawData, IDialogTree dialogTree)
        {
            // Log.Warning(dialogTree.Name);
            Clear();
            string[] lines = rawData.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            m_CachedDialogTree = dialogTree;
            for (int i = 0; i < lines.Length; i++)
            {
                IDataNode node = dialogTree.DataNodeManager.GetOrAddNode(dialogTree.Name + i.ToString());
                PhaseNode(node, lines[i]);
            }
            ExcuteAllBufferCommand();
            OnBuildEnd();
        }

        private void PhaseNode(IDataNode node, string text)
        {
            // Log.Warning(text);
            if (text.Correction() == "\n" || text.Correction() == "")
            {
                Log.Warning("Skip invalid node syntax.");
                return;
            }

            if (text.RemoveEmptySpaceLine().Substring(0, 2).Correction() == "//")
            {
                // Log.Warning("Detect comment at '{0}'.", text);
                return;
            }

            DialogDataNodeVariable data = DialogDataNodeVariable.Create("<Default>", "<Default>");

            string nodeInfo = Regex.Match(text, @"(?i)(?<=\[)(.*)(?=\])").Value.Trim();
            string dialogInfo = text.Split(']').LastOrDefault();
            string[] split = dialogInfo.SafeSplit('：');
            if (split.Length == 2) // 普通节点
            {
                string speaker = dialogInfo.Split('：').FirstOrDefault().Trim();
                string contents = dialogInfo.Split('：').LastOrDefault().Trim();
                data.Speaker = speaker;
                data.Contents = contents;
            }
            else if (split.Length == 1)    // 对话选项节点
            {
                data.IsFunctional = true;
                data.Contents = dialogInfo;
            }
            else if (split.Length == 0) // 功能性节点
            {
                data.IsFunctional = true;
            }
            else
            {
                Log.Fatal("Unsupport content symbol \':\', please replace it.");
            }

            bool customLinking = true;

            if (nodeInfo.Correction() != "")
            {
                string[] parameters = nodeInfo.Split(',');

                for (int i = 0; i < parameters.Length; i++)
                {
                    string semantic = parameters[i].Split('-').FirstOrDefault().Correction();
                    string value = parameters[i].Split('-').LastOrDefault().Correction();

                    if (prioritizedSemantics.Contains(semantic))
                        customLinking = false;

                    if (!semantics.Contains(semantic))
                        Log.Warning(string.Format("Invalid semantic {0} is used.", semantic));

                    if (semantic == "mood")
                    {
                        data.MoodName = value;
                    }

                    if (semantic == "ebranch")
                    {
                        node.ChangeName(value);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "sbranch")
                    {
                        node.ChangeName(value);
                        RecordBranch(node);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "cbranch")
                    {
                        data.IsConditionalBranch = true;
                        node.ChangeName(value);
                        RecordBranch(node);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "dbranch")
                    {
                        data.IsDiceCheckBranch = true;
                        node.ChangeName(value);
                        RecordBranch(node);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "name")
                    {
                        node.ChangeName(value);
                        RecordBranch(node);
                        RecordDeclaredDataNode(node);
                    }

                    if (semantic == "linkfrom")
                    {
                        CachedLinkFromDeclared(node, value);
                    }
                    else if (semantic == "linkto")
                    {
                        CachedLinkToDeclared(node, value);
                    }

                    if (semantic == "gcomplete")
                    {
                        data.IsGlobalCompleter = true;
                        data.GlobalCompleteConditons = value.Trim().RemoveParentheses().Split('&').ToList();
                    }

                    if (semantic == "gdivider")
                    {
                        data.IsGlobalDivider = true;
                        data.IsFunctional = true;
                        string[] cparams = value.Split(' ');
                        if (cparams.Length < 3)
                        {
                            Utility.Debugger.LogError($"[Phaser] cdivider command require at least 3 parameters");
                            return;
                        }

                        data.GlobalDividerConditions = cparams[0].Trim().RemoveParentheses().Split('&').ToList();
                        // Utility.Debugger.Log(smallBracketRegex.Match(cparams[0]));
                        CachedLinkToDeclared(node, cparams[1]);
                        CachedLinkToDeclared(node, cparams[2]);
                    }

                    if (semantic == "ccomplete")
                    {
                        data.IsLocalCompleter = true;
                        // Utility.Debugger.Log(smallBracketRegex.Match(value));
                        data.CompleteConditons = value.Trim().RemoveParentheses().Split('&').ToList();
                        foreach (var condition in data.CompleteConditons)
                        {
                            if (!m_CachedDialogTree.LocalConditions.ContainsKey(condition))
                                m_CachedDialogTree.LocalConditions.Add(condition, false);
                        }
                    }

                    if (semantic == "cdivider")
                    {
                        data.IsLocalDivider = true;
                        data.IsFunctional = true;
                        string[] cparams = value.Split(' ');
                        if (cparams.Length < 3)
                        {
                            Utility.Debugger.LogError($"[Phaser] cdivider command require at least 3 parameters");
                            return;
                        }

                        data.DividerConditions = cparams[0].Trim().RemoveParentheses().Split('&').ToList();
                        // Utility.Debugger.Log(smallBracketRegex.Match(cparams[0]));
                        CachedLinkToDeclared(node, cparams[1]);
                        CachedLinkToDeclared(node, cparams[2]);
                    }

                    if (semantic == "doption")
                    {
                        data.IsDiceCheckOption = true;
                        data.DiceConditions = DialogDataNodeVariable.GetDefaultDiceConditions();
                        try
                        {
                            string[] diceAttributes = value.Trim().RemoveParentheses().Split(' ');
                            foreach (var diceAttribute in diceAttributes)
                            {
                                string[] paramsPair = diceAttribute.Split('=');
                                string attributeName = paramsPair[0];
                                string attributeRequirement = paramsPair[1];

                                if (data.DiceConditions.ContainsKey(attributeName) && attributeRequirement != string.Empty)
                                    data.DiceConditions[attributeName] = int.Parse(attributeRequirement);
                            }
                        }
                        catch (System.Exception e)
                        {
                            Log.Error(e.Message);
                            throw;
                        }
                    }

                    if (semantic == "ddoption")
                    {
                        data.IsDiceDefaultOption = true;
                    }

                    if (semantic == "idoption")
                    {
                        data.IsInventoryCheckOption = true;
                        try
                        {
                            //Debug.Log(data.Contents);
                            string[] diceAttributes = value.Trim().RemoveParentheses().Split(' ');
                            string inventoryName = diceAttributes[0];
                            string[] stockList = diceAttributes[1].Split('&');

                            data.CachedInventoryName = inventoryName;
                            data.CachedStockConditions.Clear();
                            for (int j = 0; j < stockList.Length; j++)
                            {
                                data.CachedStockConditions.Add(stockList[j]);
                            }
                        }
                        catch (System.Exception e)
                        {
                            Log.Error(e.Message);
                            throw;
                        }
                    }
                }

                if (customLinking)
                    AddFromLast(node);
            }
            else
            {
                AddFromLast(node);
            }
            node.SetData(data);
            m_CachedDialogTree.CurrentNode = node;
            // Log.Info(data.Speaker + ">>" + data.Contents);
        }

        public void AddFromLast(IDataNode node)
        {
            AddFrom(node, m_CachedDialogTree.CurrentNode);
        }

        public void RecordBranch(IDataNode node)
        {
            if (!m_BranchNodes.Contains(node))
                m_BranchNodes.Add(node);
        }

        public void RecordDeclaredDataNode(IDataNode node)
        {
            if (!m_DeclaredNodes.Contains(node))
                m_DeclaredNodes.Add(node);
        }

        public bool ContainsDeclaredNode(string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                    return true;
            }
            return false;
        }

        public void CachedLinkToDeclared(IDataNode srcnode, string name)
        {
            LinkCommand command = new LinkCommand(srcnode, name, LinkToDeclared);
            m_LinkBuffer.Enqueue(command);
        }

        public void CachedLinkFromDeclared(IDataNode srcnode, string name)
        {
            LinkCommand command = new LinkCommand(srcnode, name, LinkFromDeclared);
            m_LinkBuffer.Enqueue(command);
        }

        public void LinkToDeclared(IDataNode srcnode, string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                {

                    AddTo(srcnode, (node as IDataNode));
                    break;
                }
            }
        }

        public void LinkFromDeclared(IDataNode srcnode, string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                {
                    AddFrom(srcnode, (node as IDataNode));
                    break;
                }
            }
        }

        public void AddFrom(IDataNode target, IDataNode parent)
        {
            parent.AddChild(target);
        }

        public void AddTo(IDataNode target, IDataNode son)
        {
            target.AddChild(son);
        }

        public void ExcuteAllBufferCommand()
        {
            foreach (var command in m_LinkBuffer)
            {
                (command as LinkCommand).Excute();
            }
            m_LinkBuffer.Clear();
        }

        public void OnBuildEnd()
        {
            m_CachedDialogTree.Reset();
        }

    }
}