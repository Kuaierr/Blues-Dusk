using UnityEngine;
using System;
using System.Reflection;
using LubanConfig.DataTable;
using LubanConfig;
using System.Text;
public static class DataTableExtension
{
    public static string GetRawString(this DataTable dataTable, string configName)
    {
        if (configName == "TbGameConfig")
        {
            Type type = typeof(TbGameConfig);
            FieldInfo singleFieldInfo = type.GetField("_data", BindingFlags.Instance | BindingFlags.NonPublic);
            if (singleFieldInfo != null)
            {
                object value = singleFieldInfo.GetValue(dataTable.GameConfigTable);
                return value.ToString();
            }
        }
        return null;
    }
}