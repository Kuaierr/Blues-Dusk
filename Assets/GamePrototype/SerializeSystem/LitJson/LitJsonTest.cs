using LitJson;
using UnityEngine;
using System;

[Serializable]
public class Person
{
    public string name;
    public int id;
}
public class LitJsonTest : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        ObjectToJson();
    }
    public void ObjectToJson()
    {
        Person persion1 = new Person();
        persion1.name = "特朗普";
        persion1.id = 748;
        string jsonStr = JsonMapper.ToJson(persion1);
        Debug.Log("转换后的json字符串是：" + jsonStr);

        Person persion2 = JsonMapper.ToObject<Person>(jsonStr);
        Debug.Log("字符串转化成对象后的name数据：" + persion2.name);
    }
}
