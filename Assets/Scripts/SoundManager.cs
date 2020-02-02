using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip
        eatApple,
        hammer,
        lightBulb,
        plantGrows,
        tearsOut,
        waterScoop,
        waterScoop2,
        pickUp,
        endMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEatApple()
    {
        audioSource.PlayOneShot(eatApple);
    }

    public void PlayHammer()
    {
        audioSource.PlayOneShot(hammer);
    }

    public void PlayLightBulb()
    {
        audioSource.PlayOneShot(lightBulb);
    }

    public void PlayPlantGrows()
    {
        audioSource.PlayOneShot(plantGrows);
    }

    public void PlayTearsOut()
    {
        audioSource.PlayOneShot(tearsOut);
    }

    public void PlayWaterScoop()
    {
        audioSource.PlayOneShot(waterScoop);
    }

    public void PlayWaterScoop2()
    {
        audioSource.PlayOneShot(waterScoop2);
    }

    public void PlayPickUp()
    {
        audioSource.PlayOneShot(pickUp);
    }

    public void PlayEndMusic()
    {
        audioSource.PlayOneShot(endMusic);
    }
}
