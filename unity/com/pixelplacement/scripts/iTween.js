//iTween: Easy movement, rotation and fading of GUITextures and meshes.//If using fadeTo on a mesh, make sure it has a shader that supports transparency.//rotateTo will not a work on a GUITexture since it doesn't have rotation.//iTween calls can be staked with delays to chain animations.//29 possible ease curves are: linear,spring,easeInQuad,easeOutQuad,easeInOutQuad,easeInCubic,easeOutCubic,easeInOutCubic,easeInQuart,easeOutQuart,easeInOutQuart,easeInQuint,easeOutQuint,easeInOutQuint,easeInSine,easeOutSine,easeInOutSine,easeInExpo,easeOutExpo,easeInOutExpo,easeInCirc,easeOutCirc,bounce,easeInBack,easeOutBack,easeInOutBack//Author: Pixelplacement//Usage: Static//Methods: fadeTo,fadeFrom,moveTo,moveFrom,rotateTo

//To do animations: volumeTo(),volumeFrom(),fadeAway(duration,delay), scaleTo(),scaleFrom(),colorTo(),colorFrom()
//To do methods: stop()
//Clean up: Handle destruction of lingering iTweens after object has been disabled - i.e. avoid issues with iSwap
#pragma strict//Init vars:static var registers : Array = new Array();static var params : Array = new Array();var id : int = 0;//Registration functions:static function moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(x,y,z,duration,delay,easing,"moveTo"));}static function moveFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(x,y,z,duration,delay,easing,"moveFrom"));}static function fadeTo(obj: GameObject,endA: float,duration: float,delay: float){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(endA,duration,delay,"linear","fadeTo"));}static function fadeFrom(obj: GameObject,endA: float,duration: float,delay: float){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(endA,duration,delay,"fadeFrom"));}static function rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(x,y,z,duration,delay,easing,"rotateTo"));}static function rotateFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(x,y,z,duration,delay,easing,"rotateFrom"));}
static function shake(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(x,y,z,duration,delay,"shake"));}
static function stab(obj: GameObject,sound: AudioSource,volume: float,pitch: float,delay: float){	obj.AddComponent ("iTween");	registers.push(obj);	params.push(new Array(sound,volume,pitch,delay,"stab"));}
//Applied component launcher and loops:function Start(){	findRegister();	var paramList : Array = params[id];	registers.RemoveAt(id);	params.RemoveAt(id);	if(paramList[paramList.length-1]=="moveTo"){		yield moveTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);	}	if(paramList[paramList.length-1]=="moveFrom"){		yield moveFrom(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);	}	if(paramList[paramList.length-1]=="fadeTo"){		yield fadeTo(paramList[0],paramList[1],paramList[2]);	}	if(paramList[paramList.length-1]=="fadeFrom"){		yield fadeFrom(paramList[0],paramList[1],paramList[2]);	}		if(paramList[paramList.length-1]=="rotateTo"){		yield rotateTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);	}	if(paramList[paramList.length-1]=="rotateFrom"){		yield rotateFrom(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);	}	if(paramList[paramList.length-1]=="shake"){		yield shake(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);	}
	if(paramList[paramList.length-1]=="stab"){		yield stab(paramList[0],paramList[1],paramList[2],paramList[3]);	}
		Destroy(this);}

//Stab applied code:
function stab(sound: AudioSource,volume: float,pitch: float,delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	sound.volume=volume;
	sound.pitch=pitch;
	sound.PlayOneShot(sound.clip);
	yield;}

//Shake applied code:
//Negative values will designate initial impact direction
function shake(x : float, y: float, z: float, duration: float, delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	var shakeMagnitude : Vector3 = Vector3(x,y,z);	obj = gameObject.transform;
	start=obj.transform.position;
	var impact : boolean=true;	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(!impact){
			obj.position.x =start.x + Random.Range(-shakeMagnitude.x, shakeMagnitude.x);
			obj.position.y =start.y + Random.Range(-shakeMagnitude.y, shakeMagnitude.y);
			obj.position.z =start.z + Random.Range(-shakeMagnitude.z, shakeMagnitude.z);
		}
		if(impact){
			impact = false;
			obj.position.x += shakeMagnitude.x;
			obj.position.y += shakeMagnitude.y;
			obj.position.z += shakeMagnitude.z;
		}
		shakeMagnitude.x=x-(i*x);
		shakeMagnitude.y=y-(i*y);
		shakeMagnitude.z=z-(i*z);
				yield;	}
	obj.position=start;}

