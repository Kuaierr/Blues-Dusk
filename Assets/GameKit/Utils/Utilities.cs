using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;

public static class Utilities
{
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    public static string EncryptWithMD5(string source)
    {
        byte[] sor = Encoding.UTF8.GetBytes(source);
        MD5 md5 = MD5.Create();
        byte[] result = md5.ComputeHash(sor);
        StringBuilder strbul = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            strbul.Append(result[i].ToString("x2"));

        }
        return strbul.ToString();
    }

    public static string GetRandomString(int length, bool useNum = false, bool useSym = false)
    {
        byte[] bytes = new byte[4];
        rngCsp.GetBytes(bytes);
        System.Random random = new System.Random(BitConverter.ToInt32(bytes, 0));
        string str = null, strSets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (useNum == true)
        {
            strSets += "0123456789";
        }
        if (useSym == true)
        {
            strSets += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        }
        for (int i = 0; i < length; i++)
        {
            str += strSets.Substring(random.Next(0, strSets.Length - 1), 1);
        }
        return str;
    }

    public static string GetRandomID()
    {
        return GetRandomString(8, useNum: true);
    }
}