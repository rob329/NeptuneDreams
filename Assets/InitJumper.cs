using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitJumper : MonoBehaviour
{
    public GameObject KeyLabelPrefab;
    public Transform LabelSpot;

    void Start()
    {
        InitPlayer(KeyCode.Q);
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
        
    }
}
