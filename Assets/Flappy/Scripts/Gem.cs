using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bird")
        {
            SoundManager.instance.PlayGemTakeSound();
            Destroy(gameObject);
            ScoreManager.instance.AddScore(Consts.GEM_VALUE, true);
        }
    }
}
