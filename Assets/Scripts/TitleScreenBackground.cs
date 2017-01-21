using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenBackground : MonoBehaviour {
	public Transform trans;
	public int DirectionAndSpeed;
	public bool FadeAway;
	public GameObject OtherObjs;
	public GameObject Parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (trans.position.x < -900 || trans.position.x > 900)
			DirectionAndSpeed = -DirectionAndSpeed;

		trans.position = new Vector3 (trans.position.x + DirectionAndSpeed * Time.deltaTime, trans.position.y, trans.position.z);

		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("DebugScene", LoadSceneMode.Additive);
			FadeAway = true;
			Destroy (OtherObjs);
		}

		if (FadeAway == true) {
			trans.position = new Vector3 (trans.position.x, trans.position.y + 10 * Time.deltaTime, trans.position.z);
			if (trans.position.y > 15)
				Destroy (Parent);
		}
	}
}
