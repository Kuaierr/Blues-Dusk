using LitJson;
using UnityEngine;
using System;

public class KeyValuePairTest : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        GetJsonData();
    }
    
    public void GetJsonData()
    {
        string jsonStr = @"  
            {  
                ""name"" : ""alex"",  
                ""id"" : 131231232,  
                ""1"":[  
	                {  
	                    ""n1"" : ""n1"",  
	                    ""n2"" : 1
	                },  
	                {  
	                    ""n1"" : ""n11"",  
	                    ""n2"" : 2
	                }  
                ]  
            }";
        // 解析json字符串 
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        // 根据json的对象名来获取数据
        Debug.Log("name = " + (string)jsonData["name"]);
        Debug.Log("id = " + (int)jsonData["id"]);
        // 有经验的程序员都知道，就算支持也不要用纯数组做对象名！！！
        JsonData jsonData_array = jsonData["1"];
        // 遍历获取数组数据
        for (int i = 0; i < jsonData_array.Count; i++)
        {
            Debug.Log("n1 = " + jsonData_array[i]["n1"]);
            Debug.Log("n2 = " + (int)jsonData_array[i]["n2"]);
        }
    }
}
