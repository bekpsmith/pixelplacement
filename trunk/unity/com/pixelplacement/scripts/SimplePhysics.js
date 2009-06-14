//SimplePhysics: Old-school, stripped down, box constrained physics.
//Author: Pixelplacement
//Methods: step, adjustEnvironment, pause, resume, applyXForce, applyYForce

//To do: Look into integration with colliders to allow surfaces.

/*
Example:

var coinPhysics : SimplePhysics;
coinPhysics = new SimplePhysics(gameObject,-1.3,2.15,1.3,-2.15,true);

StartCoroutine("listeners");

function listeners () {
	while (true) {
		coinPhysics.step();
		if(coinPhysics.sleeping){
			StopCoroutine("listeners");
			print("I'm done bouncing");
		}
		yield;
	}
}
*/

class SimplePhysics{
	//Walls
	private var leftWall : float;
	private var ceiling : float;
	private var rightWall : float;
	private var floor : float;
	private var lid : boolean;
	private var obj : GameObject;
	
	//Environment
	private var gravity : float = 2;
	private var bounce : float = 0.5;
	private var resistance : float = .06;
	private var physicsStep : float = 400;
	
	//State
	var sleeping: boolean;
	var paused : boolean;
	private var deltaX : float;
	private var deltaY : float;
	
	//Forces
	var speedX : float = 0;
	var speedY : float = 0;
	
	//Old Forces
	private var oldSpeedX : float = 0;
	private var oldSpeedY : float = 0;
	
	function SimplePhysics(o: GameObject, lw: float, c: float, rw: float,f: float, l: boolean){
		obj=o;
		leftWall=lw;
		ceiling=c;
		rightWall=rw;
		floor=f;
		lid=l;
	}
	
	function step(){	
		if(!paused){	
			speedY -= gravity;
			
			if(speedX > 0){
				speedX -= resistance;
				if(speedX<0){
					speedX=0;
				}
			}else{
				speedX += resistance;
				if(speedX>0){
					speedX=0;
				}		
			}
			
			obj.transform.position.x += speedX/physicsStep;
			obj.transform.position.y += speedY/physicsStep;
			
			if (obj.transform.position.y > ceiling && lid) {
				obj.transform.position.y = ceiling;
				speedY *= -bounce;
			}
			
			if (obj.transform.position.y < floor) {
				obj.transform.position.y = floor;
				speedY *= -bounce;
			}
			
			if (obj.transform.position.x < leftWall) {
				obj.transform.position.x = leftWall;
				speedX *= -bounce;
			}
			
			if (obj.transform.position.x > rightWall) {
				obj.transform.position.x = rightWall;
				speedX *= -bounce;
			}
			
			deltaX=(oldSpeedX-speedX)+1;
			deltaY=(oldSpeedY-speedY)+1;
						
			if(deltaX == 1 && deltaY == 1){
				sleeping=true;
			}else{
				sleeping=false;
			}
			
			oldSpeedX=speedX;
			oldSpeedY=speedY;			
		}
	}
	
	function adjustEnvironment(g: float, b: float, r: float, ps: float){
		gravity = g;
		bounce = b;
		resistance = r;
		physicsStep = ps;		
	}
	
	function pause(){
		paused = true;
	}	
	
	function resume(){
		paused = false;
	}		
	
	function applyXForce(magnitude: float){
		speedX += magnitude;
	}		
	
	function applyYForce(magnitude: float){
		speedY += magnitude;
	}	
}