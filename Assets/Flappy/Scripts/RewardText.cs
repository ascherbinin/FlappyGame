using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardText : MonoBehaviour
{
    public Text msg;
    private Vector2 startPos;
    private bool isAlive = false;
	// Use this for initialization
	void Start () {
        msg = GetComponent<Text>();
        //msg.text = "";

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isAlive)
        {
            transform.position = Vector2.up * Time.deltaTime * 10;
        }
	}

    public void Init(int value)
    {
        msg.text = "+ " + value.ToString();
        isAlive = true;
    }
}
