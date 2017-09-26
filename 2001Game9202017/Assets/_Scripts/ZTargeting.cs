using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeting : MonoBehaviour {
	private Transform player;
	public Transform enemy;
	private float speed;
	void Start(){
		
	}

	void Update(){
		player = GameObject.FindWithTag ("Player").transform;
		Targeting ();
	}

	void Targeting(){
		

		Vector3 dir = player.transform.position - enemy.transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward); 
		enemy.transform.rotation = Quaternion.Slerp (enemy.transform.rotation, q, Time.deltaTime * speed);
	}
}
