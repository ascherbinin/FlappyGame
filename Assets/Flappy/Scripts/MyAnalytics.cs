using System;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine;

public class MyAnalytics
{
    private static MyAnalytics instance;
    private static string UniqueID;
    private MyAnalytics() { }

    public static MyAnalytics Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MyAnalytics();
            }
            return instance;
        }
    }

    public static void SetID (string id)
    {
        UniqueID = id;
    }

    public static void SendEvent(string name, Dictionary<string, object> dict)
    {
        dict.Add("UniqueID", UniqueID);
        Analytics.CustomEvent(name, dict);
    }
}

