using UnityEngine;
using System.Collections;

public class OrganismC : MonoBehaviour
{
	float speed = 1f;
	Vector3 range = Vector3.one;
	Vector3 position;
	Transform _transform;
	Vector3 initPos;
	Quaternion initRot;

	void Start()
	{
		_transform = transform;
		initPos = transform.position;
		initRot = transform.rotation;
	}
	
	void Update () {
		_transform.position = initPos + Vector3.Scale(SmoothRandom.GetVector3(speed), range);
		Vector3 randomRot = Vector3.Scale(SmoothRandom.GetVector3(speed), range);
		Quaternion newRot = new Quaternion();
		newRot.x = initRot.x + randomRot.x;
		newRot.y = initRot.y + randomRot.y;
		newRot.z = initRot.z + randomRot.z;
		_transform.rotation = newRot;
	}
}