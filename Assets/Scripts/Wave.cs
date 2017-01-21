using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int PointWorth;
    public int PointsAddedPerJump;

    public bool Reversed;
    public float Speed;
    public float LifeRange;
    public float KeepAliveJumpRange;
    /// <summary>
    /// The maximum range to consider a jumper responsible for keeping a wave alive;
    /// if they jump between this and the KeepAliveJumpRange, the wave will die, but no new waves will be created
    /// </summary>
    public float TerribleJumpRange;

    private float lastX;

    public float Direction { get { return Reversed ? -1 : 1; } }

    // Use this for initialization
    void Start()
    {
        if (Reversed)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        lastX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate if it's dead
        var diesAtX = lastX + Direction * LifeRange;
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
        if  (Mathf.Abs(jumperX - transform.position.x) < KeepAliveJumpRange)
        {
            lastX = jumper.position.x;
            if (!isNpc)
            {
                GameScore.GetInstance().Score += PointWorth;
                PointWorth += PointsAddedPerJump;
            }
        }
    }

    public bool IsInRange(Transform jumper)
    {
        var jumperX = jumper.position.x;
        return Mathf.Abs(jumperX - transform.position.x) < TerribleJumpRange;
    }

    public static IList<Wave> GetAllWavesInRange(Transform jumper)
    {
        return FindObjectsOfType<Wave>().Where(w => w.IsInRange(jumper)).ToList();
    }
}
