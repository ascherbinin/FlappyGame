using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Analytics;

public enum State
{
    Play,
    Pause
}

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public GameObject ScoreText;						//A reference to the UI text component that displays the player's score.
    public State GameState;
    public Text lblButton;
    public Text txtCoinValue;
    public Text txtGemValue;
    public GameObject StartPanel;
    public GameObject GameOverCanvas;
    public GameObject HighScorePanel;
    public GameObject HelpPanel;
    public GameObject Bird;
    public bool gameOver = false;              
    public float scrollSpeed = 0f;
    public AudioSource backSound;

    public GameObject BonusManager;
    private string UniqueID;
    private ColumnPool _columnPool;
    private bool inMenu = false;

    void Awake()
	{

		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

    void Start()
    {
        ChangeUI(DifficultManager.instance.isFirstStart);
        UniqueID = SystemInfo.deviceUniqueIdentifier;
        MyAnalytics.SetID(UniqueID);
        var dictEvent = new Dictionary<string, object> { { "StartGame", true } };
        MyAnalytics.SendEvent("Start", dictEvent);
        GameState = State.Pause;
        inMenu = true;
        ScoreText.SetActive(false);
        _columnPool = GetComponent<ColumnPool>();
        SaveLoad.LoadScores();
    }

    void Update()
	{
        if (DifficultManager.instance.isFirstStart)
        {
            if (Input.anyKeyDown)
            {
                ChangeUI(!DifficultManager.instance.isFirstStart);
                DifficultManager.instance.isFirstStart = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
        {
            if (inMenu) 
                Application.Quit(); 
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartGame()
    {
        ScoreText.SetActive(true);
        GameState = State.Play;
        inMenu = false;
        EventManager.TriggerEvent("StartGame");
        StartPanel.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(false);
        Bird.gameObject.SetActive(true);
        InvokeRepeating("AddBirdScoreRepeat", 0, 1);
        backSound.Play();
    }

	public void BirdDied()
	{
        backSound.Stop();
        EventManager.TriggerEvent("GameOver");
        GameState = State.Pause;
        SoundManager.instance.PlayFailSound();
        gameOver = true;

        //Activate the game over text.
        GameOverCanvas.gameObject.SetActive(true);
        var currentScore = ScoreManager.instance.Score;
        var maxBeforeScore = ScoreManager.instance.GetMaxScore();
        if (currentScore > maxBeforeScore)
        {
            SoundManager.instance.PlayNewHighScoreSound();
            HighScorePanel.SetActive(true);
        }
        ScoreManager.instance.SaveScore();
        var dictEvent = new Dictionary<string, object> { { "HighScore", currentScore } };
        MyAnalytics.SendEvent("End", dictEvent);
    }

    public void SetDifficult(Difficult dif)
    {
        DifficultManager.instance.chooseDif = dif;
        if (dif == Difficult.Easy)
        {
            scrollSpeed = Consts.SCROLL_SPEED * 1.5F;
        }
        else
        {
            scrollSpeed = Consts.SCROLL_SPEED * 2.5F;
            _columnPool.spawnRate *= Consts.SPAWN_COL_MODIFICATOR;
            BonusManager.GetComponent<BonusManager>().spawnRate *= Consts.SPAWN_COL_MODIFICATOR;
        }
    }


    public void UpdateLegends()
    {
        txtCoinValue.text = "x " + Consts.COIN_VALUE * ScoreManager.instance.Modify;
        txtGemValue.text = "x " + Consts.GEM_VALUE * ScoreManager.instance.Modify;
    }


    //Internal
    private void AddBirdScoreRepeat()
    {
        ScoreManager.instance.AddScore(10, false);
        ScoreText.GetComponent<Text>().text = "Счет : " + ScoreManager.instance.Score.ToString();
    }

    private void ChangeUI(bool isFirst)
    {
        StartPanel.SetActive(!isFirst);
        HelpPanel.SetActive(isFirst);
    }

}
