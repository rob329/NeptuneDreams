using System.Collections;
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
        transform.position = new Vector3(transform.position.x, initialHeight + currentY, transform.position.z);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case JumperState.ON_GROUND:
                currentY = 0;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    yVelocity = JumpSpeed;
                    currentState = JumperState.JUMPING;
                }
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
}
