using UnityEngine;
using System.Collections;

public class OrganismB : MonoBehaviour
{
	float dampen = .98f;
	float vx, vy, vz, vrx, vry, vrz;
	float randomRange = .0005f;
	Transform _transform;
	
	void Start(){
	 _transform = transform;	
	}

	void Update(){
		vx+=Random.Range(-randomRange,randomRange);
		vy+=Random.Range(-randomRange,randomRange);
		vz+=Random.Range(-randomRange,randomRange);
		vrx+=Random.Range(-randomRange,randomRange);
		vry+=Random.Range(-randomRange,randomRange);
		vrz+=Random.Range(-randomRange,randomRange);	
	
		//random position:
		Vector3 newPos = _transform.position;
		newPos.x+=vrx;
		newPos.y+=vry;
		newPos.z+=vrz;
		_transform.position = newPos;
		
		//random rotation:
		Quaternion newRot = _transform.rotation;
		newRot.x+=vrx;
		newRot.y+=vry;
		newRot.z+=vrz;
		_transform.rotation = newRot;

		vx *= dampen;
		vy *= dampen;
		vz *= dampen;
		vrx *= dampen;
		vry *= dampen;
		vrz *= dampen;
	}
}
