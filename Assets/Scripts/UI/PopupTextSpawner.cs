using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTextSpawner : MonoBehaviour
{
    public GameObject PopupTextPrefab;

    public static PopupTextSpawner GetInstance()
    {
        return FindObjectOfType<PopupTextSpawner>();
    }

    public void CreatePopupText(string text,  Vector3 position)
    {
        var newPrefab = Instantiate(PopupTextPrefab, position, Quaternion.identity);
        newPrefab.GetComponentInChildren<Text>().text = text;
    }
}
