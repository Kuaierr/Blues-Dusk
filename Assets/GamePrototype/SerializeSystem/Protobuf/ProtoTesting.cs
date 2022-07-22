using Google.Protobuf;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProtoTesting : MonoBehaviour
{
    public string fileName = "sjl.txt";
    private string dirPath = Application.streamingAssetsPath + "/";
    private string filePath;
    public Button WriteBtn;
    public Button ReadBtn;

    void Start()
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        filePath = dirPath + fileName;
        WriteBtn.onClick.AddListener(WriteTo);
        ReadBtn.onClick.AddListener(ReadFrom);
    }

    /// <summary>
    /// 序列化对象并保存到本地
    /// </summary>
    public void WriteTo()
    {
        SJL sr = new SJL()
        {
            Name = "sjl",
            Age = 26,
            Height = 176
        };

        using (FileStream fs = File.OpenWrite(filePath))
        {
            byte[] bytes = sr.ToByteArray();
            fs.Write(bytes, 0, bytes.Length);
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    /// <summary>
    /// 反序列化文件
    /// </summary>
    public void ReadFrom()
    {
        using (Stream stream = File.OpenRead(filePath))
        {
            SJL sjl = SJL.Parser.ParseFrom(stream);
            Debug.LogFormat("Name:{0} Age:{1} Height:{2}", sjl.Name, sjl.Age, sjl.Height);
        }
    }
}