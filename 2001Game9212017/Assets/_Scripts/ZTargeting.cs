using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeting : MonoBehaviour {
	private int current = 0;
	private bool locked = false; 
	//r playerController : ThirdPersonController ;
	public Transform player;
	 GameObject[] enemyLocations;
	 GameObject closest;
	 Transform activeIcon; //Current targeted enemy indicator
	void Start(){
		player = this.transform;
	}

	void FixedUpdate() 
	{     
		//var playerController : ThirdPersonController = GetComponent(ThirdPersonController);

		/*
		if (closest != null && locked)

		{
			activeIcon.gameObject.SetActive (true);

			Transform tempActiveIcon;
			tempActiveIcon.position.y = (closest.transform.position.y+1);
			tempActiveIcon.position.x = (closest.transform.position.x);
			tempActiveIcon.position.z = (closest.transform.position.z);
			activeIcon.transform.position.x = tempActiveIcon.position.x;
			activeIcon.transform.position.y = tempActiveIcon.position.y;
			activeIcon.transform.position.z = tempActiveIcon.position.z;

		}
		else
		{     
			activeIcon.gameObject.SetActive (false);
		}
	*/

		if(Input.GetKeyDown (KeyCode.G))
		{       
			//Looks for the closest enemy
			FindClosestEnemy();
			locked = !locked;
		}
		if(locked) 
		{
			//If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
			if (!closest)
			{       
				activeIcon.gameObject.SetActive (false);
				locked = false;
				closest = null;
			}
			//if(Input.GetKeyDown (KeyCode.G))
				player.transform.LookAt(new Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
			//player.transform.LookAt (Vector3(1,2,3));
		}

	}


	GameObject FindClosestEnemy () 
	{
		// Find all game objects with tag Enemy
		enemyLocations = GameObject.FindGameObjectsWithTag("Enemy"); 
		//var closest : GameObject; 
		var distance = Mathf.Infinity; 
		var position = transform.position; 
		// Iterate through them and find the closest one
		foreach (GameObject go in enemyLocations) 
		{ 
			var diff = (go.gameObject.transform.position - position);
			var curDistance = diff.sqrMagnitude; 


			if (curDistance < distance) 
			{ 
				closest = go; 
				distance = curDistance; 
			} 
		} 
		return closest; 

	}
}
