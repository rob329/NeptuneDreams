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
	public bool CreditScreenToggle;
	public GameObject CreditScreen;
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
				if (Input.GetKeyDown ("c")) {
					CreditScreenToggle = !CreditScreenToggle;

				}

				if (Input.anyKeyDown && Input.GetKey ("c") != true) {
                    Destroy(Camera.main.GetComponent<AudioListener>());
                    FadeAway = true;
                    SceneManager.LoadSceneAsync("DebugScene", LoadSceneMode.Additive);
                    Invoke("PlayWhoosh", 0.05f);
                    foreach (var obj in OtherObjs)
                    {
                        Destroy(obj);
                    }
				}
			}


		}

		if (FadeAway == true) {
			trans.position = new Vector3 (trans.position.x, trans.position.y + 10 * Time.deltaTime, trans.position.z);
			if (trans.position.y > 15) {
				GameTime.GetInstance ().StartGame ();
				Destroy (Parent);
			}
				
		} else {
			CreditScreen.SetActive (CreditScreenToggle);
		}


	}

    private void PlayWhoosh()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
