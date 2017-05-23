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
            SoundManager.instance.PlayGemTakeSound();
            Destroy(gameObject);
            GameControl.instance.BirdScored(Value);
        }
    }
}
