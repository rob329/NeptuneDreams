using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimationEffect : MonoBehaviour
{
    public Sprite[] Frames;
    public float TimePerFrame;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Frames[0];
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        foreach (var frame in Frames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(TimePerFrame);
        }
        Destroy(gameObject);
    }
}
