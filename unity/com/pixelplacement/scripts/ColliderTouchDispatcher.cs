//ToDo: Add additional events: onReleaseOutside, onRelease, onReleaseOver

using UnityEngine;
using System.Collections;

public class ColliderTouchDispatcher : MonoBehaviour {
	
	public static event System.Action<RaycastHit> OnTouch;
	public Camera renderingCamera;
	public bool useTouch = true;
	public bool useMouse = true;
	static ColliderTouchDispatcher _instance = null;
	
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
						if ( OnTouch != null ) {
							OnTouch( rayCastHit );
						}	
					}
				}
			}
		}
		
		if ( useMouse ) {	
			if ( Input.GetMouseButtonDown( 0 ) ) {
				Ray ray = renderingCamera.ScreenPointToRay( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
				RaycastHit rayCastHit;
				if ( Physics.Raycast( ray, out rayCastHit, renderingCamera.farClipPlane ) ) {
					if ( OnTouch != null ) {
						OnTouch( rayCastHit );
					}	
				}
			}
		}
	}
}