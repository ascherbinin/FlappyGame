using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingInstantiateObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
        //Start the object moving.
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }

	void Update()
	{

	}

    void OnEnable()
    {
		EventManager.StartListening("GameOver", GameOver); 
    }

    void OnDisable()
    {
		EventManager.StopListening("GameOver", GameOver);
    }

    void GameOver()
    {
        rb2d.velocity = Vector2.zero;
    }
}
