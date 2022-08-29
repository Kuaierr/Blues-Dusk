using GameKit.DataNode;
using GameKit.Dialog;
using System;
using System.Xml;

namespace UnityGameKit.Runtime
{
    public sealed class XmlDialogTreeParseHelper : DialogTreePharseHelperBase
    {
        private IDialogTree m_DialogTree;
        private XmlDocument m_CachedXmlDocument;

        private void Awake()
        {
            m_CachedXmlDocument = new XmlDocument();
        }
        public override void Phase(string rawData, IDialogTree dialogTree)
        {
            m_CachedXmlDocument.LoadXml(rawData);
            XmlNode xmlRoot = m_CachedXmlDocument.SelectSingleNode("Plots");
            XmlNodeList xmlNodeDictionaryList = xmlRoot.ChildNodes;

            for (int i = 0; i < xmlNodeDictionaryList.Count; i++)
            {
                XmlNode xmlNodeDictionary = xmlNodeDictionaryList.Item(i);
                if (xmlNodeDictionary.Name != "Title")
                {
                    continue;
                }

                string day = xmlNodeDictionary.Attributes.GetNamedItem("Day").Value;
                

                XmlNodeList xmlNodeStringList = xmlNodeDictionary.ChildNodes;
                for (int j = 0; j < xmlNodeStringList.Count; j++)
                {
                    XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                    if (xmlNodeString.Name != "String")
                    {
                        continue;
                    }

                    string key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                    string value = xmlNodeString.Attributes.GetNamedItem("Value").Value;
                }
            }
        }
    }
}

