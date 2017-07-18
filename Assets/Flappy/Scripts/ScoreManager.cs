using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;            //A reference to our game control script so we can access it statically.
    public GameObject rewardTextPrefab;
    List<Score> Scores = new List<Score>();

    public int Score { get; set; }
    public int Modify { get; set; }

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
        Modify = 1;
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

    public void AddScore(int value, bool useModify)
    {
        var tmpValue = value;
        //The bird can't score if the game is over.
        if (GameControl.instance.gameOver)
            return;
        if (useModify)
        {
            tmpValue *= Modify;
            //For show reward text
            //Debug.Log("POS:" + _bird.transform.position);
            //Vector2 worldPos = new Vector2(_bird.transform.position.x, _bird.transform.position.y);
            //Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            //var go = Instantiate(rewardTextPrefab, new Vector2(_bird.transform.position.x, _bird.transform.position.y), Quaternion.identity);
            //go.GetComponent<RewardText>().Init(tmpValue);
            //go.transform.position = new Vector2(screenPos.x, screenPos.y);
        }
            
        Score += tmpValue;
    }

}
