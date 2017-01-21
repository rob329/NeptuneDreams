using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class NPCJumperControl : MonoBehaviour
{
    public float JumpRange = 0.5;

    private Jumper jumper;

    void Start()
    {
        jumper = GetComponent<Jumper>();
    }

    void Update()
    {
        var waves = Wave.GetAllWavesInRange(transform);
        if (waves.Any(w => WaveIsInRange(w)))
        {
            jumper.Jump();
        }
    }

    private bool WaveIsInRange(Wave w)
    {
        return Mathf.Abs(w.transform.position.x - transform.position.x) < JumpRange;
    }
}
