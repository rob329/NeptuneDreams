using UnityEngine;

public static class HighScore
{
    const string PLAYER_PREFS_KEY = "WaveRaveTogetherHiScore";

    public static int GetHighScore()
    {
        if (!PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
        {
            return 0;
        }
        else
        {
            return PlayerPrefs.GetInt(PLAYER_PREFS_KEY);
        }
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_KEY, score);
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(PLAYER_PREFS_KEY);
    }
}
