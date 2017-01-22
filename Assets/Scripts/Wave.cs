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
    public float LifeRange;
    public float KeepAliveJumpRange;

    /// <summary>
    /// The maximum range where a jumper can affect a wave.
    /// If they jump between this and the KeepAliveJumpRange, the wave will die, but no new waves will be created
    /// </summary>
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

    public float Speed { get { return speedUp.CurrentSpeed; } }

    private Sprite normalSprite;
    private SpriteRenderer spriteRenderer;
    private Jumper jumperToBlame;
    private Jumper jumperIsOk;
    private Jumper[] allJumpers;
    private SpeedUp speedUp;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        speedUp = SpeedUp.GetInstance();
        if (Reversed)
        {
            spriteRenderer.flipX = true;
        }
        allJumpers = FindObjectsOfType<Jumper>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find out if we're near a jumper who ought to do something
        foreach (var jumper in allJumpers)
        {
            if (IsInKeepAliveRange(jumper.transform))
            {
                if (jumper != jumperIsOk)
                {
                    jumperToBlame = jumper;
                    jumperIsOk = null;
                }
                break;
            }
        }

        // Calculate if it's dead
        var diesAtX = LastX + Direction * LifeRange;
        if ((!Reversed && transform.position.x > diesAtX)
            || (Reversed && transform.position.x < diesAtX))
        {
            float currentX = transform.position.x;

            bool isPlayer = jumperToBlame != null && jumperToBlame != jumperIsOk && jumperToBlame.GetComponent<PlayerJumperControl>() != null;
            if (isPlayer)
            {
                jumperToBlame.BeSadOnGround();
            }
            Kill(isPlayer: isPlayer);
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
        jumperIsOk = jumper.GetComponent<Jumper>();
        StartCoroutine(Pulse());
        if (!isNpc && GameTime.GetInstance().IsRunning)
        {
            GameScore.GetInstance().AddScore(PointWorth, jumper.position + new Vector3(0, PopupHeight));
            PointWorth += PointsAddedPerJump;
        }
    }

    public void Kill(bool isPlayer = false)
    {
        GameObject.Destroy(gameObject);
        if (isPlayer)
        {
            var broken = GameObject.Instantiate(BrokenPrefab, transform.position, transform.rotation);
            if (Reversed)
            {
                broken.GetComponent<SpriteRenderer>().flipX = true;
            }
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
