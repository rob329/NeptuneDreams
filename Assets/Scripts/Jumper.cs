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
    public Sprite happyMouth;
    public Sprite sadEyes;
    public Sprite sadMouth;
    public Animator sweatdropAnimator;

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
            yVelocity = JumpSpeed;
            currentState = JumperState.JUMPING;
            SpawnWaves(isNpc: isNpc);
        }
    }

    private void SpawnWaves(bool isNpc = false)
    {
		Eyes.sprite = happyEyes;
		Mouth.sprite = happyMouth;
		var waves = Wave.GetAllWavesInEffectRange(transform);
        if (waves.Count > 0)
        {
            bool anyWavesKeptAlive = false;
            var wavesNotKeptAlive = new List<Wave>();
            foreach (var wave in waves)
            {
                if (wave.IsInKeepAliveRange(transform))
                {
					anyWavesKeptAlive = true;
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
                Eyes.sprite = sadEyes;
                Mouth.sprite = sadMouth;
                sweatdropAnimator.SetTrigger("Sad");
                foreach (var wave in wavesNotKeptAlive)
                {
                    wave.Kill();
                }
            }
            else
            {
                // We kept some waves alive, yay!
                Eyes.sprite = happyEyes;
                Mouth.sprite = happyMouth;
            }
        }
        else
        {
            var rightWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position + new Vector3(WaveStartOffset, 0), Quaternion.identity);
            rightWave.Reversed = false;
            rightWave.LastX = transform.position.x;
            var leftWave = GameObject.Instantiate<Wave>(WavePrefab, transform.position - new Vector3(WaveStartOffset, 0), Quaternion.identity);
            leftWave.Reversed = true;
            leftWave.LastX = transform.position.x;
        }
    }
}
