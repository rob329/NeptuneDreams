using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour {
	public Transform trans;
	public float Speed;
	// Use this for initialization
	void Start () {
		trans = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		trans.eulerAngles = new Vector3 (trans.eulerAngles.x, trans.eulerAngles.y, trans.eulerAngles.z + Speed * Time.deltaTime);
	}
}
