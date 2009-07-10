#pragma strict

//Vars:
static var phase : iPhoneTouch;
static var touch : iPhoneTouch;
static var position : Vector2;

//Check for touch:
function Update () {
	for (touch in iPhoneInput.touches) {
		phase = iPhoneInput.touches[0];
	}
}

//Check for single hit:
static function onFingerDown(rect:Rect):boolean{
	if(phase.phase==iPhoneTouchPhase.Began && iPhoneInput.touchCount>0 && phase.position.x >0 && phase.position.y >0){
		if (rect.Contains(phase.position)){
			position=phase.position;
        	return(true);
		}else{
			position=Vector2(-1,-1);
			return(false);	
		}
	}else{
		return(false);
	}
}

//Check for held/dragged over hit:
static function onFingerHeld(rect:Rect):boolean{
	if (rect.Contains(phase.position)){
		if(phase.phase==iPhoneTouchPhase.Stationary || phase.phase==iPhoneTouchPhase.Moved && iPhoneInput.touchCount>0){
			position=phase.position;
			return(true);
		}
	}else{
		position=Vector2(-1,-1);
		return(false);	
	}
}