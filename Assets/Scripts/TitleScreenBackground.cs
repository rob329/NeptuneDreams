using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TitleScreenBackground : MonoBehaviour {
	public Transform trans;
	public int DirectionAndSpeed;
	public bool FadeAway;
	public GameObject[] OtherObjs;
	public GameObject Parent;
	public GameObject AnyKeyDisplay;
	private float AnyKeyDelay;

    public GameObject DeleteOnStart;
	
	void Update () {
		if (trans.position.x < -900 || trans.position.x > 900)
			DirectionAndSpeed = -DirectionAndSpeed;

		trans.position = new Vector3 (trans.position.x + DirectionAndSpeed * Time.deltaTime, trans.position.y, trans.position.z);



		if (AnyKeyDelay < 1) {
			AnyKeyDelay += Time.deltaTime;
			AnyKeyDisplay.SetActive (false);

		} else {

			if (AnyKeyDisplay != null) AnyKeyDisplay.SetActive (true);
			if (FadeAway == false) {

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Quitting");
                    Application.Quit();
                    return;
                }

				if (Input.anyKeyDown) {
					this.gameObject.GetComponent<AudioSource> ().Play ();
					SceneManager.LoadScene ("DebugScene", LoadSceneMode.Additive);
					FadeAway = true;
                    foreach (var obj in OtherObjs)
                    {
                        Destroy(obj);
                    }
				}
			}


		}

		if (FadeAway == true) {
			trans.position = new Vector3 (trans.position.x, trans.position.y + 10 * Time.deltaTime, trans.position.z);
			if (trans.position.y > 15)
				Destroy (Parent);
		}
	}
}
