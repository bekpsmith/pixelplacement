using UnityEngine;
using System.Collections;

public class ColliderTouchDispatcher : MonoBehaviour {
	
	public Camera renderingCamera;
	
	void Awake(){
		if ( renderingCamera == null ) {
			renderingCamera = Camera.main;
		}
	}

	void Update () {
#if UNITY_EDITOR
		if ( Input.GetMouseButton( 0 ) ) {
			Ray ray = renderingCamera.ScreenPointToRay( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
			RaycastHit rayCastHit;
			if ( Physics.Raycast( ray, out rayCastHit, renderingCamera.farClipPlane ) ) {
				Debug.Log( rayCastHit.collider.name );	
			}
		}
#endif
		
		foreach ( Touch touch in Input.touches ) {
			if ( touch.phase == TouchPhase.Began ) {
				Ray ray = renderingCamera.ScreenPointToRay( new Vector3( touch.position.x, touch.position.y, 0 ) );
				RaycastHit rayCastHit;
				if ( Physics.Raycast( ray, out rayCastHit, renderingCamera.farClipPlane ) ) {
					Debug.Log( rayCastHit.collider.name );	
				}
			}
		}
	}
}
