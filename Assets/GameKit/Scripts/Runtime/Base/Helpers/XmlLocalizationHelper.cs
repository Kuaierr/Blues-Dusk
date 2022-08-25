using GameKit.Localization;
using System;
using System.Xml;
using UnityGameKit.Runtime;


public class XmlLocalizationHelper : DefaultLocalizationHelper
{
    public override bool ParseData(ILocalizationManager localizationManager, string dictionaryString, object userData)
    {
        try
        {
            string currentLanguage = GameKitCenter.Localization.Language.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(dictionaryString);
            XmlNode xmlRoot = xmlDocument.SelectSingleNode("Dictionaries");
            XmlNodeList xmlNodeDictionaryList = xmlRoot.ChildNodes;
            for (int i = 0; i < xmlNodeDictionaryList.Count; i++)
            {
                XmlNode xmlNodeDictionary = xmlNodeDictionaryList.Item(i);
                if (xmlNodeDictionary.Name != "Dictionary")
                {
                    continue;
                }

                string language = xmlNodeDictionary.Attributes.GetNamedItem("Language").Value;
                if (language != currentLanguage)
                {
                    continue;
                }

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
                    if (!localizationManager.AddRawString(key, value))
                    {
                        Log.Warning("Can not add raw string with key '{0}' which may be invalid or duplicate.", key);
                        return false;
                    }
                }
            }

            return true;
        }
        catch (Exception exception)
        {
            Log.Warning("Can not parse dictionary data with exception '{0}'.", exception.ToString());
            return false;
        }
    }
}

