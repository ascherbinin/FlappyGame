using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bird")
        {
            SoundManager.instance.PlayCoinTakeSound();
            Destroy(gameObject);
            ScoreManager.instance.AddScore(Consts.COIN_VALUE, true);
        }
    }
}
