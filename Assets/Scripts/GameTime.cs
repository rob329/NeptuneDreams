using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    public float TimeRemaining = 2 * 60;

    public static GameTime GetInstance()
    {
        return FindObjectOfType<GameTime>();
    }

    public bool IsRunning
    {
        get
        {
            return TimeRemaining > 0;
        }
    }

    void Start()
    {
        // Little hack here: add a second to the clock so a player has a chance to see
        // the full time
        TimeRemaining += 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    void FixedUpdate()
    {
        if (IsRunning)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                StartCoroutine(EndOfGame());
            }
        }
    }

    private IEnumerator EndOfGame()
    {
        yield return BigTextController.GetInstance().ShowText("Game!");
        yield return BigTextController.GetInstance().ShowText("New high score!");
        BigTextController.GetInstance().ShowText("Press ESC to return to title screen");
    }
}
