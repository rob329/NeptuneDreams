using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBGObject : MonoBehaviour {
	public Transform trans;
	public SpriteRenderer thisRender;
	public float Speed;
	public float RotationSpeed;
	public int ListTest;
	public Sprite[] ListOfObjects;
	// Use this for initialization
	void Start () {
		trans = this.gameObject.transform;
		thisRender = this.gameObject.GetComponent<SpriteRenderer> ();
		ResetObject ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale < 0.1f) return;

        trans.position = new Vector3 (trans.position.x - Speed*Time.deltaTime, trans.position.y, trans.position.z);
		trans.eulerAngles = new Vector3 (trans.eulerAngles.x, trans.eulerAngles.y, trans.eulerAngles.z - RotationSpeed*Time.deltaTime);


		if (trans.position.x < 0) {
			if (thisRender.isVisible == false) {
				trans.position = new Vector3 (-trans.position.x, trans.position.y, trans.position.z);
				ResetObject ();
			}
		}


	}

	void ResetObject(){
		Speed = Random.Range (1, 4);
		RotationSpeed = Random.Range (-15, 15);
		ListTest = Random.Range (0, ListOfObjects.Length);
		thisRender.sprite = ListOfObjects [ListTest];
	}
}
