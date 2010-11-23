using UnityEngine;
using System.Collections;

public class SmoothDrag : MonoBehaviour {
	float distance = 0;
	Vector3 targetPosition = Vector3.zero;
	Transform dragObject;
	Vector3 offset;
	
	void Update () {		
		if(Input.touchCount>0){
			
			//set the object to be dragged or clear out a previouslt dragged object:
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Moveable"){
					distance = hit.distance;
					dragObject = hit.collider.transform;
					offset = dragObject.position -  new Vector3(hit.point.x, hit.point.y, dragObject.position.z);
					Debug.Log(offset);
					targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distance));
				}else{
					dragObject = null;
				}
			}
			
			//recalcualte the target position if we detect a finger movement:
			if(Input.GetTouch(0).phase == TouchPhase.Moved){
				targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distance));
			}			
		}
		
		//update the target object's position at a speed of 6 with iTween's Vector3Update calculation for real-time easing:
		if(dragObject){
			dragObject.position = iTween.Vector3Update(dragObject.position, new Vector3(targetPosition.x + offset.x,targetPosition.y + offset.y,dragObject.position.z), 6);
		}
	}

}