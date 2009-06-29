#pragma strict

//Vars:
var target: Renderer;
private var obj: GameObject;
obj=gameObject;
private var initBounds : Bounds; 
initBounds = target.bounds;
private var boundPercent : Vector3;
private var initXScale : float;
initXScale = gameObject.transform.localScale.x;
private var initYScale : float;
initYScale = gameObject.transform.localScale.y;
private var initZScale : float;
initZScale = gameObject.transform.localScale.z;

//Adjust width and height of shadow to mimic visible size of target to create a quasi-accurate drop shadow:
function Update () {
	boundPercent = Vector3(target.bounds.size.x/initBounds.size.x, target.bounds.size.y/initBounds.size.y, target.bounds.size.z/initBounds.size.z);
	obj.transform.localScale.x =initXScale *  boundPercent.x;
	obj.transform.localScale.y =initXScale *  boundPercent.y;
	obj.transform.localScale.z =initXScale *  boundPercent.z;
}