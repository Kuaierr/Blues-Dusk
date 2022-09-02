using GameKit.DataNode;
using System.Collections.Generic;
using GameKit.Dialog;
using GameKit;
using System.Text.RegularExpressions;
using System.Linq;
using GameKit.DataStructure;

namespace UnityGameKit.Runtime
{
    public sealed partial class TDMLDialogTreePharseHelper : DialogTreePharseHelperBase
    {
        private Queue<CommandBase> m_LinkBuffer;
        private List<IDataNode> m_DeclaredNodes;
        private List<IDataNode> m_BranchNodes;
        private IDialogTree m_CachedDialogTree;

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
            "ccomplete"
        };
        private List<string> prioritizedSemantics = new List<string>() // 该语法集合不会自动上链
        {
            "enter",
            "ebranch",
            "linkfrom"
        };

        private List<string> declareSemantics = new List<string>() // 该语法集合声明节点名称
        {
            "name",
            "sbranch",
            "ebranch",
            "cbranch"
        };

        static Regex smallBracketRegex;
        private void Awake()
        {
            m_BranchNodes = new List<IDataNode>();
            m_DeclaredNodes = new List<IDataNode>();
            m_LinkBuffer = new Queue<CommandBase>();
            smallBracketRegex = new Regex(@"\(\S+\)", RegexOptions.IgnoreCase);
        }
        public override void Phase(string rawData, IDialogTree dialogTree)
        {
            // throw new System.NotImplementedException();
            Clear();
            string[] lines = rawData.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            m_CachedDialogTree = dialogTree;
            for (int i = 0; i < lines.Length; i++)
            {
                IDataNode node = dialogTree.DataNodeManager.GetOrAddNode(i.ToString());
                PhaseNode(node, lines[i]);
                // m_DataNodeManager.SetData<DialogDataNodeVariable>(DialogDataNodeVariable.Create());
                // DialogPhaser.PhaseNode(node, line);
            }
            ExcuteAllBufferCommand();
            OnBuildEnd();
        }

        private void PhaseNode(IDataNode node, string text)
        {
            if (text.Correction() == "\n" || text.Correction() == "")
            {
                Utility.Debugger.LogWarning("[Phaser] Skip invalid node syntax.");
                return;
            }

            if (text.Substring(0, 2).Correction() == "//")
            {
                Utility.Debugger.Log($"[Phaser] Detect comments.");
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
                // node.SetData(data);
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
                Utility.Debugger.LogFail("Unsupport content symbol \':\', please replace it.");
            }

            bool customLinking = true;

            if (nodeInfo != "")
            {
                string[] parameters = nodeInfo.Split(',');

                for (int i = 0; i < parameters.Length; i++)
                {
                    string semantic = parameters[i].Split('-').FirstOrDefault().Correction();
                    string value = parameters[i].Split('-').LastOrDefault().Correction();

                    if (prioritizedSemantics.Contains(semantic))
                        customLinking = false;

                    if (!semantics.Contains(semantic))
                        Utility.Debugger.LogWarning(string.Format("Invalid semantic {0} is used.", semantic));

                    if (semantic == "mood")
                    {
                        data.MoodName = value;
                    }

                    if (semantic == "sbranch")
                    {
                        RecordBranch(node);
                        node.ChangeName(value);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "ebranch")
                    {
                        node.ChangeName(value);
                        RecordDeclaredDataNode(node);
                    }
                    else if (semantic == "name")
                    {
                        node.ChangeName(value);
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

                    if (semantic == "ccomplete")
                    {
                        data.IsCompleter = true;
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
                        data.IsDivider = true;
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
        }

        public void Clear()
        {
            m_DeclaredNodes.Clear();
            m_BranchNodes.Clear();
            m_LinkBuffer.Clear();
            m_CachedDialogTree = null;
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