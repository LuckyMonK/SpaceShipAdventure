using System.Globalization;
using System.IO;
using UnityEngine;

public static class DataStorageHelper
{
    public static string GetPath()
    {
        return Path.Combine(Application.streamingAssetsPath, "data.json");
    }

    public static string GetJson()
    {
        if (!File.Exists(DataStorageHelper.GetPath()))
            return "";
        var sr = new StreamReader(DataStorageHelper.GetPath());
        string result = sr.ReadToEnd();
        sr.Close();
        return result;
    }
}
