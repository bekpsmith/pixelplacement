using UnityEngine;
using System.Collections;

public class QuickTest : MonoBehaviour
{

	void Start (){
		StartCoroutine(LookForTouch());
	}
	
	IEnumerator LookForTouch(){
		Camera mainCamera = Camera.main;
		bool active = true;
		
		while (active) {
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					RaycastHit hit;
					Ray ray =  mainCamera.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out hit, 100)){
						GameObject touched = hit.collider.gameObject;
						active = false;
						break;
					}
				}
			}
			yield return null;
		}
		
		Debug.Log("Touched");
	}
}