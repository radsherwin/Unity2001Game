using UnityEngine;
using System.Collections;
using ORKFramework;
public class Orbit : MonoBehaviour
{
	PlayerManager playerMang;

	private GameObject player;
	private Rigidbody rgbd;
	private bool lockedON = false;

	GameObject[] enemyLocations;
	GameObject closest;
	public float lockOnDistance;

	void Start()
	{
		playerMang = GetComponent<PlayerManager> ();
		player = ORK.Game.GetPlayer ();
		rgbd = GetComponent<Rigidbody> ();
		// Make the rigid body not change rotation
		if (rgbd)
			rgbd.freezeRotation = true;


	}
		


	void Update()
	{
		if (Input.GetButtonDown ("Target")) {
			ClosestEnemy ();
			lockedON = !lockedON;
			ORK.Game.Variables.Set ("lockOnOrk", lockedON.ToString ());
		}

		if (lockedON)
			LockOn ();
	}

	//LockOn to closest Enemy
	void LockOn(){

		if(lockedON)
		{
			//If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
			if (!closest)
			{      
				//activeIcon.active = false;
				lockedON = false;
				closest = null;
			}

			//if (playerController.isAttacking)
			if(closest != null)
				transform.LookAt(new Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
		}
	}

	//Find ClosestEnemy
	private GameObject ClosestEnemy(){
		// Find all game objects with tag Enemy
		enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");
		//var closest : GameObject;
		lockOnDistance = Mathf.Infinity;
		Vector3 position = transform.position;
		// Iterate through them and find the closest one
		foreach (GameObject go in enemyLocations)
		{
			Vector3 diff = (go.transform.position - position);
			float curDistance = diff.sqrMagnitude;

			if (curDistance < lockOnDistance)
			{
				closest = go;
				lockOnDistance = curDistance;
			}
		}
		return closest;
	}
}