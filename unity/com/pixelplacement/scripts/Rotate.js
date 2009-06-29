#pragma strict

//Vars:
var speed : float;
var axis : String;
private var obj: GameObject;
obj=gameObject;

//Rotate object based on selected axis and speed:
function Update () {
	if(axis == "X" || axis == "x"){
		obj.transform.Rotate(Time.deltaTime*speed,0,0);
	}
	if(axis == "Y" || axis == "y"){
		obj.transform.Rotate(0,Time.deltaTime*speed,0);
	}
	if(axis == "Z" || axis == "z"){
		obj.transform.Rotate(0,0,Time.deltaTime*speed);
	}
	if(axis == ""){
		Debug.LogError("ERROR: Please indicate an axis!",this);
        enabled = false;
        return;
	}
}