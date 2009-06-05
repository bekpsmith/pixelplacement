//iTween: Easy movement, rotation and fading of GUITextures and meshes.
//If using fadeTo on a mesh, make sure it has a shader that supports transparency.
//rotateTo will not a work on a GUITexture since it doesn't have rotation.
//iTween calls can be staked with delays to chain animations.
//Possible ease curves are: linear,easeIn,easeOut, and easeInOut
//Author: Pixelplacement
//Usage: Static
//Methods: fadeTo,fadeFrom,moveTo,moveFrom,rotateTo

//Example:
/*
fadeTo(obj: GameObject,beginA: float, endA: float,duration: float,delay: float,easing: String)
iTween.fadeTo(gameObject,1,0,1,0,"linear");

moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float,easing: String)
iTween.moveTo(gameObject,.2,.2,.2,1,0,"easein");

rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float,easing: String)
iTween.rotateTo(gameObject,0,0,270,10,1,"easeInOut");
*/

#pragma strict

//Init vars:
static var registers : Array = new Array();
static var params : Array = new Array();
var id : int = 0;

//moveTo registration function:
static function moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(x,y,z,duration,delay,easing,"moveTo"));
}

//moveFrom registration function:
static function moveFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(x,y,z,duration,delay,easing,"moveFrom"));
}


//fadeTo registration function:
static function fadeTo(obj: GameObject,endA: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(endA,duration,delay,easing,"fadeTo"));
}

static function fadeFrom(obj: GameObject,endA: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(endA,duration,delay,easing,"fadeFrom"));
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
	params.push(new Array(x,y,z,duration,delay,"rotate"));
}

//Applied component launcher and loops:
function Start(){
	findRegister();
	var paramList : Array = params[id];
	registers.RemoveAt(id);
	params.RemoveAt(id);
	if(paramList[paramList.length-1]=="moveTo"){
		yield moveTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);
	}
	if(paramList[paramList.length-1]=="moveFrom"){
		yield moveFrom(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);
	}
	if(paramList[paramList.length-1]=="fadeTo"){
		yield fadeTo(paramList[0],paramList[1],paramList[2],paramList[3]);
	}
	if(paramList[paramList.length-1]=="fadeFrom"){
		yield fadeFrom(paramList[0],paramList[1],paramList[2],paramList[3]);
	}	
	if(paramList[paramList.length-1]=="rotate"){
		yield rotate(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}
	
	Destroy(this);
}

//Move applied code:
function moveTo(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	start = obj.position;
	end = Vector3(x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			transform.position.x=easeIn(start.x,end.x,i);
			transform.position.y=easeIn(start.y,end.y,i);
			transform.position.z=easeIn(start.z,end.z,i);
		}
		if(easing=="easeOut" || easing=="easeout"){
			transform.position.x=easeOut(start.x,end.x,i);
			transform.position.y=easeOut(start.y,end.y,i);
			transform.position.z=easeOut(start.z,end.z,i);
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			transform.position.x=easeInOut(start.x,end.x,i);
			transform.position.y=easeInOut(start.y,end.y,i);
			transform.position.z=easeInOut(start.z,end.z,i);
		}
		if(easing=="linear"){
			transform.position.x=linear(start.x,end.x,i);
			transform.position.y=linear(start.y,end.y,i);
			transform.position.z=linear(start.z,end.z,i);
		}
		yield;
	}
	obj.position=end;	
}

function moveFrom(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	end = Vector3(obj.position.x, obj.position.y, obj.position.z);
	obj.position=Vector3(x,y,z);
	start = obj.position;
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			transform.position.x=easeIn(start.x,end.x,i);
			transform.position.y=easeIn(start.y,end.y,i);
			transform.position.z=easeIn(start.z,end.z,i);
		}
		if(easing=="easeOut" || easing=="easeout"){
			transform.position.x=easeOut(start.x,end.x,i);
			transform.position.y=easeOut(start.y,end.y,i);
			transform.position.z=easeOut(start.z,end.z,i);
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			transform.position.x=easeInOut(start.x,end.x,i);
			transform.position.y=easeInOut(start.y,end.y,i);
			transform.position.z=easeInOut(start.z,end.z,i);
		}
		if(easing=="linear"){
			transform.position.x=linear(start.x,end.x,i);
			transform.position.y=linear(start.y,end.y,i);
			transform.position.z=linear(start.z,end.z,i);
		}
		yield;
	}
	obj.position=end;	
}

//Fade applied code:
function fadeTo(endA: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	if(guiTexture){
		start=guiTexture.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				guiTexture.color.a=easeIn(start,endA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				guiTexture.color.a=easeOut(start,endA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				guiTexture.color.a=easeInOut(start,endA,i);
			}
			if(easing=="linear"){
				guiTexture.color.a=linear(start,endA,i);
			}			
			yield;
		}
		guiTexture.color.a= endA;
	}else{
		start=renderer.material.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				renderer.material.color.a=easeIn(start,endA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				renderer.material.color.a=easeOut(start,endA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				renderer.material.color.a=easeInOut(start,endA,i);
			}
			if(easing=="linear"){
				renderer.material.color.a=linear(start,endA,i);
			}					
			yield;
		}
		renderer.material.color.a= endA;
	}
}

function fadeFrom(endA: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	if(guiTexture){
		finalA = guiTexture.color.a;
		guiTexture.color.a=endA;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				guiTexture.color.a=easeIn(endA,finalA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				guiTexture.color.a=easeOut(endA,finalA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				guiTexture.color.a=easeInOut(endA,finalA,i);
			}
			if(easing=="linear"){
				guiTexture.color.a=linear(endA,finalA,i);
			}				
			yield;
		}
		guiTexture.color.a= finalA;
	}else{
		finalA = renderer.material.color.a;
		renderer.material.color.a=endA;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				renderer.material.color.a=easeIn(endA,finalA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				renderer.material.color.a=easeOut(endA,finalA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				renderer.material.color.a=easeInOut(endA,finalA,i);
			}
			if(easing=="linear"){
				renderer.material.color.a=linear(endA,finalA,i);
			}					
			yield;
		}
		renderer.material.color.a= finalA;
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

//Curves
function easeOut(start : float, end : float, value : float) : float{
    return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5));
}

function easeIn(start : float, end : float, value : float) : float{
    return Mathf.Lerp(start, end, 1.0 - Mathf.Cos(value * Mathf.PI * 0.5));
}

function easeInOut(start : float, end : float, value : float) : float{
	return Mathf.SmoothStep(start, end, value);
}

function linear(start : float, end : float, value : float) : float{
	return Mathf.Lerp(start, end, value);
}
