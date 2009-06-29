#pragma strict

//Vars:
private var obj : GameObject;
obj = gameObject;
var target : Transform;

//Look at target:
function Update() {
	obj.transform.LookAt(target);
}