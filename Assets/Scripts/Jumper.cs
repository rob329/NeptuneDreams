using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    enum JumperState
    {
        ON_GROUND,
        JUMPING
    }

    public KeyCode Key;
    public float JumpSpeed;
    public float Gravity;
    public Wave WavePrefab;

    private JumperState currentState = JumperState.ON_GROUND;
    private float currentY;
    private float initialHeight;
    private float yVelocity;    

    void Start()
    {
        currentY = initialHeight = transform.position.y;
    }

    void Update()
    {
        if (currentState == JumperState.ON_GROUND)
        {
            if (Input.GetKeyDown(Key))
            {
                yVelocity = JumpSpeed;
                currentState = JumperState.JUMPING;
                SpawnWaves();
            }
        }
        transform.position = new Vector3(transform.position.x, initialHeight + currentY, transform.position.z);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case JumperState.ON_GROUND:
                currentY = 0;
                break;
            case JumperState.JUMPING:
                yVelocity -= Gravity;
                currentY += yVelocity;
                if (currentY <= 0)
                {
                    currentState = JumperState.ON_GROUND;
                    currentY = 0;
                }
                break;
        }
    }

    private void SpawnWaves()
    {
        var rightWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position, Quaternion.identity);
        var leftWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position, Quaternion.identity);
        leftWave.Reversed = true;
    }
}
