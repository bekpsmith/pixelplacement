#pragma strict

//Vars:
private var rotSpeed : int = 460;
private var maxSpeed : int = 15;
private var speed : float =0;
private var acceleration : float =.3;
private var mainCamera : Camera;
private var deadZone : float = .06;
mainCamera=Camera.main;

//Driving work:
function Update():void{
	
	//Speed calculations:
	if(GlobalVars.driving){
		speed+=acceleration;
		if(speed>maxSpeed){
			speed=maxSpeed;
		}
	}
	
	if(GlobalVars.reversing){
		speed-=acceleration;
		if(speed<-maxSpeed){
			speed=-maxSpeed;
		}
	}
	
	if(speed>0 && !GlobalVars.driving){
		speed-=acceleration;
		if(speed<0){
			speed=0;
		}
	}
	
	if(speed<0 && !GlobalVars.reversing){
		speed+=acceleration;
		if(speed>0){
			speed=0;
		}
	}
	
	speedPercentage=speed/maxSpeed;
	
	//Steering application:
	if(Mathf.Abs(GlobalVars.activeCarGameObject.rigidbody.velocity.x)>0 || Mathf.Abs(GlobalVars.activeCarGameObject.rigidbody.velocity.z)>0){
		if(GlobalVars.driving){
			GlobalVars.activeCarGameObject.transform.Rotate(0, ((iPhoneRotation.getRotationY(deadZone)*rotSpeed)*-Time.deltaTime)*speedPercentage, 0);
		}
		
		if(GlobalVars.reversing){
			GlobalVars.activeCarGameObject.transform.Rotate(0, (((iPhoneRotation.getRotationY(deadZone)*rotSpeed)*-Time.deltaTime)*speedPercentage)*-1, 0);
		}	
	}
}

//A touch of physics:
function FixedUpdate(){
	//GlobalVars.driving force application:
	if(GlobalVars.driving){
		GlobalVars.activeCarGameObject.rigidbody.AddRelativeForce(Vector3.forward*speed);
	}
	
	if(GlobalVars.reversing){
		GlobalVars.activeCarGameObject.rigidbody.AddRelativeForce(Vector3.forward*speed);
	}
	
	//Camera follow - needs polish:
	mainCamera.transform.position.x=Mathf.Lerp(mainCamera.transform.position.x,GlobalVars.activeCarGameObject.transform.position.x,Time.deltaTime*8);
	mainCamera.main.transform.position.z=Mathf.Lerp(mainCamera.transform.position.z,GlobalVars.activeCarGameObject.transform.position.z,Time.deltaTime*8)-.045;		
}
