using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class NPCJumperControl : MonoBehaviour
{
    public float JumpRange = 0.5f;

    private Jumper jumper;

    void Start()
    {
        jumper = GetComponent<Jumper>();
    }

    void Update()
    {
        if (Time.timeScale < 0.1f) return;
        var waves = Wave.GetAllWavesInEffectRange(transform);
        if (waves.Any(w => WaveIsInRange(w)))
        {
            jumper.Jump(isNpc: true);
        }
    }

    private bool WaveIsInRange(Wave w)
    {
        return Mathf.Abs(w.transform.position.x - transform.position.x) < JumpRange;
    }
}
