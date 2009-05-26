//iTouch: Global iPhone touch manager for single touch handling on meshes with colliders.
//If you fail to get a response, ensure the mesh has a collider attached.
//Author: Pixelplacement
//Usage: Component, Static
//Methods: OnFingerDown, OnFingerHeld

//Example:
/*
if(iTouch.OnFingerDown("GasPedal")){
		print("Down!");
	}
	
	if(iTouch.onFingerHeld(gameObject.name)){
		print("Held");
	}
*/

#pragma strict

//Init vars:
static var touching : String;
static var phase : iPhoneTouchPhase;

//Seek single touch and set 'touching' to actively touched object:
function Update(){
	for (var touch : iPhoneTouch in iPhoneInput.touches) {
		phase = iPhoneInput.touches[0].phase;
		var ray : Ray = Camera.main.ScreenPointToRay (iPhoneInput.touches[0].position);
		//Debug.DrawRay (ray.origin, ray.direction, Color.yellow);
		var hit : RaycastHit;
		if (Physics.Raycast (ray, hit, 100)){
			touching = hit.collider.gameObject.name;
		}else{
			touching=null;	
		}
		if(iPhoneInput.touches[0].phase==iPhoneTouchPhase.Ended){
			touching = null;
		}
	}
}

//Check for single hit:
static function OnFingerDown(target:String):boolean{
	if(phase==iPhoneTouchPhase.Began){
		if(touching==target){
			return true;
		}else{
			return false;
		}
	}
}

//Check for held/dragged over hit:
static function OnFingerHeld(target:String):boolean{
	if(touching == target){
		return true;
	}else{
		return false;	
	}
}
