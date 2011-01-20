using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{
	public Transform target;
	float moveSpeed = 20;
	float rotationSpeed = 1;
	float seekThreshold = 6;
	float distanceThreshold = 1;
	float timeToLive = 3;
	float launchTime;
	float overTimeIncrement = .3f;
	Vector3 startingPosition;
	bool seek, arrived;
	bool flatMovement = true;
	
	void Start(){
		launchTime = Time.time;
		startingPosition = transform.position;	
	}

	void Update (){
		if (arrived) {
			return;
		}
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		if (seek) {
			if (Time.time-launchTime > timeToLive) {
				rotationSpeed+=overTimeIncrement;
			}
			transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
			if (flatMovement) {
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}			
		}else if (Vector3.Distance(transform.position,startingPosition) > seekThreshold) {
			seek=true;
		}
		
		if (Vector3.Distance(transform.position,target.position) <= distanceThreshold) {
			arrived=true;
		}
	}
}