using UnityEngine;
using System.Collections;

public class OrganismA : MonoBehaviour
{	
	float positionDeviation = .3f;
	float rotationDeviation = 18f;
	
	void Start(){	
		iTween.ShakePosition(gameObject, new Vector3(positionDeviation, positionDeviation, positionDeviation), float.MaxValue);
		iTween.ShakeRotation(gameObject, new Vector3(rotationDeviation, rotationDeviation, rotationDeviation), float.MaxValue);
	}
}
