#pragma strict

public static var driving : boolean;
public static var reversing : boolean;
private var rotSpeed : int = 460;
private var maxSpeed : int = 11;
private var speed : float =0;
private var acceleration : float =.3;
var car : GameObject;
var mainCamera : Camera;

private var deadZone : float = .06;

function Start():void{
	car = gameObject;
	car.rigidbody.mass = 1;
	car.rigidbody.drag = 5;
	car.rigidbody.angularDrag = 10;
	mainCamera=Camera.main;
}

function Update():void{
	//Driving boolean setting:
	if(iTouch.OnFingerHeld("Gas")){
		driving = true;
	}else{
		driving = false;	
	}
	
	//Reverse boolean setting:
	if(iTouch.OnFingerHeld("Brake")){
		reversing = true;
	}else{
		reversing = false;	
	}
	
	//Speed calculations:
	if(driving){
		speed+=acceleration;
		if(speed>maxSpeed){
			speed=maxSpeed;
		}
	}
	
	if(reversing){
		speed-=acceleration;
		if(speed<-maxSpeed){
			speed=-maxSpeed;
		}
	}
	
	if(speed>0 && !driving){
		speed-=acceleration;
		if(speed<0){
			speed=0;
		}
	}
	
	if(speed<0 && !reversing){
		speed+=acceleration;
		if(speed>0){
			speed=0;
		}
	}
	
	speedPercentage=speed/maxSpeed;
	
	//Driving application:
	//transform.Translate(Vector3.forward * (Time.deltaTime*speed));
	
	//Steering application:
	if(Mathf.Abs(rigidbody.velocity.x)>0 || Mathf.Abs(rigidbody.velocity.z)>0){
		if(driving){
			car.transform.Rotate(0, ((iPhoneRotation.getRotationY(deadZone)*rotSpeed)*-Time.deltaTime)*speedPercentage, 0);
		}
		
		if(reversing){
			car.transform.Rotate(0, (((iPhoneRotation.getRotationY(deadZone)*rotSpeed)*-Time.deltaTime)*speedPercentage)*-1, 0);
		}	
	}
}

function FixedUpdate(){
	//Driving force application:
	if(driving){
		car.rigidbody.AddRelativeForce(Vector3.forward*speed);
	}
	
	if(reversing){
		car.rigidbody.AddRelativeForce(Vector3.forward*speed);
	}
	
//Camera follow:
	mainCamera.transform.position.x=Mathf.Lerp(mainCamera.transform.position.x,car.transform.position.x,Time.deltaTime*8);
	mainCamera.transform.position.z=Mathf.Lerp(mainCamera.transform.position.z,car.transform.position.z,Time.deltaTime*8);		
}
