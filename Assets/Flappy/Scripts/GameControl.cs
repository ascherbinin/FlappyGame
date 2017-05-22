using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum Difficult
{
    Easy,
    Normal
}

public enum State
{
    Play,
    Pause
}

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
    public State GameState;
    public Text lblButton;
    public GameObject StartPanel;
    public GameObject GameOverCanvas;
    public GameObject Bird;
    private int score = 0;						//The player's score.
    private ColumnPool columnPool;
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = 0f;
	//private string inputSTR = "";
    private const float SCROLL_SPEED = -2.5f;
    private const float SPAWN_COL_MODIFICATOR = 0.8f;
    


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
        columnPool = GetComponent<ColumnPool>();
        SaveLoad.LoadScores();
        foreach (var item in SaveLoad.scoresList)
        {
            Debug.Log(item.Date + ":" + item.Value);
        }
    }

    void Update()
	{
		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(kcode))
				//Debug.Log("KeyCode down: " + kcode);
				lblButton.text = ""+kcode;
		}

        //		inputSTR = (string)Input.inputString;
        //		if(inputSTR != "")
        //			Debug.Log(inputSTR);

    }

    public void StartGame()
    {
        GameState = State.Play;
        EventManager.TriggerEvent("StartGame");
        StartPanel.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(false);
        Bird.gameObject.SetActive(true);
        InvokeRepeating("AddBirdScoreRepeat", 0, 1);
    }

	public void BirdScored(int value)
	{
		//The bird can't score if the game is over.
		if (gameOver)	
			return;
		//If the game is not over, increase the score...
		score += value;
		//...and adjust the score text.
		scoreText.text = "Счет : " + score.ToString();
	}

	public void BirdDied()
	{
        EventManager.TriggerEvent("GameOver");
        GameState = State.Pause;
        SaveLoad.SaveScore(score);
        gameOver = true;
        Debug.Log("Save score:" + score);
        //Activate the game over text.
        GameOverCanvas.gameObject.SetActive(true);
        //Set the game to be over.
    
	}

    public void SetDifficult(Difficult dif)
    {
        if (dif == Difficult.Easy)
        {
            scrollSpeed = SCROLL_SPEED;
        }
        else
        {
            scrollSpeed = SCROLL_SPEED * 2F;
            columnPool.spawnRate *= SPAWN_COL_MODIFICATOR;
        }
    }

    //Internal
    private void AddBirdScoreRepeat()
    {
        BirdScored(10);
    }


}
