using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    private Text text;
    private GameTime gameTime;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        gameTime = FindObjectOfType<GameTime>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float timeRemaining = gameTime.TimeRemaining;
        int minutesRemaining = Mathf.FloorToInt(timeRemaining / 60);
        int secondsRemaining = Mathf.FloorToInt(timeRemaining % 60);

        var textTimer = System.String.Format("{0}:{1}", minutesRemaining, secondsRemaining.ToString().PadLeft(2, '0'));
        text.text = textTimer;
    }
}
