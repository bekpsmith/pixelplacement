using UnityEngine;
using System.Collections;

public class ColliderTouchHandler {
	
	public static event System.Action<RaycastHit> OnTouch;
	
	public static void HandleTouch( RaycastHit hit ){
		if ( OnTouch != null ) {
			OnTouch( hit );
		}
	}
	
}
