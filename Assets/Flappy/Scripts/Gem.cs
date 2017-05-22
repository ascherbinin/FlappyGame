using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private const int Value = 200;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
            GameControl.instance.BirdScored(Value);
        }
    }
}
