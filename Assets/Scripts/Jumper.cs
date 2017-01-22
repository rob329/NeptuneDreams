using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    enum JumperState
    {
        ON_GROUND,
        JUMPING
    }

    public float JumpSpeed;
    public float Gravity;
    public float WaveStartOffset;
    public Wave WavePrefab;
    public SpriteRenderer Eyes;
    public SpriteRenderer Mouth;
    public Sprite baseEyes;
    public Sprite baseMouth;
    public Sprite happyEyes;
    public Sprite winkingHappyEyes;
    public Sprite happyMouth;
    public Sprite sadEyes;
    public Sprite sadMouth;
    public Animator sweatdropAnimator;
    public float ChanceToWink = 0.3f;
    public AudioClip LandingSound;
    public AudioClip JumpSound;
    public AudioClip WaveSuccessSound;
    public AudioClip[] WaveCompletionSounds;

    private AudioSource audioSource;
    private JumperState currentState = JumperState.ON_GROUND;
    private float currentY;
    private float initialHeight;
    private float yVelocity;

    public bool CanJump {
        get
        {
            return currentState == JumperState.ON_GROUND;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentY = initialHeight = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, initialHeight + currentY, transform.position.z);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case JumperState.ON_GROUND:
                currentY = 0;
                break;
            case JumperState.JUMPING:
                yVelocity -= Gravity;
                currentY += yVelocity;
                if (currentY <= 0)
                {
                    audioSource.PlayOneShot(LandingSound);
                    Eyes.sprite = baseEyes;
                    Mouth.sprite = baseMouth;
                    currentState = JumperState.ON_GROUND;
                    currentY = 0;
                }
                break;
        }
    }

    public void Jump(bool isNpc = false)
    {
        if (currentState == JumperState.ON_GROUND)
        {
            audioSource.PlayOneShot(JumpSound);
            yVelocity = JumpSpeed;
            currentState = JumperState.JUMPING;
            SpawnWaves(isNpc: isNpc);
        }
    }

    private void SpawnWaves(bool isNpc = false)
    {
		var waves = Wave.GetAllWavesInEffectRange(transform);
        if (waves.Count > 0)
        {
            bool anyWavesKeptAlive = false;
            bool anyWavesCompleted = false;
            var wavesNotKeptAlive = new List<Wave>();
            foreach (var wave in waves)
            {
                if (wave.IsInKeepAliveRange(transform))
                {
					anyWavesKeptAlive = true;
                    if (wave.IsMaxValue)
                    {
                        anyWavesCompleted = true;
                    }
                    wave.KeepAlive(transform, isNpc: isNpc);
                }
                else
                {
					wavesNotKeptAlive.Add(wave);
                }
            }
            // Only kill waves if you haven't successfully jumped for any of them;
            // don't want to rain on their successful happiness and stuff
            if (!anyWavesKeptAlive)
            {
                BeSad();
                foreach (var wave in wavesNotKeptAlive)
                {
                    wave.Kill(true);
                }
            }
            else
            {
                // We kept some waves alive, yay!
                if (!isNpc)
                {
                    audioSource.PlayOneShot(WaveSuccessSound);
                    if (anyWavesCompleted)
                    {
                        audioSource.PlayOneShot(WaveCompletionSounds[Random.Range(0, WaveCompletionSounds.Length)]);
                    }
                }
                BeHappy();
            }
        }
        else
        {
            // Spawn waves
            BeHappy();
            var rightWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position + new Vector3(WaveStartOffset, 0), Quaternion.identity);
            rightWave.Reversed = false;
            rightWave.LastX = transform.position.x;
            var leftWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position - new Vector3(WaveStartOffset, 0), Quaternion.identity);
            leftWave.Reversed = true;
            leftWave.LastX = transform.position.x;
        }
    }

    // The logic for this will be slightly different because
    // we can't rely on landing to take the face back to normal
    public void BeSadOnGround()
    {
        BeSad();
    }

    private void BeSad()
    {
        Eyes.sprite = sadEyes;
        Eyes.flipX = false;
        Mouth.sprite = sadMouth;
        sweatdropAnimator.SetTrigger("Sad");
    }

    private void BeHappy()
    {
        if (Random.value > ChanceToWink)
        {
            Eyes.flipX = false;
            Eyes.sprite = happyEyes;
        }
        else
        {
            Eyes.sprite = winkingHappyEyes;
            // Randomly wink the opposite eye
            Eyes.flipX = Random.value > 0.5f;
        }
        Mouth.sprite = happyMouth;
    }
}
