using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Ford/Touch Manager")]
public class TouchManager : MonoBehaviour{
	
	public bool AllowMouse;
	
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
						hit.collider.SendMessage("PlayVideo", SendMessageOptions.DontRequireReceiver);
						Debug.Log("Touch!");
					}
				}
			}
			
			if (AllowMouse && Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				Ray ray =  mainCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 100)){
					hit.collider.SendMessage("PlayVideo", SendMessageOptions.DontRequireReceiver);
					Debug.Log("Click!");
				}
			}
			
			yield return null;
		}
	}	
}