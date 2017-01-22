using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Jumper))]
public class InitJumper : MonoBehaviour
{
    [System.Serializable]
    public class BodyTypeInfo
    {
        public Sprite BodySprite;
        public AudioClip[] WaveCompletionSounds;
    }

    public GameObject KeyLabelPrefab;
    public Transform LabelSpot;
    public Color NPCColor;

    public Sprite[] bodies;
    public Sprite bodyWithoutMouth;
    public BodyTypeInfo[] BodyTypeExtras;

    private void Start()
    {
        var jumper = GetComponent<Jumper>();
        var bodySprite = bodies[Random.Range(0, bodies.Length)];
        GetComponent<SpriteRenderer>().sprite = bodySprite;
        if (bodySprite == bodyWithoutMouth)
        {
            jumper.Mouth.enabled = false;
        }
        foreach (var extras in BodyTypeExtras)
        {
            if (extras.BodySprite == bodySprite)
            {
                var newSounds = new List<AudioClip>(jumper.WaveCompletionSounds);
                newSounds.AddRange(extras.WaveCompletionSounds);
                jumper.WaveCompletionSounds = newSounds.ToArray();
                break;
            }
        }
    }

    public void InitPlayer(KeyCode key)
    {
        var controller = gameObject.AddComponent<PlayerJumperControl>();
        controller.Key = key;
        var keyLabel = GameObject.Instantiate(KeyLabelPrefab, LabelSpot, false);
        keyLabel.transform.localPosition = Vector3.zero;
        keyLabel.GetComponentInChildren<Text>().text = key.ToString();
    }
    
    public void InitNPC()
    {
        gameObject.AddComponent<NPCJumperControl>();
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = NPCColor;
        }
    }
}
