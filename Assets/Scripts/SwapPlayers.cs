using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlayers : MonoBehaviour {

	public Transform[] AllJumpers;
	public Transform[] Players;
	public Transform[] CPUs;
	public int PlayerToSwap;
	public int CPUToSwap;

	public Transform Swap1;
	public Transform Swap2;

	public float Distance;

	public Transform CenterSwap;
	public float TimerToSwap = -8;

	public float Speed = 200;
	public bool Swapping;
	
	public void Init () {


		int b = 0;
		int c = 0;


		for (int a = 0; a < AllJumpers.Length; a = a + 1) {
			if (AllJumpers [a].GetComponent <PlayerJumperControl>() != null) {
				Players [b] = AllJumpers [a];
				b = b + 1;
			}

			if (AllJumpers [a].GetComponent <NPCJumperControl>() != null) {
				CPUs [c] = AllJumpers [a];
				c = c + 1;
			}
		}
			


	}
	
	// Update is called once per frame
	void Update () {

		if (TimerToSwap > 10) {
			PlayerToSwap = Random.Range (0, Players.Length);
			CPUToSwap = Random.Range (0, CPUs.Length);

			Swap1 = Players [PlayerToSwap];
			Swap2 = CPUs [CPUToSwap];


			if (Swap1.position.x < Swap2.position.x) {
				Distance = Swap2.position.x - Swap1.position.x;
				CenterSwap.position = new Vector3 (Swap1.position.x + (Distance/2), CenterSwap.position.y, CenterSwap.position.z);
			} else {
				Distance = Swap1.position.x - Swap2.position.x;
				CenterSwap.position = new Vector3 (Swap2.position.x + (Distance/2), CenterSwap.position.y, CenterSwap.position.z);
			}

			Swapping = true;
			Swap1.parent = CenterSwap;
			Swap2.parent = CenterSwap;
			Swap1.position = new Vector3 (Swap1.position.x, -2.77f, Swap1.position.z);
			Swap2.position = new Vector3 (Swap2.position.x, -2.77f, Swap2.position.z);

			Swap1.gameObject.GetComponent<Jumper> ().enabled = false;
			Swap2.gameObject.GetComponent<Jumper> ().enabled = false;


			TimerToSwap = 0;
		} else {
			TimerToSwap += Time.deltaTime;
		}

		if (Swapping == true) {
			Swap1.eulerAngles = new Vector3 (Swap1.eulerAngles.x, Swap1.eulerAngles.y, 0);
			Swap2.eulerAngles = new Vector3 (Swap2.eulerAngles.x, Swap2.eulerAngles.y, 0);

			CenterSwap.eulerAngles = new Vector3 (CenterSwap.eulerAngles.x, CenterSwap.eulerAngles.y, CenterSwap.eulerAngles.z + (Time.deltaTime * Speed));

			if (CenterSwap.eulerAngles.z >= 180) {
				CenterSwap.eulerAngles = new Vector3 (CenterSwap.eulerAngles.x, CenterSwap.eulerAngles.y, 180);
				Swap1.eulerAngles = new Vector3 (Swap1.eulerAngles.x, Swap1.eulerAngles.y, 0);
				Swap2.eulerAngles = new Vector3 (Swap2.eulerAngles.x, Swap2.eulerAngles.y, 0);
				Swap1.parent = null;
				Swap2.parent = null;

				CenterSwap.eulerAngles = new Vector3 (CenterSwap.eulerAngles.x, CenterSwap.eulerAngles.y, 0);
				Swap1.position = new Vector3 (Swap1.position.x, -2.77f, Swap1.position.z);
				Swap2.position = new Vector3 (Swap2.position.x, -2.77f, Swap2.position.z);
				Swap1.gameObject.GetComponent<Jumper> ().enabled = true;
				Swap2.gameObject.GetComponent<Jumper> ().enabled = true;
				Swapping = false;
			}
		}

	}
}
