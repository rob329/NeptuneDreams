using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Wave : MonoBehaviour
{
    public Sprite pulseSprite;
    public GameObject BrokenPrefab;

    public int PointWorth;
    public int PointsAddedPerJump;
    public float PopupHeight;
    public int MaximumPointValue = 20;
    public float PulseSize;
    public float PulseTime;

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

    public bool IsMaxValue
    {
        get
        {
            return PointWorth >= MaximumPointValue;
        }
    }
    public float Direction { get { return Reversed ? -1 : 1; } }

    private Sprite normalSprite;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        if (Reversed)
        {
            spriteRenderer.flipX = true;
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

    private IEnumerator Pulse()
    {
        spriteRenderer.sprite = pulseSprite;
        yield return new WaitForSeconds(PulseTime);
        spriteRenderer.sprite = normalSprite;
    }

    public void KeepAlive(Transform jumper, bool isNpc = false)
    {
        LastX = jumper.position.x;
        StartCoroutine(Pulse());
        if (!isNpc && GameTime.GetInstance().IsRunning)
        {
            GameScore.GetInstance().AddScore(PointWorth, jumper.position + new Vector3(0, PopupHeight));
            PointWorth += PointsAddedPerJump;
        }
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
        var broken = GameObject.Instantiate(BrokenPrefab, transform.position, transform.rotation);
        if (Reversed)
        {
            broken.GetComponent<SpriteRenderer>().flipX = true;
        }
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
