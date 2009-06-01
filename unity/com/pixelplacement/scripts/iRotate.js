//iRotate: Simple script to rotate an object on a single axis.
//Author: Pixelplacement
//Usage: Component
//Methods: None

var speed : float;
var axis : String;

function Update () {
	if(axis == "X" || axis == "x"){
		transform.Rotate(Time.deltaTime*speed,0,0);
	}
	if(axis == "Y" || axis == "y"){
		transform.Rotate(0,Time.deltaTime*speed,0);
	}
	if(axis == "Z" || axis == "z"){
		transform.Rotate(0,0,Time.deltaTime*speed);
	}
	
}