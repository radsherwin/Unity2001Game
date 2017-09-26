using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class PlayerManager : MonoBehaviour {
	private Rigidbody rigidBody;

	//Control
	public float movementSpeed;
	public float lookSpeed = 10;
	private Vector3 curLoc;
	private Vector3 prevLoc;

	Vector3 movement;


	[HideInInspector]
	public bool canMove = true;

	private void Start(){
		rigidBody = GetComponent<Rigidbody> ();
	}

    private void FixedUpdate()
    {
		if(canMove)
			ControllPlayer (); 
    }
		
	void ControllPlayer()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		transform.Translate (movement * movementSpeed * Time.deltaTime, Space.World);

		prevLoc = curLoc;
		curLoc = transform.position;

		if (moveHorizontal < 0) {
			curLoc.x -= 1 * Time.fixedDeltaTime;
			ORK.Game.Variables.Set ("movementHorizontal", moveHorizontal.ToString ());
		}
		if (moveHorizontal > 0) {
			curLoc.x += 1 * Time.fixedDeltaTime;
			ORK.Game.Variables.Set ("movementHorizontal", moveHorizontal.ToString ());
		}
		if (moveVertical > 0) {
			curLoc.z += 1 * Time.fixedDeltaTime;
		}
		if (moveVertical < 0) {
			curLoc.z -= 1 * Time.fixedDeltaTime;
		}
		transform.position = curLoc;
		if (movement.magnitude > .0001) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);

		}

	}
		

}
