using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficult
{
    Easy,
    Normal
}

public class DifficultManager : MonoBehaviour
{
    public static DifficultManager instance;

    public Difficult chooseDif = Difficult.Easy;
    public bool isFirstStart;

    void Awake()
    {
        isFirstStart = true;
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
