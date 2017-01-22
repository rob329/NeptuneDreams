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
    public bool InstructionsScreenToggle;
    public GameObject CreditScreen;
    public GameObject InstructionsScreen;
    public GameObject DeleteOnStart;
	public AudioSource TitleScreenMusicObj;
	public AudioClip TitleScreenMusic;

	void Start (){
		AnyKeyDisplay.SetActive (false);
	}
	void Update () {
		if (trans.position.x < -900 || trans.position.x > 900)
			DirectionAndSpeed = -DirectionAndSpeed;



		trans.position = new Vector3 (trans.position.x + DirectionAndSpeed * Time.deltaTime, trans.position.y, trans.position.z);

		if (AnyKeyDelay < 1) {
		//	AnyKeyDelay += Time.deltaTime;
		//	AnyKeyDisplay.SetActive (false);

		} else {

			if (AnyKeyDisplay != null) AnyKeyDisplay.SetActive (true);
			if (FadeAway == false) {

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Quitting");
                    Application.Quit();
                    return;
                }
				if (Input.GetKeyDown ("x") && !InstructionsScreenToggle) {
					CreditScreenToggle = !CreditScreenToggle;
				}
                if (Input.GetKeyDown(KeyCode.Space) && !CreditScreenToggle)
                {
                    InstructionsScreenToggle = !InstructionsScreenToggle;
                }

				if (Input.anyKeyDown && !Input.GetKey ("x") && !Input.GetKey(KeyCode.Space) && !InstructionsScreenToggle) {
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
			if (TitleScreenMusicObj.isPlaying == false) {
				TitleScreenMusicObj.clip = TitleScreenMusic;
				TitleScreenMusicObj.loop = true;
				TitleScreenMusicObj.Play ();
				AnyKeyDelay = 1;
			}
			CreditScreen.SetActive (CreditScreenToggle);
            InstructionsScreen.SetActive(InstructionsScreenToggle);

        }


	}

    private void PlayWhoosh()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
