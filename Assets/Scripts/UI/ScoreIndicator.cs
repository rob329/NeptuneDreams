using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreIndicator : MonoBehaviour
{
    private Text text;
    private GameScore gameScore;

    void Start()
    {
        text = GetComponent<Text>();
        gameScore = FindObjectOfType<GameScore>();
        UpdateText();
    }

    void FixedUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        // Format number with commas
        text.text = String.Format("Score: {0:N0}", gameScore.Score);
    }
}
