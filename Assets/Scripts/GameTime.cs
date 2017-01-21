using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    public float TimeRemaining = 2 * 60 + 1; // extra one second so it shows up on the clock, which rounds down

    void FixedUpdate()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            TimeRemaining = 0;
            // TODO: game over?
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
