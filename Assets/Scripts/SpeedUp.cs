using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float SpeedUpWithRemaining = 45;
    public float InitialSpeed;
    public float FastSpeed;
    public AudioClip FasterMusic;

    private bool hasSpedUp = false;
    private GameTime gameTime;

    public float CurrentSpeed
    {
        get
        {
            return hasSpedUp ? FastSpeed : InitialSpeed;
        }
    }

    public static SpeedUp GetInstance()
    {
        return FindObjectOfType<SpeedUp>();
    }

    void Start()
    {
        gameTime = GameTime.GetInstance();
    }

    void Update()
    {
        if (!hasSpedUp && gameTime.TimeRemaining < SpeedUpWithRemaining)
        {
            hasSpedUp = true;
            BigTextController.GetInstance().ShowText("Faster!");
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = FasterMusic;
            audioSource.Play();
        }
    }
}
