using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    void Start()
    {
        int highScore = HighScore.GetHighScore();
        GetComponent<Text>().text = string.Format("HIGH SCORE: {0:N0}", highScore); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            HighScore.ResetHighScore();
            Start();
        }
    }
}
