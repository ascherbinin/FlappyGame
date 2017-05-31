using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPoolObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;
    private bool isMoving = false;
    // Use this for initialization
    void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
		//Start the object moving.

	}

    //void Update()
    //{
    //    if (isMoving)
    //        transform.Translate(Vector3.right * Time.deltaTime * GameControl.instance.scrollSpeed);
    //}

    void OnEnable()
    {
		EventManager.StartListening("StartGame", StartGame);
		EventManager.StartListening("GameOver", GameOver); 
    }

    void OnDisable()
    {
        EventManager.StopListening("StartGame", StartGame);
		EventManager.StopListening("GameOver", GameOver);
    }

    void StartGame()
    {
        //isMoving = true;
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }

    void GameOver()
    {
        isMoving = false;
        rb2d.velocity = Vector2.zero;
    }
}