//Fade applied code:private function fadeTo(endA: float, duration: float, delay: float) {	if(delay > 0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	if(obj.guiTexture){		start=obj.guiTexture.color.a;		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {			obj.guiTexture.color.a=linear(start,endA,i);					yield;		}		guiTexture.color.a= endA;	}else{		start=obj.renderer.material.color.a;		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {			obj.renderer.material.color.a=linear(start,endA,i);						yield;		}		obj.renderer.material.color.a= endA;	}}private function fadeFrom(endA: float, duration: float, delay: float) {	if(delay > 0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	if(obj.guiTexture){		finalA = obj.guiTexture.color.a;		obj.guiTexture.color.a=endA;		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {			obj.guiTexture.color.a=linear(endA,finalA,i);							yield;		}		obj.guiTexture.color.a= finalA;	}else{		finalA = obj.renderer.material.color.a;		obj.renderer.material.color.a=endA;		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {			obj.renderer.material.color.a=linear(endA,finalA,i);						yield;		}		obj.renderer.material.color.a= finalA;	}}//Move applied code:private function moveTo(x : float, y: float, z: float, duration: float, delay: float, easing: String) {	if(delay > 0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	start = obj.position;	end = Vector3(x, y, z);	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {		if(easing=="easeInQuad"){			obj.position.x=easeInQuad(start.x,end.x,i);			obj.position.y=easeInQuad(start.y,end.y,i);			obj.position.z=easeInQuad(start.z,end.z,i);		}		if(easing=="easeOutQuad"){			obj.position.x=easeOutQuad(start.x,end.x,i);			obj.position.y=easeOutQuad(start.y,end.y,i);			obj.position.z=easeOutQuad(start.z,end.z,i);		}		if(easing=="easeInOutQuad"){			obj.position.x=easeInOutQuad(start.x,end.x,i);			obj.position.y=easeInOutQuad(start.y,end.y,i);			obj.position.z=easeInOutQuad(start.z,end.z,i);		}		if(easing=="easeInCubic"){			obj.position.x=easeInCubic(start.x,end.x,i);			obj.position.y=easeInCubic(start.y,end.y,i);			obj.position.z=easeInCubic(start.z,end.z,i);		}		if(easing=="easeOutCubic"){			obj.position.x=easeOutCubic(start.x,end.x,i);			obj.position.y=easeOutCubic(start.y,end.y,i);			obj.position.z=easeOutCubic(start.z,end.z,i);		}		if(easing=="easeInOutCubic"){			obj.position.x=easeInOutCubic(start.x,end.x,i);			obj.position.y=easeInOutCubic(start.y,end.y,i);			obj.position.z=easeInOutCubic(start.z,end.z,i);		}
		if(easing=="easeInQuart"){			obj.position.x=easeInQuart(start.x,end.x,i);			obj.position.y=easeInQuart(start.y,end.y,i);			obj.position.z=easeInQuart(start.z,end.z,i);		}		if(easing=="easeOutQuart"){			obj.position.x=easeOutQuart(start.x,end.x,i);			obj.position.y=easeOutQuart(start.y,end.y,i);			obj.position.z=easeOutQuart(start.z,end.z,i);		}		if(easing=="easeInOutQuart"){			obj.position.x=easeInOutQuart(start.x,end.x,i);			obj.position.y=easeInOutQuart(start.y,end.y,i);			obj.position.z=easeInOutQuart(start.z,end.z,i);		}	
		if(easing=="easeInQuint"){			obj.position.x=easeInQuint(start.x,end.x,i);			obj.position.y=easeInQuint(start.y,end.y,i);			obj.position.z=easeInQuint(start.z,end.z,i);		}		if(easing=="easeOutQuint"){			obj.position.x=easeOutQuint(start.x,end.x,i);			obj.position.y=easeOutQuint(start.y,end.y,i);			obj.position.z=easeOutQuint(start.z,end.z,i);		}		if(easing=="easeInOutQuint"){			obj.position.x=easeInOutQuint(start.x,end.x,i);			obj.position.y=easeInOutQuint(start.y,end.y,i);			obj.position.z=easeInOutQuint(start.z,end.z,i);		}	
		if(easing=="easeInSine"){			obj.position.x=easeInSine(start.x,end.x,i);			obj.position.y=easeInSine(start.y,end.y,i);			obj.position.z=easeInSine(start.z,end.z,i);		}		if(easing=="easeOutSine"){			obj.position.x=easeOutSine(start.x,end.x,i);			obj.position.y=easeOutSine(start.y,end.y,i);			obj.position.z=easeOutSine(start.z,end.z,i);		}		if(easing=="easeInOutSine"){			obj.position.x=easeInOutSine(start.x,end.x,i);			obj.position.y=easeInOutSine(start.y,end.y,i);			obj.position.z=easeInOutSine(start.z,end.z,i);		}
		if(easing=="easeInExpo"){			obj.position.x=easeInExpo(start.x,end.x,i);			obj.position.y=easeInExpo(start.y,end.y,i);			obj.position.z=easeInExpo(start.z,end.z,i);		}		if(easing=="easeOutExpo"){			obj.position.x=easeOutExpo(start.x,end.x,i);			obj.position.y=easeOutExpo(start.y,end.y,i);			obj.position.z=easeOutExpo(start.z,end.z,i);		}		if(easing=="easeInOutExpo"){			obj.position.x=easeInOutExpo(start.x,end.x,i);			obj.position.y=easeInOutExpo(start.y,end.y,i);			obj.position.z=easeInOutExpo(start.z,end.z,i);		}
		if(easing=="easeInCirc"){			obj.position.x=easeInCirc(start.x,end.x,i);			obj.position.y=easeInCirc(start.y,end.y,i);			obj.position.z=easeInCirc(start.z,end.z,i);		}		if(easing=="easeOutCirc"){			obj.position.x=easeOutCirc(start.x,end.x,i);			obj.position.y=easeOutCirc(start.y,end.y,i);			obj.position.z=easeOutCirc(start.z,end.z,i);		}		if(easing=="easeInOutCirc"){			obj.position.x=easeInOutCirc(start.x,end.x,i);			obj.position.y=easeInOutCirc(start.y,end.y,i);			obj.position.z=easeInOutCirc(start.z,end.z,i);		}		if(easing=="linear"){			obj.position.x=linear(start.x,end.x,i);			obj.position.y=linear(start.y,end.y,i);			obj.position.z=linear(start.z,end.z,i);		}		if(easing=="spring"){			obj.position.x=spring(start.x,end.x,i);			obj.position.y=spring(start.y,end.y,i);			obj.position.z=spring(start.z,end.z,i);		}
		if(easing=="bounce"){			obj.position.x=bounce(start.x,end.x,i);			obj.position.y=bounce(start.y,end.y,i);			obj.position.z=bounce(start.z,end.z,i);		}
		if(easing=="easeInBack"){			obj.position.x=easeInBack(start.x,end.x,i);			obj.position.y=easeInBack(start.y,end.y,i);			obj.position.z=easeInBack(start.z,end.z,i);		}
		if(easing=="easeOutBack"){			obj.position.x=easeOutBack(start.x,end.x,i);			obj.position.y=easeOutBack(start.y,end.y,i);			obj.position.z=easeOutBack(start.z,end.z,i);		}
		if(easing=="easeInOutBack"){			obj.position.x=easeInOutBack(start.x,end.x,i);			obj.position.y=easeInOutBack(start.y,end.y,i);			obj.position.z=easeInOutBack(start.z,end.z,i);		}		yield;	}	obj.position=end;	}private function moveFrom(x : float, y: float, z: float, duration: float, delay: float, easing: String) {
	if(delay>0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	end = Vector3(obj.position.x, obj.position.y, obj.position.z);	obj.position=Vector3(x,y,z);	start = obj.position;	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {		if(easing=="easeInQuad"){			obj.position.x=easeInQuad(start.x,end.x,i);			obj.position.y=easeInQuad(start.y,end.y,i);			obj.position.z=easeInQuad(start.z,end.z,i);		}		if(easing=="easeOutQuad"){			obj.position.x=easeOutQuad(start.x,end.x,i);			obj.position.y=easeOutQuad(start.y,end.y,i);			obj.position.z=easeOutQuad(start.z,end.z,i);		}		if(easing=="easeInOutQuad"){			obj.position.x=easeInOutQuad(start.x,end.x,i);			obj.position.y=easeInOutQuad(start.y,end.y,i);			obj.position.z=easeInOutQuad(start.z,end.z,i);		}		if(easing=="easeInCubic"){			obj.position.x=easeInCubic(start.x,end.x,i);			obj.position.y=easeInCubic(start.y,end.y,i);			obj.position.z=easeInCubic(start.z,end.z,i);		}		if(easing=="easeOutCubic"){			obj.position.x=easeOutCubic(start.x,end.x,i);			obj.position.y=easeOutCubic(start.y,end.y,i);			obj.position.z=easeOutCubic(start.z,end.z,i);		}		if(easing=="easeInOutCubic"){			obj.position.x=easeInOutCubic(start.x,end.x,i);			obj.position.y=easeInOutCubic(start.y,end.y,i);			obj.position.z=easeInOutCubic(start.z,end.z,i);		}	
		if(easing=="easeInQuart"){			obj.position.x=easeInQuart(start.x,end.x,i);			obj.position.y=easeInQuart(start.y,end.y,i);			obj.position.z=easeInQuart(start.z,end.z,i);		}		if(easing=="easeOutQuart"){			obj.position.x=easeOutQuart(start.x,end.x,i);			obj.position.y=easeOutQuart(start.y,end.y,i);			obj.position.z=easeOutCubic(start.z,end.z,i);		}		if(easing=="easeInOutQuart"){			obj.position.x=easeInOutQuart(start.x,end.x,i);			obj.position.y=easeInOutQuart(start.y,end.y,i);			obj.position.z=easeInOutQuart(start.z,end.z,i);		}
		if(easing=="easeInQuint"){			obj.position.x=easeInQuint(start.x,end.x,i);			obj.position.y=easeInQuint(start.y,end.y,i);			obj.position.z=easeInQuint(start.z,end.z,i);		}		if(easing=="easeOutQuint"){			obj.position.x=easeOutQuint(start.x,end.x,i);			obj.position.y=easeOutQuint(start.y,end.y,i);			obj.position.z=easeOutQuint(start.z,end.z,i);		}		if(easing=="easeInOutQuint"){			obj.position.x=easeInOutQuint(start.x,end.x,i);			obj.position.y=easeInOutQuint(start.y,end.y,i);			obj.position.z=easeInOutQuint(start.z,end.z,i);		}	
		if(easing=="easeInSine"){			obj.position.x=easeInSine(start.x,end.x,i);			obj.position.y=easeInSine(start.y,end.y,i);			obj.position.z=easeInSine(start.z,end.z,i);		}		if(easing=="easeOutSine"){			obj.position.x=easeOutSine(start.x,end.x,i);			obj.position.y=easeOutSine(start.y,end.y,i);			obj.position.z=easeOutSine(start.z,end.z,i);		}		if(easing=="easeInOutSine"){			obj.position.x=easeInOutSine(start.x,end.x,i);			obj.position.y=easeInOutSine(start.y,end.y,i);			obj.position.z=easeInOutSine(start.z,end.z,i);		}
		if(easing=="easeInExpo"){			obj.position.x=easeInExpo(start.x,end.x,i);			obj.position.y=easeInExpo(start.y,end.y,i);			obj.position.z=easeInExpo(start.z,end.z,i);		}		if(easing=="easeOutExpo"){			obj.position.x=easeOutExpo(start.x,end.x,i);			obj.position.y=easeOutExpo(start.y,end.y,i);			obj.position.z=easeOutExpo(start.z,end.z,i);		}		if(easing=="easeInOutExpo"){			obj.position.x=easeInOutExpo(start.x,end.x,i);			obj.position.y=easeInOutExpo(start.y,end.y,i);			obj.position.z=easeInOutExpo(start.z,end.z,i);		}
		if(easing=="easeInCirc"){			obj.position.x=easeInCirc(start.x,end.x,i);			obj.position.y=easeInCirc(start.y,end.y,i);			obj.position.z=easeInCirc(start.z,end.z,i);		}		if(easing=="easeOutCirc"){			obj.position.x=easeOutCirc(start.x,end.x,i);			obj.position.y=easeOutCirc(start.y,end.y,i);			obj.position.z=easeOutCirc(start.z,end.z,i);		}		if(easing=="easeInOutCirc"){			obj.position.x=easeInOutCirc(start.x,end.x,i);			obj.position.y=easeInOutCirc(start.y,end.y,i);			obj.position.z=easeInOutCirc(start.z,end.z,i);		}				if(easing=="linear"){			obj.position.x=linear(start.x,end.x,i);			obj.position.y=linear(start.y,end.y,i);			obj.position.z=linear(start.z,end.z,i);		}		if(easing=="spring"){			obj.position.x=spring(start.x,end.x,i);			obj.position.y=spring(start.y,end.y,i);			obj.position.z=spring(start.z,end.z,i);		}
		if(easing=="bounce"){			obj.position.x=bounce(start.x,end.x,i);			obj.position.y=bounce(start.y,end.y,i);			obj.position.z=bounce(start.z,end.z,i);		}
		if(easing=="easeInBack"){			obj.position.x=easeInBack(start.x,end.x,i);			obj.position.y=easeInBack(start.y,end.y,i);			obj.position.z=easeInBack(start.z,end.z,i);		}		yield;	}	obj.position=end;}//Rotate applied code:private function rotateTo(x : float, y: float, z: float, duration: float, delay: float, easing: String) {	if(guiTexture){		Debug.LogError("ERROR: GUITextures cannot be rotated!");        return;			}	if(delay > 0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	start = Vector3(obj.eulerAngles.x,obj.eulerAngles.y,obj.eulerAngles.z);	end = Vector3(clerp(start.x,x,1), clerp(start.y,y,1), clerp(start.z,z,1));		print(clerp(start.x,end.x,1));	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {		if(easing=="easeInQuad"){			obj.rotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));		}		if(easing=="easeOutQuad"){			obj.rotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));		}		if(easing=="easeInOutQuad"){			obj.rotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));		}		if(easing=="easeInCubic"){			obj.rotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));		}		if(easing=="easeOutCubic"){			obj.rotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));		}		if(easing=="easeInOutCubic"){			obj.rotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));		}
		if(easing=="easeInQuart"){			obj.rotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));		}		if(easing=="easeOutQuart"){			obj.rotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));		}		if(easing=="easeInOutQuart"){			obj.rotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));		}	
		if(easing=="easeInQuint"){			obj.rotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));		}		if(easing=="easeOutQuint"){			obj.rotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));		}		if(easing=="easeInOutQuint"){			obj.rotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));		}	
		if(easing=="easeInSine"){			obj.rotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));		}		if(easing=="easeOutSine"){			obj.rotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));		}		if(easing=="easeInOutSine"){			obj.rotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));		}	
		if(easing=="easeInExpo"){			obj.rotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));		}		if(easing=="easeOutExpo"){			obj.rotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));		}		if(easing=="easeInOutExpo"){			obj.rotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));		}	
		if(easing=="easeInCirc"){			obj.rotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));		}		if(easing=="easeOutCirc"){			obj.rotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));		}		if(easing=="easeInOutCirc"){			obj.rotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));		}		if(easing=="linear"){			obj.rotation=Quaternion.Euler(clerp(start.x,end.x,i),clerp(start.y,end.y,i),clerp(start.z,end.z,i));		}		if(easing=="spring"){			obj.rotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));		}
		if(easing=="bounce"){			obj.rotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));		}
		if(easing=="easeInBack"){			obj.rotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));		}
		if(easing=="easeOutBack"){			obj.rotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));		}
		if(easing=="easeInOutBack"){			obj.rotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));		}		yield;	}	obj.rotation=Quaternion.Euler(end.x,end.y,end.z);}private function rotateFrom(x : float, y: float, z: float, duration: float, delay: float, easing: String) {	if(guiTexture){		Debug.LogError("ERROR: GUITextures cannot be rotated!");        return;			}	if(delay > 0){
		yield WaitForSeconds (delay);
	}	obj = gameObject.transform;	end = Vector3(obj.rotation.x, obj.rotation.y, obj.rotation.z);	obj.rotation = Quaternion.Euler(x,y,z);	start = Vector3(obj.eulerAngles.x,obj.eulerAngles.y,obj.eulerAngles.z);	end = Vector3(clerp(start.x,end.x,1), clerp(start.y,end.y,1), clerp(start.z,end.z,1));	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {		if(easing=="easeInQuad"){			obj.rotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));		}		if(easing=="easeOutQuad"){			obj.rotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));		}		if(easing=="easeInOutQuad"){			obj.rotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));		}		if(easing=="easeInCubic"){			obj.rotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));		}		if(easing=="easeOutCubic"){			obj.rotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));		}		if(easing=="easeInOutCubic"){			obj.rotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));		}
		if(easing=="easeInQuart"){			obj.rotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));		}		if(easing=="easeOutQuart"){			obj.rotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));		}		if(easing=="easeInOutQuart"){			obj.rotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));		}
		if(easing=="easeInQuint"){			obj.rotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));		}		if(easing=="easeOutQuint"){			obj.rotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));		}		if(easing=="easeInOutQuint"){			obj.rotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));		}
		if(easing=="easeInSine"){			obj.rotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));		}		if(easing=="easeOutSine"){			obj.rotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));		}		if(easing=="easeInOutSine"){			obj.rotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));		}
		if(easing=="easeInExpo"){			obj.rotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));		}		if(easing=="easeOutExpo"){			obj.rotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));		}		if(easing=="easeInOutExpo"){			obj.rotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));		}
		if(easing=="easeInCirc"){			obj.rotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));		}		if(easing=="easeOutCirc"){			obj.rotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));		}		if(easing=="easeInOutCirc"){			obj.rotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));		}				if(easing=="linear"){			obj.rotation=Quaternion.Euler(clerp(start.x,end.x,i),clerp(start.y,end.y,i),clerp(start.z,end.z,i));		}		if(easing=="spring"){			obj.rotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));		}
		if(easing=="bounce"){			obj.rotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));		}
		if(easing=="easeInBack"){			obj.rotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));		}
		if(easing=="easeOutBack"){			obj.rotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));		}
		if(easing=="easeInOutBack"){			obj.rotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));		}		yield;	}	obj.rotation.x=end.x;	obj.rotation.y=end.y;	obj.rotation.z=end.z;	}//Registers ID lookup:private function findRegister(){	for(i = 0; i < registers.length; i++){		if(registers[i]==gameObject){			id =  i;		}	}}//Curvesprivate function linear(start : float, end : float, value : float) : float{	return Mathf.Lerp(start, end, value);}private function clerp(start : float, end : float, value : float) : float {   var min = 0.0;   var max = 360.0;   var half = Mathf.Abs((max - min)/2.0);   var retval = 0.0;   var diff = 0.0;      if((end - start) < -half){       diff = ((max - start)+end)*value;       retval =  start+diff;   }   else if((end - start) > half){       diff = -((max - end)+start)*value;       retval =  start+diff;   }   else retval =  start+(end-start)*value;   return retval;}function spring(start : float, end : float, value : float) : float{    value = Mathf.Clamp01(value);    value = (Mathf.Sin(value * Mathf.PI * (0.2 + 2.5 * value * value * value)) * Mathf.Pow(1 - value, 2.2) + value) * (1 + (1.2 * (1 - value)));    return start + (end - start) * value;}function easeInQuad(start : float, end : float, value : float) : float {	value /= 1;	end -= start;	return end*value*value + start;}function easeOutQuad(start : float, end : float, value : float) : float{	value /= 1;	end -= start;	return -end * value*(value-2) + start;}function easeInOutQuad(start : float, end : float, value : float) : float {	value /= .5;	end -= start;	if (value < 1) return end/2*value*value + start;	value--;	return -end/2 * (value*(value-2) - 1) + start;};function easeInCubic (start : float, end : float, value : float) : float {	value /= 1;	end -= start;	return end*value*value*value + start;};function easeOutCubic (start : float, end : float, value : float) : float {	value /= 1;	value--;	end -= start;	return end*(value*value*value + 1) + start;};function easeInOutCubic (start : float, end : float, value : float) : float {	value /= .5;	end -= start;	if (value < 1) return end/2*value*value*value + start;	value -= 2;	return end/2*(value*value*value + 2) + start;};function easeInQuart (start : float, end : float, value : float) : float {	value /= 1;	end -= start;	return end*value*value*value*value + start;};function easeOutQuart(start : float, end : float, value : float) : float {	value /= 1;	value--;	end -= start;	return -end * (value*value*value*value - 1) + start;};function easeInOutQuart(start : float, end : float, value : float) : float {	value /= .5;	end -= start;	if (value < 1) return end/2*value*value*value*value + start;	value -= 2;	return -end/2 * (value*value*value*value - 2) + start;};

