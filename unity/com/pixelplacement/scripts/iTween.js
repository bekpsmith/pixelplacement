//iTween: Easy movement, rotation and fading of GUITextures and meshes.
//If using fadeTo on a mesh, make sure it has a shader that supports transparency.
//rotateTo will not a work on a GUITexture since it doesn't have rotation.
//iTween calls can be staked with delays to chain animations.
//Author: Pixelplacement
//Usage: Static
//Methods: fadeTo,moveTo,rotateTo

//Example:
/*
fadeTo(obj: GameObject,beginA: float, endA: float,duration: float,delay: float)
iTween.fadeTo(gameObject,1,0,1,0);

moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float)
iTween.moveTo(gameObject,.2,.2,.2,1,0);

rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float)
iTween.rotateTo(gameObject,0,0,270,10,1);
*/

#pragma strict
//GLOBAL ANIMATION DURATION NEEDS WORK!!

//Init vars:
static var registers : Array = new Array();
static var params : Array = new Array();
var id : int;

//moveTo registration function:
static function moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){
	if(duration<0){
		Debug.LogError("ERROR: Duration property must be greater than or equal to 0!");
        return;
	}
	if(delay<0){
		Debug.LogError("ERROR: Delay property must be greater than or equal to 0!");
        return;
	}
	obj.AddComponent ("iTween");
	registers.push(obj);
	//Adjust correct length of animation time!
	params.push(new Array(x,y,z,duration,delay,"move"));
}

//fadeTo registration function:
static function fadeTo(obj: GameObject,beginA: float, endA: float,duration: float,delay: float){
	if(duration<0){
		Debug.LogError("ERROR: Duration property must be greater than or equal to 0!");
        return;
	}
	if(delay<0){
		Debug.LogError("ERROR: Delay property must be greater than or equal to 0!");
        return;
	}
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(beginA,endA,duration,delay,"fade"));
}

//rotateTo registration function:
static function rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){
	if(duration<0){
		Debug.LogError("ERROR: Duration property must be greater than or equal to 0!");
        return;
	}
	if(delay<0){
		Debug.LogError("ERROR: Delay property must be greater than or equal to 0!");
        return;
	}
	obj.AddComponent ("iTween");
	registers.push(obj);
	//Adjust correct length of animation time!
	params.push(new Array(x,y,z,duration,delay,"rotate"));
}

//Applied component launcher and loops:
function Start(){
	findRegister();
	var paramList : Array = params[id];
	registers.RemoveAt(id);
	params.RemoveAt(id);
	if(paramList[paramList.length-1]=="move"){
		yield move(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}
	if(paramList[paramList.length-1]=="fade"){
		yield fade(paramList[0],paramList[1],paramList[2],paramList[3]);
	}
	if(paramList[paramList.length-1]=="rotate"){
		yield rotate(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}
	
	Destroy(this);
}

//Move applied code:
function move (x : float, y: float, z: float, duration: float, delay: float) {
	yield WaitForSeconds (delay);	
	obj = gameObject.transform;
	target = Vector3(x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		xAug=Mathf.SmoothStep(obj.position.x, x, i);
		yAug=Mathf.SmoothStep(obj.position.y, y, i);
		zAug=Mathf.SmoothStep(obj.position.z, z, i);
		obj.position=Vector3(xAug,yAug,zAug);
		yield;
	}
	obj.position=Vector3(x,y,z);	
}

//Fade applied code:
function fade (beginA : float, endA: float, duration: float, delay: float) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	if(guiTexture){
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			guiTexture.color.a=Mathf.Lerp(beginA, endA, i);
			yield;
		}
		guiTexture.color.a= endA;
	}else{
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			renderer.material.color.a=Mathf.Lerp(beginA, endA, i);
			yield;
		}
		renderer.material.color.a= endA;
	}
}

//Rotate applied code:
function rotate (x : float, y: float, z: float, duration: float, delay: float) {
	if(guiTexture){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	targetRot = Quaternion.Euler (x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		obj.rotation = Quaternion.Slerp(obj.rotation, targetRot, i);
		yield;
	} 
	obj.rotation = targetRot;
}

//Registers ID lookup:
function findRegister(){
	for(i = 0; i < registers.length; i++){
		if(registers[i]==gameObject){
			id =  i;
		}
	}
}