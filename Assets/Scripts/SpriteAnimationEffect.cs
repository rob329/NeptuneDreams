using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimationEffect : MonoBehaviour
{
    public Sprite[] Frames;
    public float TimePerFrame;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (audioSource)
        {
            // Wait for audio to finish
            float clipLength = audioSource.clip.length;
            float timeRemaining = clipLength - Frames.Length * TimePerFrame;
            if (timeRemaining > 0)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(timeRemaining);
            }
        }
        Destroy(gameObject);
    }
}
