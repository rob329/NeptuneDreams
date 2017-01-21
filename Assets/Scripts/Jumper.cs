﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    enum JumperState
    {
        ON_GROUND,
        JUMPING
    }

    public float JumpSpeed;
    public float Gravity;
    public Wave WavePrefab;

    private JumperState currentState = JumperState.ON_GROUND;
    private float currentY;
    private float initialHeight;
    private float yVelocity;

    public bool CanJump {
        get
        {
            return currentState == JumperState.ON_GROUND;
        }
    }

    void Start()
    {
        currentY = initialHeight = transform.position.y;
    }

    void Update()
    {
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

    public void Jump()
    {
        if (currentState == JumperState.ON_GROUND)
        {
            yVelocity = JumpSpeed;
            currentState = JumperState.JUMPING;
            SpawnWaves();
        }
    }

    private void SpawnWaves()
    {
        var waves = Wave.GetAllWavesInRange(transform);
        if (waves.Count > 0)
        {
            foreach (var wave in waves)
            {
                wave.KeepAlive(transform);
            }
        }
        else
        {
            var rightWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position, Quaternion.identity);
            rightWave.Reversed = false;
            var leftWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position, Quaternion.identity);
            leftWave.Reversed = true;
        }
    }
}
