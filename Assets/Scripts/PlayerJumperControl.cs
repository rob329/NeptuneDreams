using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class PlayerJumperControl : MonoBehaviour
{
    private Jumper jumper;
    public KeyCode Key;

    void Start()
    {
        jumper = GetComponent<Jumper>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key) && jumper.CanJump)
        {
            jumper.Jump();
        }
    }
}
