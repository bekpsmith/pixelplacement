using UnityEngine;
using System.Collections;

public enum Swipe{Left,Right,Up,Down};

public class SwipeDetection : MonoBehaviour {
	float screenDiagonalSize;
	float minSwipeDistancePixels;
	bool touchStarted;
	Vector2 touchStartPos;
	public float minSwipeDistance = .1f;
	public static event System.Action<Swipe> OnSwipeDetected;
	
	void Start() {
		screenDiagonalSize = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
		minSwipeDistancePixels = minSwipeDistance * screenDiagonalSize;	
	}
	
	void Update() {
		if (Input.touchCount > 0) {
				var touch = Input.touches[0];
				
				switch (touch.phase) {
				
				case TouchPhase.Began:
					touchStarted = true;
					touchStartPos = touch.position;
					break;
					
				case TouchPhase.Ended:
					if (touchStarted) {
						TestForSwipeGesture(touch);
						touchStarted = false;
					}
					break;
					
				case TouchPhase.Canceled:
					touchStarted = false;
					break;
					
				case TouchPhase.Stationary:
					break;

				case TouchPhase.Moved:
					break;
			}
		}		
	}
	
	void TestForSwipeGesture(Touch touch){
			if (OnSwipeDetected == null) {
				return;
			}
		
			Vector2 lastPos = touch.position;
			float distance = Vector2.Distance(lastPos, touchStartPos);
			
			if (distance > minSwipeDistancePixels) {
				float dy = lastPos.y - touchStartPos.y;
				float dx = lastPos.x - touchStartPos.x;
				
				float angle = Mathf.Rad2Deg * Mathf.Atan2(dx, dy);
				
				angle = (360 + angle - 45) % 360;
				
				if (angle < 90) {
					OnSwipeDetected(Swipe.Right);
				} else if (angle < 180) {
					OnSwipeDetected(Swipe.Down);
				} else if (angle < 270) {
					OnSwipeDetected(Swipe.Left);
				} else {
					OnSwipeDetected(Swipe.Up);
				}
		}
	}
}
