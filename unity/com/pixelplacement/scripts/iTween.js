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

//fadeFrom registration function:
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
static function rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(x,y,z,duration,delay,easing,"rotateTo"));
}

//rotateFrom registration function:
static function rotateFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){
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
	params.push(new Array(x,y,z,duration,delay,easing,"rotateFrom"));
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
	if(paramList[paramList.length-1]=="rotateTo"){
		yield rotateTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);
	}
	if(paramList[paramList.length-1]=="rotateFrom"){
		yield rotateFrom(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);
	}
	
	Destroy(this);
}

//Move applied code:
private function moveTo(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	start = obj.position;
	end = Vector3(x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			obj.position.x=easeIn(start.x,end.x,i);
			obj.position.y=easeIn(start.y,end.y,i);
			obj.position.z=easeIn(start.z,end.z,i);
		}
		if(easing=="easeOut" || easing=="easeout"){
			obj.position.x=easeOut(start.x,end.x,i);
			obj.position.y=easeOut(start.y,end.y,i);
			obj.position.z=easeOut(start.z,end.z,i);
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			obj.position.x=easeInOut(start.x,end.x,i);
			obj.position.y=easeInOut(start.y,end.y,i);
			obj.position.z=easeInOut(start.z,end.z,i);
		}
		if(easing=="linear"){
			obj.position.x=linear(start.x,end.x,i);
			obj.position.y=linear(start.y,end.y,i);
			obj.position.z=linear(start.z,end.z,i);
		}
		yield;
	}
	obj.position=end;	
}

private function moveFrom(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	end = Vector3(obj.position.x, obj.position.y, obj.position.z);
	obj.position=Vector3(x,y,z);
	start = obj.position;
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			obj.position.x=easeIn(start.x,end.x,i);
			obj.position.y=easeIn(start.y,end.y,i);
			obj.position.z=easeIn(start.z,end.z,i);
		}
		if(easing=="easeOut" || easing=="easeout"){
			obj.position.x=easeOut(start.x,end.x,i);
			obj.position.y=easeOut(start.y,end.y,i);
			obj.position.z=easeOut(start.z,end.z,i);
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			obj.position.x=easeInOut(start.x,end.x,i);
			obj.position.y=easeInOut(start.y,end.y,i);
			obj.position.z=easeInOut(start.z,end.z,i);
		}
		if(easing=="linear"){
			obj.position.x=linear(start.x,end.x,i);
			obj.position.y=linear(start.y,end.y,i);
			obj.position.z=linear(start.z,end.z,i);
		}
		yield;
	}
	obj.position=end;	
}

//Fade applied code:
private function fadeTo(endA: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	if(obj.guiTexture){
		start=obj.guiTexture.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				obj.guiTexture.color.a=easeIn(start,endA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				obj.guiTexture.color.a=easeOut(start,endA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				obj.guiTexture.color.a=easeInOut(start,endA,i);
			}
			if(easing=="linear"){
				obj.guiTexture.color.a=linear(start,endA,i);
			}			
			yield;
		}
		guiTexture.color.a= endA;
	}else{
		start=obj.renderer.material.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				obj.renderer.material.color.a=easeIn(start,endA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				obj.renderer.material.color.a=easeOut(start,endA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				obj.renderer.material.color.a=easeInOut(start,endA,i);
			}
			if(easing=="linear"){
				obj.renderer.material.color.a=linear(start,endA,i);
			}					
			yield;
		}
		obj.renderer.material.color.a= endA;
	}
}

private function fadeFrom(endA: float, duration: float, delay: float, easing: String) {
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	if(obj.guiTexture){
		finalA = obj.guiTexture.color.a;
		obj.guiTexture.color.a=endA;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				obj.guiTexture.color.a=easeIn(endA,finalA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				obj.guiTexture.color.a=easeOut(endA,finalA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				obj.guiTexture.color.a=easeInOut(endA,finalA,i);
			}
			if(easing=="linear"){
				obj.guiTexture.color.a=linear(endA,finalA,i);
			}				
			yield;
		}
		obj.guiTexture.color.a= finalA;
	}else{
		finalA = obj.renderer.material.color.a;
		obj.renderer.material.color.a=endA;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeIn" || easing=="easein"){
				obj.renderer.material.color.a=easeIn(endA,finalA,i);
			}
			if(easing=="easeOut" || easing=="easeout"){
				obj.renderer.material.color.a=easeOut(endA,finalA,i);
			}
			if(easing=="easeInOut" || easing=="easeinout"){
				obj.renderer.material.color.a=easeInOut(endA,finalA,i);
			}
			if(easing=="linear"){
				obj.renderer.material.color.a=linear(endA,finalA,i);
			}					
			yield;
		}
		obj.renderer.material.color.a= finalA;
	}
}

