﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitJumper : MonoBehaviour
{
    public GameObject KeyLabelPrefab;
    public Transform LabelSpot;
    public SpriteRenderer Eyes;
    public SpriteRenderer Mouth;
    public Color NPCColor;

    public Sprite[] bodies;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bodies[Random.Range(0, bodies.Length)];
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
        var controller = gameObject.AddComponent<NPCJumperControl>();
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = NPCColor;
        }
    }
}
