using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumperSetup : MonoBehaviour
{
    public KeyCode[] PlayerKeys;

    void Start()
    {
        var jumpers = FindObjectsOfType<InitJumper>().ToList();
        foreach (var key in PlayerKeys)
        {
            // Assign a key to a random jumper
            var rand = jumpers[Random.Range(0, jumpers.Count)];
            jumpers.Remove(rand);
            rand.InitPlayer(key);
        }
        // Everybody else is NPCs
        foreach (var jumper in jumpers)
        {
            jumper.InitNPC();
        }

        // Players and NPCs are set up, can safely initialize SwapPlayers now
        var swapPlayers = FindObjectOfType<SwapPlayers>();
        if (swapPlayers)
        {
            swapPlayers.Init();
        }
    }
}
