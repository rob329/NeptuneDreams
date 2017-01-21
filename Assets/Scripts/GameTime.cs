using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    public float TimeRemaining = 2 * 60;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            // TODO: game over?
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
