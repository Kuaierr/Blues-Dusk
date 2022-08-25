using GameKit;
using LitJson;
using System;

internal class LitJsonHelper : Utility.Json.IJsonHelper
{
    public string ToJson(object obj)
    {
        return JsonMapper.ToJson(obj);
    }

    public T ToObject<T>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }

    public object ToObject(Type objectType, string json)
    {
        // TODO: 反射为 ToObject<T>(string json)
        throw new NotSupportedException("ToObject(Type objectType, string json)");
    }
}

