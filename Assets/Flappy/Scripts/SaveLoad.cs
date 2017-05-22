using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad {
    public static List<Score> scoresList = new List<Score>();
    private static string filePath = Application.persistentDataPath + "/userScores.gd";

    public static void SaveScore(int pScore)
    {
        var score = new Score(pScore);
        scoresList.Add(score);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, scoresList);
        file.Close();
    }

    public static void LoadScores()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            scoresList = (List<Score>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void ClearScores()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            scoresList.Clear();
            Debug.Log("Deleted");
        }
    }
}


[System.Serializable]
public class Score
{
    public int Value { get; set; }
    public DateTime Date { get; set; }

    public Score(int val)
    {
        Value = val;
        Date = DateTime.Now;
    }
}