#pragma strict

//Vars:
static var phase : iPhoneTouch;
static var touch : iPhoneTouch;
static var position : Vector2;
static var touched : boolean;

//Check for touch:
function Update () {
	for (touch in iPhoneInput.touches) {
		phase = iPhoneInput.touches[0];
		position=phase.position;
	}
}

//Check for single hit:
static function onFingerTouch(rect:Rect):boolean{
	if(phase.phase==iPhoneTouchPhase.Began && iPhoneInput.touchCount>0 && phase.position.x >0 && phase.position.y >0){
		if (rect.Contains(phase.position)){
			touched=true;
        	return(true);
		}else{
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
			touched=true;
			return(true);
		}
	}else{
		return(false);	
	}
}

//Check for released:
static function onFingerUp():boolean{
	if(phase.phase==iPhoneTouchPhase.Ended && touched){
		touched=false;
		return(true);
	}else{
		return(false);	
	}
}