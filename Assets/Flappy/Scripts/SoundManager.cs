using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;			//A reference to our game control script so we can access it statically.
    public AudioClip coinTakeSound;
    public AudioClip gemTakeSound;
    public AudioClip failSound;
    public AudioClip newHighScoreSound;
    public AudioClip selectSound;

    private AudioSource _asource;

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    void Start()
    {
        _asource = GetComponent<AudioSource>();
    }

    public void PlayCoinTakeSound()
    {
        _asource.PlayOneShot(coinTakeSound);
    }

    public void PlayGemTakeSound()
    {
        _asource.PlayOneShot(gemTakeSound);
    }

    public void PlayNewHighScoreSound()
    {
        _asource.PlayOneShot(newHighScoreSound, 0.7F);
    }

    public void PlayFailSound()
    {
        _asource.PlayOneShot(failSound, 0.4F);
    }

    public void PlaySelectSound()
    {
        _asource.PlayOneShot(selectSound, 0.4F);
    }
}
