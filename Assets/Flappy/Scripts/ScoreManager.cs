using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;                      //The player's score.
    public static ScoreManager instance;            //A reference to our game control script so we can access it statically.
    List<Score> Scores = new List<Score>();

    public int Score { get; set; }

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    public void SaveScore ()
    {
        SaveLoad.SaveScore(Score);
    }

    public List<Score> GetScoresList()
    {
        SaveLoad.LoadScores();
        Scores = SaveLoad.scoresList;
        Scores.Sort((Score item1, Score item2) => item2.Value.CompareTo(item1.Value));
        return Scores;
    }

    public int GetMaxScore()
    {
        SaveLoad.LoadScores();
        var scores = SaveLoad.scoresList;
        scores.Sort((Score item1, Score item2) => item2.Value.CompareTo(item1.Value));
        if(scores.Count > 0)
        {
            return scores[0].Value;
        }
        return 0;
    }

    public void ClearScoreList()
    {
        Scores.Clear();
        SaveLoad.ClearScores();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
