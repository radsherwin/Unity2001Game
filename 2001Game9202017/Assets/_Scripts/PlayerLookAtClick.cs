using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Turn ();
		}
	}

	void Turn(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Vector3 mousePos = Input.mousePosition;
		mousePos.y = transform.rotation.y;
		transform.LookAt (mousePos);

	}
}
