//iPhoneRotation: Retrieves iPhone rotation axis and stores static variables in range from -1 to 1 including a dead zone value.
//Author: Pixelplacement
//Usages: Component, Static
//Methods: getRotationX, getRotationY

#pragma strict

//Init vars:
public static var rotationX:float;
public static var rotationY:float;

//Constant retrieval
function Update(){
	rotationX = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.x, -1, 1)*10))/10;
	rotationY = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.y, -1, 1)*10))/10;
}

//Static retrievals
public static function getRotationX(deadZone:float):float{
	if(deadZone>=1){
		Debug.LogError("ERROR: Dead zone must be less than 1!");
		return;
	}else{
		rotationX = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.x, -1, 1)*10))/10;
		rotationX = rotationX-deadZone;
		if(rotationX>=0){
			rotationX = (rotationX-deadZone)/(1-deadZone);
			if(rotationX<0){
				rotationX=0;
			}
		}else{
			rotationX = (rotationX+deadZone)/(1-deadZone);
			if(rotationX>0){
				rotationX=0;
			}
		}
		return(rotationX);
	}
}

public static function getRotationY(deadZone:float):float{
	if(deadZone>=1){
		Debug.LogError("ERROR: Dead zone must be less than 1!");
		return;	
	}else{
		rotationY = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.y, -1, 1)*10))/10;
		if(rotationY>=0){
			rotationY = (rotationY-deadZone)/(1-deadZone);
			if(rotationY<0){
				rotationY=0;
			}
		}else{
			rotationY = (rotationY+deadZone)/(1-deadZone);
			if(rotationY>0){
				rotationY=0;
			}
		}
		return(rotationY);
	}
}