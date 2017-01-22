using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Wave : MonoBehaviour
{
    public int PointWorth;
    public int PointsAddedPerJump;
    public float PopupHeight;

    public bool Reversed;
    public float Speed;
    public float LifeRange;
    public float KeepAliveJumpRange;
    /// <summary>
    /// The maximum range where a jumper can affect a wave.
    /// If they jump between this and the KeepAliveJumpRange, the wave will die, but no new waves will be created
    /// </summary>
    [FormerlySerializedAs("TerribleJumpRange")]
    public float EffectJumpRange;

    public float LastX;

    public float Direction { get { return Reversed ? -1 : 1; } }

    // Use this for initialization
    void Start()
    {
        if (Reversed)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate if it's dead
        var diesAtX = LastX + Direction * LifeRange;
        if ((!Reversed && transform.position.x > diesAtX)
            || (Reversed && transform.position.x < diesAtX))
        {
            GameObject.Destroy(gameObject);
        }

        transform.position += new Vector3(Direction * Speed * Time.deltaTime, 0, 0);
    }

    public void KeepAlive(Transform jumper, bool isNpc = false)
    {
        var jumperX = jumper.position.x;
        LastX = jumper.position.x;
        if (!isNpc && GameTime.GetInstance().IsRunning)
        {
            GameScore.GetInstance().AddScore(PointWorth, jumper.position + new Vector3(0, PopupHeight));
            PointWorth += PointsAddedPerJump;
        }
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }

    public bool IsInKeepAliveRange(Transform jumper)
    {
        var jumperX = jumper.position.x;
        return Mathf.Abs(jumperX - transform.position.x) < KeepAliveJumpRange;
    }

    public bool IsInEffectRange(Transform jumper)
    {
        var jumperX = jumper.position.x;
        return Mathf.Abs(jumperX - transform.position.x) < EffectJumpRange;
    }

    public static IList<Wave> GetAllWavesInEffectRange(Transform jumper)
    {
        return FindObjectsOfType<Wave>().Where(w => w.IsInEffectRange(jumper)).ToList();
    }
}
