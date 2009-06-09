//iTouchGUI: Global button method for GUITextures and rectangle objects.
//Author: Pixelplacement
//Usage: Component, Static
//Methods: OnFingerDown, OnFingerHeld

//Example:
/*
if(iTouchGUI.OnFingerHeld(guiTexture.GetScreenRect())){
	print("Down");
	guiTexture.color=Color(.2,.2,.2,.5);
}else{
	guiTexture.color=Color(.5,.5,.5,.5);	
}
*/

#pragma strict

//Init vars:
static var phase : iPhoneTouch;
static var touch : iPhoneTouch;
static var touchLocation : Vector2;

//Check for touch:
function Update () {
	for (touch in iPhoneInput.touches) {
		phase = iPhoneInput.touches[0];
	}
}

//Check for single hit:
static function OnFingerDown(rect:Rect):boolean{
	if(phase.phase==iPhoneTouchPhase.Began && iPhoneInput.touchCount>0){
		if (rect.Contains(phase.position)){
			touchLocation=phase.position;
        	return(true);
		}else{
			touchLocation=Vector2(-1,-1);
			return(false);	
		}
	}else{
		return(false);
	}
}

//Check for held/dragged over hit:
static function OnFingerHeld(rect:Rect):boolean{
	if (rect.Contains(phase.position)){
		if(phase.phase==iPhoneTouchPhase.Stationary || phase.phase==iPhoneTouchPhase.Moved){
			touchLocation=phase.position;
			return(true);
		}
	}else{
		touchLocation=Vector2(-1,-1);
		return(false);	
	}
}