using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMachine : MonoBehaviour {
#region Singleton
    public static CameraMachine instance;

    private void Awake()
    {
        instance = this;
    }
#endregion
    public Transform target;
    public Transform defaultTarget;
    float distance;
    float startTime;
    float speed = 1f;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        target = defaultTarget;
	}
	
	// Update is called once per frame
	void Update () {
        CameraDontRotate();
    }

    void CameraDontRotate()
    {
        distance = target.position.y * .49f;
        Vector3 movePosition = new Vector3(target.position.x, target.position.y, target.position.z - distance);
        float journeyLength = Vector3.Distance(transform.position, movePosition);
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(transform.position, movePosition, fracJourney);

    }

}
