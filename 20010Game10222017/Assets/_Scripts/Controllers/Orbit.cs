using UnityEngine;
using System.Collections;
public class Orbit : MonoBehaviour
{
	Animator anim;

	private GameObject player;
	private Rigidbody rgbd;
	private bool lockedON = false;
    Transform interactableTarget;
	GameObject[] enemyLocations;
	GameObject closest;
	public float lockOnDistance;
	public float moveHorizontal, moveVertical;
    float buttonCoolDown;

    bool canTarget;

	void Start()
	{
		anim = GetComponent<Animator> ();
        player = this.gameObject;
		rgbd = GetComponent<Rigidbody> ();
		// Make the rigid body not change rotation
		if (rgbd)
			rgbd.freezeRotation = true;
	}
		


	void Update()
	{
        buttonCoolDown -= Time.deltaTime;

        canTarget = IsPaused();

        if(canTarget)
            Controls();	

    }

    void Controls()
    {
        if (Input.GetButtonDown("Target"))
        {
            ClosestEnemy();
            lockedON = !lockedON;

        }

        if (lockedON)
        {
            if(Chest.instance != null)
                Chest.instance.canUseChest = false;
            anim.SetBool("strafe", true);
            LockOnToEnemy();

        }
        else
        {
            if (Chest.instance != null)
                Chest.instance.canUseChest = true;
            anim.SetBool("strafe", false);
        }
    }

    bool IsPaused()
    {
        if (Time.timeScale == 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //LockOn to closest Enemy
    void LockOnToEnemy(){

		if(lockedON)
		{
			//If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
			if (!closest)
			{      
				//activeIcon.active = false;
				lockedON = false;
				closest = null;
                anim.SetBool("strafe", false);
			}
            
            if (closest != null)
            {
                transform.LookAt(new Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
               // transform.rotation = Quaternion.RotateTowards(transform.rotation, closest.transform.rotation, 5f * Time.deltaTime);
            }

            if (Input.GetButtonDown("Fire2") && buttonCoolDown < 0)
            {
                anim.SetTrigger("roll back");
                buttonCoolDown = .6f;

            }
				
			
		}
        else
        {
            anim.SetBool("strafe", false);
        }
        
	}

    public void LockOnToInteractable(Interactables newTarget)
    {
        interactableTarget = newTarget.interactionTransform;
        if (interactableTarget != null)
        {
            transform.LookAt(new Vector3(interactableTarget.transform.position.x, transform.position.y, interactableTarget.transform.position.z));
        }
    }

    public void StopLockingOnToInteractable()
    {
        interactableTarget = null;
    }
		

	//Find ClosestEnemy
	private GameObject ClosestEnemy(){
		// Find all game objects with tag Enemy
		enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyLocations != null)
        {
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
        }
		return closest;
	}


}