function easeInQuint(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value*value*value*value + start;
};
function easeOutQuint(start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return end*(value*value*value*value*value + 1) + start;
};

function easeInOutQuint(start : float, end : float, value : float) : float {
	value /= .5;	end -= start;
	if (value < 1) return end/2*value*value*value*value*value + start;
	value -= 2;
	return end/2*(value*value*value*value*value + 2) + start;
};

function easeInSine(start : float, end : float, value : float) : float {
	end -= start;
	return -end * Mathf.Cos(value/1 * (Mathf.PI/2)) + end + start;
};

function easeOutSine(start : float, end : float, value : float) : float {
	end -= start;
	return end * Mathf.Sin(value/1 * (Mathf.PI/2)) + start;
};

function easeInOutSine(start : float, end : float, value : float) : float {
	end -= start;
	return -end/2 * (Mathf.Cos(Mathf.PI*value/1) - 1) + start;
};

function easeInExpo(start : float, end : float, value : float) : float {
	end -= start;
	return end * Mathf.Pow(2, 10 * (value/1 - 1) ) + start;
};

function easeOutExpo(start : float, end : float, value : float) : float {
	end -= start;
	return end * ( -Mathf.Pow( 2, -10 * value/1 ) + 1 ) + start;
};

function easeInOutExpo(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2 * Mathf.Pow( 2, 10 * (value - 1) ) + start;
	value--;
	return end/2 * ( -Mathf.Pow( 2, -10 * value) + 2 ) + start;
};

