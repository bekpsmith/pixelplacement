//InfiniteGround: Scrolls a plane's UVs to give the illusion of an infinite background scroll.
//Parent a plane to a camera, ensure it covers everything the camera sees.  Move the camera as needed.
//Author: Pixelplacement
//Usage: Component
//Methods: None

#pragma strict

var target: Transform; 
var targetInit : Vector3;

function Start(){
 target = gameObject.transform.parent;	
 targetInit = target.position;
}

function Update() {
	var difference=targetInit - target.position;
	renderer.material.mainTextureOffset = difference;
}
