using UnityEngine;
using System.Collections;

public class ColliderTouchDispatcher : MonoBehaviour {
	
	public Camera renderingCamera;
	public bool useTouch = true;
	public bool useMouse = true;
	static ColliderTouchDispatcher _instance;
	
	void Awake(){
		//duplicate instance check:
		if ( _instance != null ) {
			Destroy( this );
			Debug.LogError( "ColliderTouchDispatcher error! You should only have one instance of ColliderTouchDispatcher in your scene!" );
		}		
		
		if ( renderingCamera == null ) {
			renderingCamera = Camera.main;
		}
		
		_instance = this;
	}

	void Update () {
		if ( useTouch ) {
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
		
		if ( useMouse ) {	
			if ( Input.GetMouseButton( 0 ) ) {
				Ray ray = renderingCamera.ScreenPointToRay( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
				RaycastHit rayCastHit;
				if ( Physics.Raycast( ray, out rayCastHit, renderingCamera.farClipPlane ) ) {
					Debug.Log( rayCastHit.collider.name );	
				}
			}
		}
	}
}
