//iPhoneRotation: Retrieves iPhone rotation axis and stores static variables in range from -1 to 1.
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
public static function getRotationX():float{
	rotationX = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.x, -1, 1)*10))/10;
	return(rotationX);
}

public static function getRotationY():float{
	rotationY = (Mathf.Floor(Mathf.Clamp(iPhoneInput.acceleration.y, -1, 1)*10))/10;
	return(rotationY);
}