//Rotate applied code:
private function rotateTo(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	if(guiTexture){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	start = Vector3(obj.eulerAngles.x,obj.eulerAngles.y,obj.eulerAngles.z);
	end = Vector3(Clerp(start.x,x,1), Clerp(start.y,y,1), Clerp(start.z,z,1));
	
	print(Clerp(start.x,end.x,1));
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			obj.rotation=Quaternion.Euler(easeIn(start.x,end.x,i),easeIn(start.y,end.y,i),easeIn(start.z,end.z,i));
		}
		if(easing=="easeOut" || easing=="easeout"){
			obj.rotation=Quaternion.Euler(easeOut(start.x,end.x,i),easeOut(start.y,end.y,i),easeOut(start.z,end.z,i));
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			obj.rotation=Quaternion.Euler(easeInOut(start.x,end.x,i),easeInOut(start.y,end.y,i),easeInOut(start.z,end.z,i));
		}
		if(easing=="linear"){
			obj.rotation=Quaternion.Euler(Clerp(start.x,end.x,i),Clerp(start.y,end.y,i),Clerp(start.z,end.z,i));
		}
		yield;
	}
	obj.rotation=Quaternion.Euler(end.x,end.y,end.z);
}

private function rotateFrom(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	if(guiTexture){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	yield WaitForSeconds (delay);
	obj = gameObject.transform;
	end = Vector3(obj.rotation.x, obj.rotation.y, obj.rotation.z);
	obj.rotation = Quaternion.Euler(x,y,z);
	start = Vector3(obj.eulerAngles.x,obj.eulerAngles.y,obj.eulerAngles.z);
	end = Vector3(Clerp(start.x,end.x,1), Clerp(start.y,end.y,1), Clerp(start.z,end.z,1));
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeIn" || easing=="easein"){
			obj.rotation=Quaternion.Euler(easeIn(start.x,end.x,i),easeIn(start.y,end.y,i),easeIn(start.z,end.z,i));
		}
		if(easing=="easeOut" || easing=="easeout"){
			obj.rotation=Quaternion.Euler(easeOut(start.x,end.x,i),easeOut(start.y,end.y,i),easeOut(start.z,end.z,i));
		}
		if(easing=="easeInOut" || easing=="easeinout"){
			obj.rotation=Quaternion.Euler(easeInOut(start.x,end.x,i),easeInOut(start.y,end.y,i),easeInOut(start.z,end.z,i));
		}
		if(easing=="linear"){
			obj.rotation=Quaternion.Euler(Clerp(start.x,end.x,i),Clerp(start.y,end.y,i),Clerp(start.z,end.z,i));
		}
		yield;
	}
	obj.rotation.x=end.x;
	obj.rotation.y=end.y;
	obj.rotation.z=end.z;	
}

//Registers ID lookup:
private function findRegister(){
	for(i = 0; i < registers.length; i++){
		if(registers[i]==gameObject){
			id =  i;
		}
	}
}

//Curves
private function easeOut(start : float, end : float, value : float) : float{
    return Clerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5));
}

private function easeIn(start : float, end : float, value : float) : float{
    return Clerp(start, end, 1.0 - Mathf.Cos(value * Mathf.PI * 0.5));
}

private function easeInOut(start : float, end : float, value : float) : float{
	return Mathf.SmoothStep(start, end, value);
}

private function linear(start : float, end : float, value : float) : float{
	return Mathf.Lerp(start, end, value);
}

private function Clerp(start : float, end : float, value : float) : float {
   var min = 0.0;
   var max = 360.0;
   var half = Mathf.Abs((max - min)/2.0);
   var retval = 0.0;
   var diff = 0.0;
   
   if((end - start) < -half){
       diff = ((max - start)+end)*value;
       retval =  start+diff;
   }
   else if((end - start) > half){
       diff = -((max - end)+start)*value;
       retval =  start+diff;
   }
   else retval =  start+(end-start)*value;
   return retval;
}
