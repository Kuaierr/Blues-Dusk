using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using GameKit.DataStructure;

public static class DialogPhaser
{
    public static List<string> semantics = new List<string>(){
        "pos",
        "name",
        "sbranch",
        "cbranch",
        "condition",
        "select",
        "end",
        "copy",
        "linkfrom",
        "linkto",
        "mood",
    };
    public static void PhaseTree(string text)
    {

    }
    public static void PhaseNode(Node<Dialog> node, string text)
    {
        if (text == "\n")
        {
            Debug.Log("SKIP");
            return;
        }

        DialogTree tree = (node.Tree as DialogTree);
        tree.AddFromLast(node);
        string nodeInfo = Regex.Match(text, @"(?i)(?<=\[)(.*)(?=\])").Value;
        // string streamSetting = Regex.Match(text, @"(?i)(?<=\[)(.*)(?=\])").Value;
        string dialogInfo = text.Split(']').LastOrDefault();
        string speaker = dialogInfo.Split('：').FirstOrDefault();
        string contents = dialogInfo.Split('：').LastOrDefault();

        node.nodeEntity.speaker = speaker;
        node.nodeEntity.contents = contents;

        if (nodeInfo != "")
        {
            string[] parameters = nodeInfo.Split(',');
            for (int i = 0; i < parameters.Length; i++)
            {
                string semantic = parameters[i].Split('-').FirstOrDefault().Trim();
                string value = parameters[i].Split('-').LastOrDefault().Trim();

                if (!semantics.Contains(semantic))
                {
                    Debug.LogWarning(string.Format("Invalid semantic {0} is used.", semantic));
                }

                if (semantic == "pos")
                {
                    node.nodeEntity.pos = value == "left" ? SpritePos.Left : SpritePos.Right;
                }

                if (semantic == "sbranch")
                {
                    node.IsSBranch = true;
                    tree.RecordBranch(node);
                    node.Id = value;
                    tree.RecordDeclaredNode(node);
                }
                else if (semantic == "name")
                {
                    node.Id = value;
                    tree.RecordDeclaredNode(node);
                }

                if (semantic == "select")
                {
                    tree.LinkFromDeclared(node, value);
                }
                else if (semantic == "end")
                {
                    if (value != "$quit" || semantic == value)
                        tree.LinkToDeclared(node, value);
                }

                if (semantic == "linkfrom")
                {
                    tree.LinkFromDeclared(node, value);
                }
                else if (semantic == "linkto")
                {
                    tree.LinkToDeclared(node, value);
                }
            }
        }
    }
}
