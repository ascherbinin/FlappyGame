﻿using UnityEngine;
using System.Collections;

public class ColumnPool : MonoBehaviour 
{
	public GameObject columnPrefab;									//The column game object.
	public int columnPoolSize = 5;									//How many columns to keep on standby.
	public float spawnRate = 3f;									//How quickly columns spawn.
	public float columnMin = -1f;									//Minimum y value of the column position.
	public float columnMax = 3.5f;									//Maximum y value of the column position.

	private GameObject[] columns;									//Collection of pooled columns.
	private int currentColumn = 0;									//Index of the current column in the collection.

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);		//A holding position for our unused columns offscreen.
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;


	void Start()
	{
		timeSinceLastSpawned = 0f;

		var go = new GameObject ();
		go.name = "Colums";


		//Initialize the columns collection.
		columns = new GameObject[columnPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < columnPoolSize; i++)
        {
            //...and create the individual columns.
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
			columns[i].transform.parent = go.transform;
        }
    }

    void OnEnable()
    {
        //EventManager.StartListening("StartGame", GenerateColumns);
    }

    void OnDisable()
    {
        //EventManager.StopListening("StartGame", GenerateColumns);
    }

    //This spawns columns as long as the game is not over.
    void Update()
	{
        if (GameControl.instance.GameState == State.Play)
        {
            timeSinceLastSpawned += Time.deltaTime;
            if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate && GameControl.instance.GameState == State.Play)
            {

                timeSinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPosition = Random.Range(columnMin, columnMax);

                //...then set the current column to that position.
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);

                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                currentColumn++;

                if (currentColumn >= columnPoolSize)
                {
                    currentColumn = 0;
                }
            }

        }
	}

}