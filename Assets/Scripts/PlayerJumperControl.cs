using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class PlayerJumperControl : MonoBehaviour
{
    private Jumper jumper;
    public KeyCode Key;
    public SpriteRenderer Exclamation;

    void Start()
    {
        jumper = GetComponent<Jumper>();
        Exclamation = jumper.Exclamation;
    }

    void Update()
    {
        if (Input.GetKeyDown(Key) && jumper.CanJump)
        {
            jumper.Jump();
        }
        Exclamation.enabled = Wave.GetAllWavesInEffectRange(transform)
            .Any(w => w.IsInKeepAliveRange(transform) && w.JumperIsOk != jumper);
    }
}
