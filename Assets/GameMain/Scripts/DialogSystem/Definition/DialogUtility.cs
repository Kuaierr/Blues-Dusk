using System;
using UnityEngine;

public static class DialogUtility
{
    public static Color GetDiceAttributColor(String diceAttributeName)
    {
        Color color;
        switch (diceAttributeName)
        {
            case "SWORD":

                ColorUtility.TryParseHtmlString("#1E90FF", out color); // 道奇蓝
                break;
            case "GRAIL":
                ColorUtility.TryParseHtmlString("#7FFFAA", out color); // 碧绿色
                break;
            case "STARCOIN":
                ColorUtility.TryParseHtmlString("#FFFF00", out color); // 纯黄
                break;
            case "WAND":
                ColorUtility.TryParseHtmlString("#DC143C", out color); // 猩红
                break;
            case "SPECIAL":
                ColorUtility.TryParseHtmlString("#BA55D3", out color); // 适中的兰花紫
                break;
            default:
                return Color.white;
        }
        return color;
    }

    public static Color GetDiceAttributColor(Dice_SuitType type)
    {
        return GetDiceAttributColor(System.Enum.GetName(typeof(Dice_SuitType), type));
    }
}