function easeInCirc(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return -end * (Mathf.Sqrt(1 - value*value) - 1) + start;
};

function easeOutCirc(start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return end * Mathf.Sqrt(1 - value*value) + start;
};

function easeInOutCirc(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return -end/2 * (Mathf.Sqrt(1 - value*value) - 1) + start;
	value -= 2;
	return end/2 * (Mathf.Sqrt(1 - value*value) + 1) + start;
};

function bounce (start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	if (value < (1/2.75)) {
		return end*(7.5625*value*value) + start;
	} else if (value < (2/2.75)) {
		value-=(1.5/2.75);
		return end*(7.5625*(value)*value + .75) + start;
	} else if (value < (2.5/2.75)) {
		value-=(2.25/2.75);
		return end*(7.5625*(value)*value + .9375) + start;
	} else {
		value-=(2.625/2.75);
		return end*(7.5625*(value)*value + .984375) + start;
	}
}

function easeInBack (start : float, end : float, value : float) : float {
	end -= start;
	value/=1;
	s = 1.70158;
	return end*(value)*value*((s+1)*value - s) + start;
}

function easeOutBack (start : float, end : float, value : float) : float {
	s = 1.70158;
	end -= start;
	value=(value/1)-1;
	return end*((value)*value*((s+1)*value + s) + 1) + start;
}

function easeInOutBack (start : float, end : float, value : float) : float {
	s = 1.70158; 
	end -= start;
	value/=.5;
	if ((value) < 1) {
		s*=(1.525);
		return end/2*(value*value*(((s)+1)*value - s)) + start;
	}
	value-=2;
	s*=(1.525);
	return end/2*((value)*value*(((s)+1)*value + s) + 2) + start;
}