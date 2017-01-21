using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int Score = 0;

    public static GameScore GetInstance()
    {
        return FindObjectOfType<GameScore>();
    }
    
    public void AddScore(int points, Vector3 position)
    {
        Score += points;
        PopupTextSpawner.GetInstance().CreatePopupText(points.ToString("N0"), position);
    }
}
