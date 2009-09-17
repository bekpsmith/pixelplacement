#pragma strict

//Rotation functions need work
//Add moveBy?

//Init vars:
static var registers : Array = new Array();
static var params : Array = new Array();
var id : int = 0;

//Environment init (running this in a GO's Awake that is going to have dynamic iTweens added significantly lessens performance impacts):
static function init(obj: GameObject){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array("init"));
}

//Stop tweening
static function stop(obj: GameObject){
	var scripts = obj.GetComponents (iTween);
	for (var script : iTween in scripts) {
		Destroy(script);
	}
}

//Tween count retrieval
static function getTweenCount(obj: GameObject) : int{
	var scripts = obj.GetComponents (iTween);
	var scriptCount : int;
	for (var script : iTween in scripts) {
		scriptCount+=1;
	}
	return scriptCount;
}

//Registration functions, loopable:
static function moveFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop: String){
	startX=obj.transform.position.x;
	startY=obj.transform.position.y;
	startZ=obj.transform.position.z;
	obj.transform.position.x=x;
	obj.transform.position.y=y;
	obj.transform.position.z=z;
	moveTo(obj,startX,startY,startZ,duration,delay,easing,loop);
}

static function moveTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop: String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,easing,loop,"moveTo"));
}

static function fadeFrom(obj: GameObject,endA: float,duration: float,delay: float, easing: String, loop: String){
	if(obj.guiTexture){
		startA = obj.guiTexture.color.a;
		obj.guiTexture.color.a=endA;
	}else{
		startA = obj.renderer.material.color.a;
		obj.renderer.material.color.a=endA;	
	}
	fadeTo(obj,startA,duration,delay,easing,loop);	
}

static function fadeTo(obj: GameObject,endA: float,duration: float,delay: float, easing: String, loop: String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(endA,duration,delay,easing,loop,"fadeTo"));
}

static function rotateTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop: String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,easing,loop,"rotateTo"));
}

static function rotateBy(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop:String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,easing,loop,"rotateBy"));
}

static function scaleFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop:String){
	targetX=obj.transform.localScale.x;
	targetY=obj.transform.localScale.y;
	targetZ=obj.transform.localScale.z;
	obj.transform.localScale.x=x;
	obj.transform.localScale.y=y;
	obj.transform.localScale.z=z;
	iTween.scaleTo(obj,targetX,targetY,targetZ,duration,delay,easing,loop);
}

static function scaleTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String, loop:String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,easing,loop,"scaleTo"));
}

static function colorFrom(obj: GameObject,r: float,g: float,b: float,duration: float,delay: float, easing: String, loop:String){
	if(obj.guiTexture){
		startR = obj.guiTexture.color.r;
		startG = obj.guiTexture.color.g;
		startB = obj.guiTexture.color.b;
		obj.guiTexture.color.r=r;
		obj.guiTexture.color.g=g;
		obj.guiTexture.color.b=b;
	}else{
		startR = obj.renderer.material.color.r;
		startG = obj.renderer.material.color.g;
		startB = obj.renderer.material.color.b;
		obj.renderer.material.color.r=r;	
		obj.renderer.material.color.g=g;	
		obj.renderer.material.color.b=b;	
	}
	colorTo(obj,startR,startG,startB,duration,delay,easing,loop);		
}

static function colorTo(obj: GameObject,r: float,g: float,b: float,duration: float,delay: float, easing: String, loop:String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(r,g,b,duration,delay,easing,loop,"colorTo"));
}

//Registration functions, non-loopable:
static function punchRotation(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,"punchRotation"));
}

static function punchPosition(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,"punchPosition"));
}

static function audioTo(obj: GameObject,sound: AudioSource,volume: float, pitch: float,duration: float,delay: float, easing: String){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(sound,volume,pitch,duration,delay,easing,"audioTo"));
}

static function shake(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(x,y,z,duration,delay,"shake"));
}

static function stab(obj: GameObject,sound: AudioSource,volume: float,pitch: float,delay: float){
	obj.AddComponent ("iTween");
	registers.push(obj);
	params.push(new Array(sound,volume,pitch,delay,"stab"));
}

//Applied component launcher and loops:
function Start(){
	findRegister();
	var paramList : Array = params[id];
	registers.RemoveAt(id);
	params.RemoveAt(id);
	if(paramList[paramList.length-1]=="init"){
		initMe();
	}
	if(paramList[paramList.length-1]=="moveTo"){
		while (true){
			yield moveTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5],paramList[6]);
		}
	}
	if(paramList[paramList.length-1]=="fadeTo"){
		while (true){
			yield fadeTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
		}
	}
	if(paramList[paramList.length-1]=="rotateTo"){
		while (true){
			yield rotateTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5],paramList[6]);
		}
	}
	if(paramList[paramList.length-1]=="rotateBy"){
		while (true){
			yield rotateBy(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5],paramList[6]);
		}
	}
	if(paramList[paramList.length-1]=="scaleTo"){
		while (true){
			yield scaleTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5],paramList[6]);
		}
	}
	if(paramList[paramList.length-1]=="colorTo"){
		while (true){
			yield colorTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5],paramList[6]);
		}
	}
	if(paramList[paramList.length-1]=="punchRotation"){
		yield punchRotation(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}
	if(paramList[paramList.length-1]=="punchPosition"){
		yield punchPosition(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}		
	if(paramList[paramList.length-1]=="shake"){
		yield shake(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4]);
	}
	if(paramList[paramList.length-1]=="stab"){
		yield stab(paramList[0],paramList[1],paramList[2],paramList[3]);
	}	
	if(paramList[paramList.length-1]=="audioTo"){
		yield audioTo(paramList[0],paramList[1],paramList[2],paramList[3],paramList[4],paramList[5]);
	}	
}

//Init:
private function initMe(){
	Destroy (this);
}

//Loops
private function punchRotation(x : float, y: float, z: float, duration: float, delay: float) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	x*=360;
	y*=360;
	z*=360;
	obj = gameObject.transform;
	pos = obj.localEulerAngles;
	var posAug : Vector3;	
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(x>0){
			posAug.x = punch(x,i) + pos.x;
		}else if(x<0){
			posAug.x=-punch(Mathf.Abs(x),i) + pos.x;	
		}
		if(y>0){
			posAug.y=punch(y,i) + pos.y;
		}else if(y<0){
			posAug.y=-punch(Mathf.Abs(y),i) + pos.y;	
		}
		if(z>0){
			posAug.z=punch(z,i) + pos.z;
		}else if(z<0){
			posAug.z=-punch(Mathf.Abs(z),i) + pos.z;	
		}
		obj.localRotation=Quaternion.Euler(posAug.x,posAug.y,posAug.z);
		yield;
	}
	obj.localRotation=Quaternion.Euler(pos.x,pos.y,pos.z);
	Destroy (this);
}

private function punchPosition(x : float, y: float, z: float, duration: float, delay: float) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	pos = obj.position;
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(x>0){
			obj.position.x=punch(x,i) + pos.x;
		}else if(x<0){
			obj.position.x=-punch(Mathf.Abs(x),i) + pos.x;	
		}
		if(y>0){
			obj.position.y=punch(y,i) + pos.y;
		}else if(y<0){
			obj.position.y=-punch(Mathf.Abs(y),i) + pos.y;	
		}
		if(z>0){
			obj.position.z=punch(z,i) + pos.z;
		}else if(z<0){
			obj.position.z=-punch(Mathf.Abs(z),i) + pos.z;	
		}
		yield;
	}
	obj.position=pos;
}

private function audioTo(sound: AudioSource, volume : float, pitch: float, duration: float, delay: float, easing: String) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	startV = sound.volume;
	endV = volume;
	startP = sound.pitch;
	endP = pitch;
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeInQuad"){
			sound.volume =easeInQuad(startV,endV,i);
			sound.pitch =easeInQuad(startP,endP,i);
		}
		if(easing=="easeOutQuad"){
			sound.volume =easeOutQuad(startV,endV,i);
			sound.pitch =easeOutQuad(startP,endP,i);
		}
		if(easing=="easeInOutQuad"){
			sound.volume =easeInOutQuad(startV,endV,i);
			sound.pitch =easeInOutQuad(startP,endP,i);
		}
		if(easing=="easeInCubic"){
			sound.volume =easeInCubic(startV,endV,i);
			sound.pitch =easeInCubic(startP,endP,i);
		}
		if(easing=="easeOutCubic"){
			sound.volume =easeOutCubic(startV,endV,i);
			sound.pitch =easeOutCubic(startP,endP,i);
		}
		if(easing=="easeInOutCubic"){
			sound.volume =easeInOutCubic(startV,endV,i);
			sound.pitch =easeInOutCubic(startP,endP,i);
		}
		if(easing=="easeInQuart"){
			sound.volume =easeInQuart(startV,endV,i);
			sound.pitch =easeInQuart(startP,endP,i);
		}
		if(easing=="easeOutQuart"){
			sound.volume =easeOutQuart(startV,endV,i);
			sound.pitch =easeOutQuart(startP,endP,i);
		}
		if(easing=="easeInOutQuart"){
			sound.volume =easeInOutQuart(startV,endV,i);
			sound.pitch =easeInOutQuart(startP,endP,i);
		}	
		if(easing=="easeInQuint"){
			sound.volume =easeInQuint(startV,endV,i);
			sound.pitch =easeInQuint(startP,endP,i);
		}
		if(easing=="easeOutQuint"){
			sound.volume =easeOutQuint(startV,endV,i);
			sound.pitch =easeOutQuint(startP,endP,i);
		}
		if(easing=="easeInOutQuint"){
			sound.volume =easeInOutQuint(startV,endV,i);
			sound.pitch =easeInOutQuint(startP,endP,i);
		}	
		if(easing=="easeInSine"){
			sound.volume =easeInSine(startV,endV,i);
			sound.pitch =easeInSine(startP,endP,i);
		}
		if(easing=="easeOutSine"){
			sound.volume =easeOutSine(startV,endV,i);
			sound.pitch =easeOutSine(startP,endP,i);
		}
		if(easing=="easeInOutSine"){
			sound.volume =easeInOutSine(startV,endV,i);
			sound.pitch =easeInOutSine(startP,endP,i);
		}
		if(easing=="easeInExpo"){
			sound.volume =easeInExpo(startV,endV,i);
			sound.pitch =easeInExpo(startP,endP,i);
		}
		if(easing=="easeOutExpo"){
			sound.volume =easeOutExpo(startV,endV,i);
			sound.pitch =easeOutExpo(startP,endP,i);
		}
		if(easing=="easeInOutExpo"){
			sound.volume =easeInOutExpo(startV,endV,i);
			sound.pitch =easeInOutExpo(startP,endP,i);
		}
		if(easing=="easeInCirc"){
			sound.volume =easeInCirc(startV,endV,i);
			sound.pitch =easeInCirc(startP,endP,i);
		}
		if(easing=="easeOutCirc"){
			sound.volume =easeOutCirc(startV,endV,i);
			sound.pitch =easeOutCirc(startP,endP,i);
		}
		if(easing=="easeInOutCirc"){
			sound.volume =easeInOutCirc(startV,endV,i);
			sound.pitch =easeInOutCirc(startP,endP,i);
		}
		if(easing=="linear"){
			sound.volume =linear(startV,endV,i);
			sound.pitch =linear(startP,endP,i);
		}
		if(easing=="spring"){
			sound.volume =spring(startV,endV,i);
			sound.pitch =spring(startP,endP,i);
		}
		if(easing=="bounce"){
			sound.volume =bounce(startV,endV,i);
			sound.pitch =bounce(startP,endP,i);
		}
		if(easing=="easeInBack"){
			sound.volume =easeInBack(startV,endV,i);
			sound.pitch =easeInBack(startP,endP,i);
		}
		if(easing=="easeOutBack"){
			sound.volume =easeOutBack(startV,endV,i);
			sound.pitch =easeOutBack(startP,endP,i);
		}
		if(easing=="easeInOutBack"){
			sound.volume =easeInOutBack(startV,endV,i);
			sound.pitch =easeInOutBack(startP,endP,i);
		}
		yield;
	}
	sound.volume = endV;
	sound.pitch = endP;
}

private function scaleTo(x : float, y: float, z: float, duration: float, delay: float, easing: String,loop: String) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	start = obj.localScale;
	end = Vector3(x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeInQuad"){
			obj.localScale.x=easeInQuad(start.x,end.x,i);
			obj.localScale.y=easeInQuad(start.y,end.y,i);
			obj.localScale.z=easeInQuad(start.z,end.z,i);
		}
		if(easing=="easeOutQuad"){
			obj.localScale.x=easeOutQuad(start.x,end.x,i);
			obj.localScale.y=easeOutQuad(start.y,end.y,i);
			obj.localScale.z=easeOutQuad(start.z,end.z,i);
		}
		if(easing=="easeInOutQuad"){
			obj.localScale.x=easeInOutQuad(start.x,end.x,i);
			obj.localScale.y=easeInOutQuad(start.y,end.y,i);
			obj.localScale.z=easeInOutQuad(start.z,end.z,i);
		}
		if(easing=="easeInCubic"){
			obj.localScale.x=easeInCubic(start.x,end.x,i);
			obj.localScale.y=easeInCubic(start.y,end.y,i);
			obj.localScale.z=easeInCubic(start.z,end.z,i);
		}
		if(easing=="easeOutCubic"){
			obj.localScale.x=easeOutCubic(start.x,end.x,i);
			obj.localScale.y=easeOutCubic(start.y,end.y,i);
			obj.localScale.z=easeOutCubic(start.z,end.z,i);
		}
		if(easing=="easeInOutCubic"){
			obj.localScale.x=easeInOutCubic(start.x,end.x,i);
			obj.localScale.y=easeInOutCubic(start.y,end.y,i);
			obj.localScale.z=easeInOutCubic(start.z,end.z,i);
		}
		if(easing=="easeInQuart"){
			obj.localScale.x=easeInQuart(start.x,end.x,i);
			obj.localScale.y=easeInQuart(start.y,end.y,i);
			obj.localScale.z=easeInQuart(start.z,end.z,i);
		}
		if(easing=="easeOutQuart"){
			obj.localScale.x=easeOutQuart(start.x,end.x,i);
			obj.localScale.y=easeOutQuart(start.y,end.y,i);
			obj.localScale.z=easeOutQuart(start.z,end.z,i);
		}
		if(easing=="easeInOutQuart"){
			obj.localScale.x=easeInOutQuart(start.x,end.x,i);
			obj.localScale.y=easeInOutQuart(start.y,end.y,i);
			obj.localScale.z=easeInOutQuart(start.z,end.z,i);
		}	
		if(easing=="easeInQuint"){
			obj.localScale.x=easeInQuint(start.x,end.x,i);
			obj.localScale.y=easeInQuint(start.y,end.y,i);
			obj.localScale.z=easeInQuint(start.z,end.z,i);
		}
		if(easing=="easeOutQuint"){
			obj.localScale.x=easeOutQuint(start.x,end.x,i);
			obj.localScale.y=easeOutQuint(start.y,end.y,i);
			obj.localScale.z=easeOutQuint(start.z,end.z,i);
		}
		if(easing=="easeInOutQuint"){
			obj.localScale.x=easeInOutQuint(start.x,end.x,i);
			obj.localScale.y=easeInOutQuint(start.y,end.y,i);
			obj.localScale.z=easeInOutQuint(start.z,end.z,i);
		}	
		if(easing=="easeInSine"){
			obj.localScale.x=easeInSine(start.x,end.x,i);
			obj.localScale.y=easeInSine(start.y,end.y,i);
			obj.localScale.z=easeInSine(start.z,end.z,i);
		}
		if(easing=="easeOutSine"){
			obj.localScale.x=easeOutSine(start.x,end.x,i);
			obj.localScale.y=easeOutSine(start.y,end.y,i);
			obj.localScale.z=easeOutSine(start.z,end.z,i);
		}
		if(easing=="easeInOutSine"){
			obj.localScale.x=easeInOutSine(start.x,end.x,i);
			obj.localScale.y=easeInOutSine(start.y,end.y,i);
			obj.localScale.z=easeInOutSine(start.z,end.z,i);
		}
		if(easing=="easeInExpo"){
			obj.localScale.x=easeInExpo(start.x,end.x,i);
			obj.localScale.y=easeInExpo(start.y,end.y,i);
			obj.localScale.z=easeInExpo(start.z,end.z,i);
		}
		if(easing=="easeOutExpo"){
			obj.localScale.x=easeOutExpo(start.x,end.x,i);
			obj.localScale.y=easeOutExpo(start.y,end.y,i);
			obj.localScale.z=easeOutExpo(start.z,end.z,i);
		}
		if(easing=="easeInOutExpo"){
			obj.localScale.x=easeInOutExpo(start.x,end.x,i);
			obj.localScale.y=easeInOutExpo(start.y,end.y,i);
			obj.localScale.z=easeInOutExpo(start.z,end.z,i);
		}
		if(easing=="easeInCirc"){
			obj.localScale.x=easeInCirc(start.x,end.x,i);
			obj.localScale.y=easeInCirc(start.y,end.y,i);
			obj.localScale.z=easeInCirc(start.z,end.z,i);
		}
		if(easing=="easeOutCirc"){
			obj.localScale.x=easeOutCirc(start.x,end.x,i);
			obj.localScale.y=easeOutCirc(start.y,end.y,i);
			obj.localScale.z=easeOutCirc(start.z,end.z,i);
		}
		if(easing=="easeInOutCirc"){
			obj.localScale.x=easeInOutCirc(start.x,end.x,i);
			obj.localScale.y=easeInOutCirc(start.y,end.y,i);
			obj.localScale.z=easeInOutCirc(start.z,end.z,i);
		}
		if(easing=="linear"){
			obj.localScale.x=linear(start.x,end.x,i);
			obj.localScale.y=linear(start.y,end.y,i);
			obj.localScale.z=linear(start.z,end.z,i);
		}
		if(easing=="spring"){
			obj.localScale.x=spring(start.x,end.x,i);
			obj.localScale.y=spring(start.y,end.y,i);
			obj.localScale.z=spring(start.z,end.z,i);
		}
		if(easing=="bounce"){
			obj.localScale.x=bounce(start.x,end.x,i);
			obj.localScale.y=bounce(start.y,end.y,i);
			obj.localScale.z=bounce(start.z,end.z,i);
		}
		if(easing=="easeInBack"){
			obj.localScale.x=easeInBack(start.x,end.x,i);
			obj.localScale.y=easeInBack(start.y,end.y,i);
			obj.localScale.z=easeInBack(start.z,end.z,i);
		}
		if(easing=="easeOutBack"){
			obj.localScale.x=easeOutBack(start.x,end.x,i);
			obj.localScale.y=easeOutBack(start.y,end.y,i);
			obj.localScale.z=easeOutBack(start.z,end.z,i);
		}
		if(easing=="easeInOutBack"){
			obj.localScale.x=easeInOutBack(start.x,end.x,i);
			obj.localScale.y=easeInOutBack(start.y,end.y,i);
			obj.localScale.z=easeInOutBack(start.z,end.z,i);
		}
		yield;
	}
	if(loop == "loop"){
		obj.localScale=start;	
	} else if(loop == "pingpong"){
		obj.localScale=end;	
		iTween.scaleTo(obj.gameObject,start.x,start.y,start.z,duration,delay,easing,loop);
		Destroy (this);
	} else {
		obj.localScale=end;	
		Destroy (this);
	}	
}

function stab(sound: AudioSource,volume: float,pitch: float,delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	sound.volume=volume;
	sound.pitch=pitch;
	sound.PlayOneShot(sound.clip);
	yield;
}

function shake(x : float, y: float, z: float, duration: float, delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	var shakeMagnitude : Vector3 = Vector3(x,y,z);
	obj = gameObject.transform;
	start=obj.transform.position;
	var impact : boolean=true;
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
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
		
		yield;
	}
	obj.position=start;
}

private function fadeTo(endA: float, duration: float, delay: float, easing: String, loop:String) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	if(obj.guiTexture){
		start=obj.guiTexture.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeInQuad"){
				obj.guiTexture.color.a=easeInQuad(start,endA,i);
			}
			if(easing=="easeOutQuad"){
				obj.guiTexture.color.a=easeOutQuad(start,endA,i);
			}
			if(easing=="easeInOutQuad"){
				obj.guiTexture.color.a=easeInOutQuad(start,endA,i);
			}
			if(easing=="easeInCubic"){
				obj.guiTexture.color.a=easeInCubic(start,endA,i);
			}
			if(easing=="easeOutCubic"){
				obj.guiTexture.color.a=easeOutCubic(start,endA,i);
			}
			if(easing=="easeInOutCubic"){
				obj.guiTexture.color.a=easeInOutCubic(start,endA,i);
			}	
			if(easing=="easeInQuart"){
				obj.guiTexture.color.a=easeInQuart(start,endA,i);
			}
			if(easing=="easeOutQuart"){
				obj.guiTexture.color.a=easeOutQuart(start,endA,i);
			}
			if(easing=="easeInOutQuart"){
				obj.guiTexture.color.a=easeInOutQuart(start,endA,i);
			}
			if(easing=="easeInQuint"){
				obj.guiTexture.color.a=easeInQuint(start,endA,i);
			}
			if(easing=="easeOutQuint"){
				obj.guiTexture.color.a=easeOutQuint(start,endA,i);
			}
			if(easing=="easeInOutQuint"){
				obj.guiTexture.color.a=easeInOutQuint(start,endA,i);
			}	
			if(easing=="easeInSine"){
				obj.guiTexture.color.a=easeInSine(start,endA,i);
			}
			if(easing=="easeOutSine"){
				obj.guiTexture.color.a=easeOutSine(start,endA,i);
			}
			if(easing=="easeInOutSine"){
				obj.guiTexture.color.a=easeInOutSine(start,endA,i);
			}
			if(easing=="easeInExpo"){
				obj.guiTexture.color.a=easeInExpo(start,endA,i);
			}
			if(easing=="easeOutExpo"){
				obj.guiTexture.color.a=easeOutExpo(start,endA,i);
			}
			if(easing=="easeInOutExpo"){
				obj.guiTexture.color.a=easeInOutExpo(start,endA,i);
			}
			if(easing=="easeInCirc"){
				obj.guiTexture.color.a=easeInCirc(start,endA,i);
			}
			if(easing=="easeOutCirc"){
				obj.guiTexture.color.a=easeOutCirc(start,endA,i);
			}
			if(easing=="easeInOutCirc"){
				obj.guiTexture.color.a=easeInOutCirc(start,endA,i);
			}		
			if(easing=="linear"){
				obj.guiTexture.color.a=linear(start,endA,i);
			}
			if(easing=="spring"){
				obj.guiTexture.color.a=spring(start,endA,i);
			}
			if(easing=="bounce"){
				obj.guiTexture.color.a=bounce(start,endA,i);
			}
			if(easing=="easeInBack"){
				obj.guiTexture.color.a=easeInBack(start,endA,i);
			}
			if(easing=="easeOutBack"){
				obj.guiTexture.color.a=easeOutBack(start,endA,i);
			}	
			if(easing=="easeInOutBack"){
				obj.guiTexture.color.a=easeInOutBack(start,endA,i);
			}				
			yield;
		}
		if(loop == "loop"){
			guiTexture.color.a=start;
		} else if(loop == "pingpong"){
			guiTexture.color.a= endA;
			iTween.fadeTo(obj.gameObject,start,duration,delay,easing,loop);
			Destroy (this);
		} else {
			guiTexture.color.a= endA;
			Destroy (this);
		}	
	}else{
		start=obj.renderer.material.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeInQuad"){
				obj.renderer.material.color.a=easeInQuad(start,endA,i);
			}
			if(easing=="easeOutQuad"){
				obj.renderer.material.color.a=easeOutQuad(start,endA,i);
			}
			if(easing=="easeInOutQuad"){
				obj.renderer.material.color.a=easeInOutQuad(start,endA,i);
			}
			if(easing=="easeInCubic"){
				obj.renderer.material.color.a=easeInCubic(start,endA,i);
			}
			if(easing=="easeOutCubic"){
				obj.renderer.material.color.a=easeOutCubic(start,endA,i);
			}
			if(easing=="easeInOutCubic"){
				obj.renderer.material.color.a=easeInOutCubic(start,endA,i);
			}	
			if(easing=="easeInQuart"){
				obj.renderer.material.color.a=easeInQuart(start,endA,i);
			}
			if(easing=="easeOutQuart"){
				obj.renderer.material.color.a=easeOutQuart(start,endA,i);
			}
			if(easing=="easeInOutQuart"){
				obj.renderer.material.color.a=easeInOutQuart(start,endA,i);
			}
			if(easing=="easeInQuint"){
				obj.renderer.material.color.a=easeInQuint(start,endA,i);
			}
			if(easing=="easeOutQuint"){
				obj.renderer.material.color.a=easeOutQuint(start,endA,i);
			}
			if(easing=="easeInOutQuint"){
				obj.renderer.material.color.a=easeInOutQuint(start,endA,i);
			}	
			if(easing=="easeInSine"){
				obj.renderer.material.color.a=easeInSine(start,endA,i);
			}
			if(easing=="easeOutSine"){
				obj.renderer.material.color.a=easeOutSine(start,endA,i);
			}
			if(easing=="easeInOutSine"){
				obj.renderer.material.color.a=easeInOutSine(start,endA,i);
			}
			if(easing=="easeInExpo"){
				obj.renderer.material.color.a=easeInExpo(start,endA,i);
			}
			if(easing=="easeOutExpo"){
				obj.renderer.material.color.a=easeOutExpo(start,endA,i);
			}
			if(easing=="easeInOutExpo"){
				obj.renderer.material.color.a=easeInOutExpo(start,endA,i);
			}
			if(easing=="easeInCirc"){
				obj.renderer.material.color.a=easeInCirc(start,endA,i);
			}
			if(easing=="easeOutCirc"){
				obj.renderer.material.color.a=easeOutCirc(start,endA,i);
			}
			if(easing=="easeInOutCirc"){
				obj.renderer.material.color.a=easeInOutCirc(start,endA,i);
			}		
			if(easing=="linear"){
				obj.renderer.material.color.a=linear(start,endA,i);
			}
			if(easing=="spring"){
				obj.renderer.material.color.a=spring(start,endA,i);
			}
			if(easing=="bounce"){
				obj.renderer.material.color.a=bounce(start,endA,i);
			}
			if(easing=="easeInBack"){
				obj.renderer.material.color.a=easeInBack(start,endA,i);
			}
			if(easing=="easeOutBack"){
				obj.renderer.material.color.a=easeOutBack(start,endA,i);
			}
			if(easing=="easeInOutBack"){
				obj.renderer.material.color.a=easeInOutBack(start,endA,i);
			}			
			yield;
		}
		if(loop == "loop"){
			obj.renderer.material.color.a=start;
		} else if(loop == "pingpong"){
			obj.renderer.material.color.a=endA;
			iTween.fadeTo(obj.gameObject,start,duration,delay,easing,loop);
			Destroy (this);
		} else {
			obj.renderer.material.color.a= endA;
			Destroy (this);
		}
	}
}

private function moveTo(x : float, y: float, z: float, duration: float, delay: float, easing: String, loop: String) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	start = obj.position;
	end = Vector3(x, y, z);
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeInQuad"){
			obj.position.x=easeInQuad(start.x,end.x,i);
			obj.position.y=easeInQuad(start.y,end.y,i);
			obj.position.z=easeInQuad(start.z,end.z,i);
		}
		if(easing=="easeOutQuad"){
			obj.position.x=easeOutQuad(start.x,end.x,i);
			obj.position.y=easeOutQuad(start.y,end.y,i);
			obj.position.z=easeOutQuad(start.z,end.z,i);
		}
		if(easing=="easeInOutQuad"){
			obj.position.x=easeInOutQuad(start.x,end.x,i);
			obj.position.y=easeInOutQuad(start.y,end.y,i);
			obj.position.z=easeInOutQuad(start.z,end.z,i);
		}
		if(easing=="easeInCubic"){
			obj.position.x=easeInCubic(start.x,end.x,i);
			obj.position.y=easeInCubic(start.y,end.y,i);
			obj.position.z=easeInCubic(start.z,end.z,i);
		}
		if(easing=="easeOutCubic"){
			obj.position.x=easeOutCubic(start.x,end.x,i);
			obj.position.y=easeOutCubic(start.y,end.y,i);
			obj.position.z=easeOutCubic(start.z,end.z,i);
		}
		if(easing=="easeInOutCubic"){
			obj.position.x=easeInOutCubic(start.x,end.x,i);
			obj.position.y=easeInOutCubic(start.y,end.y,i);
			obj.position.z=easeInOutCubic(start.z,end.z,i);
		}
		if(easing=="easeInQuart"){
			obj.position.x=easeInQuart(start.x,end.x,i);
			obj.position.y=easeInQuart(start.y,end.y,i);
			obj.position.z=easeInQuart(start.z,end.z,i);
		}
		if(easing=="easeOutQuart"){
			obj.position.x=easeOutQuart(start.x,end.x,i);
			obj.position.y=easeOutQuart(start.y,end.y,i);
			obj.position.z=easeOutQuart(start.z,end.z,i);
		}
		if(easing=="easeInOutQuart"){
			obj.position.x=easeInOutQuart(start.x,end.x,i);
			obj.position.y=easeInOutQuart(start.y,end.y,i);
			obj.position.z=easeInOutQuart(start.z,end.z,i);
		}	
		if(easing=="easeInQuint"){
			obj.position.x=easeInQuint(start.x,end.x,i);
			obj.position.y=easeInQuint(start.y,end.y,i);
			obj.position.z=easeInQuint(start.z,end.z,i);
		}
		if(easing=="easeOutQuint"){
			obj.position.x=easeOutQuint(start.x,end.x,i);
			obj.position.y=easeOutQuint(start.y,end.y,i);
			obj.position.z=easeOutQuint(start.z,end.z,i);
		}
		if(easing=="easeInOutQuint"){
			obj.position.x=easeInOutQuint(start.x,end.x,i);
			obj.position.y=easeInOutQuint(start.y,end.y,i);
			obj.position.z=easeInOutQuint(start.z,end.z,i);
		}	
		if(easing=="easeInSine"){
			obj.position.x=easeInSine(start.x,end.x,i);
			obj.position.y=easeInSine(start.y,end.y,i);
			obj.position.z=easeInSine(start.z,end.z,i);
		}
		if(easing=="easeOutSine"){
			obj.position.x=easeOutSine(start.x,end.x,i);
			obj.position.y=easeOutSine(start.y,end.y,i);
			obj.position.z=easeOutSine(start.z,end.z,i);
		}
		if(easing=="easeInOutSine"){
			obj.position.x=easeInOutSine(start.x,end.x,i);
			obj.position.y=easeInOutSine(start.y,end.y,i);
			obj.position.z=easeInOutSine(start.z,end.z,i);
		}
		if(easing=="easeInExpo"){
			obj.position.x=easeInExpo(start.x,end.x,i);
			obj.position.y=easeInExpo(start.y,end.y,i);
			obj.position.z=easeInExpo(start.z,end.z,i);
		}
		if(easing=="easeOutExpo"){
			obj.position.x=easeOutExpo(start.x,end.x,i);
			obj.position.y=easeOutExpo(start.y,end.y,i);
			obj.position.z=easeOutExpo(start.z,end.z,i);
		}
		if(easing=="easeInOutExpo"){
			obj.position.x=easeInOutExpo(start.x,end.x,i);
			obj.position.y=easeInOutExpo(start.y,end.y,i);
			obj.position.z=easeInOutExpo(start.z,end.z,i);
		}
		if(easing=="easeInCirc"){
			obj.position.x=easeInCirc(start.x,end.x,i);
			obj.position.y=easeInCirc(start.y,end.y,i);
			obj.position.z=easeInCirc(start.z,end.z,i);
		}
		if(easing=="easeOutCirc"){
			obj.position.x=easeOutCirc(start.x,end.x,i);
			obj.position.y=easeOutCirc(start.y,end.y,i);
			obj.position.z=easeOutCirc(start.z,end.z,i);
		}
		if(easing=="easeInOutCirc"){
			obj.position.x=easeInOutCirc(start.x,end.x,i);
			obj.position.y=easeInOutCirc(start.y,end.y,i);
			obj.position.z=easeInOutCirc(start.z,end.z,i);
		}
		if(easing=="linear"){
			obj.position.x=linear(start.x,end.x,i);
			obj.position.y=linear(start.y,end.y,i);
			obj.position.z=linear(start.z,end.z,i);
		}
		if(easing=="spring"){
			obj.position.x=spring(start.x,end.x,i);
			obj.position.y=spring(start.y,end.y,i);
			obj.position.z=spring(start.z,end.z,i);
		}
		if(easing=="bounce"){
			obj.position.x=bounce(start.x,end.x,i);
			obj.position.y=bounce(start.y,end.y,i);
			obj.position.z=bounce(start.z,end.z,i);
		}
		if(easing=="easeInBack"){
			obj.position.x=easeInBack(start.x,end.x,i);
			obj.position.y=easeInBack(start.y,end.y,i);
			obj.position.z=easeInBack(start.z,end.z,i);
		}
		if(easing=="easeOutBack"){
			obj.position.x=easeOutBack(start.x,end.x,i);
			obj.position.y=easeOutBack(start.y,end.y,i);
			obj.position.z=easeOutBack(start.z,end.z,i);
		}
		if(easing=="easeInOutBack"){
			obj.position.x=easeInOutBack(start.x,end.x,i);
			obj.position.y=easeInOutBack(start.y,end.y,i);
			obj.position.z=easeInOutBack(start.z,end.z,i);
		}
		yield;
	}
	if(loop == "loop"){
		obj.position=start;
	} else if(loop == "pingpong"){
		obj.position=end;
		iTween.moveTo(obj.gameObject,start.x,start.y,start.z,duration,delay,easing,loop);
		Destroy (this);
	} else {
		obj.position=end;
		Destroy (this);
	}	
}

private function rotateBy(x : float, y: float, z: float, duration: float, delay: float, easing: String,loop: String) {
	if(guiTexture){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	start = Vector3(obj.localEulerAngles.x,obj.localEulerAngles.y,obj.localEulerAngles.z);
	end = Vector3(360*x + obj.localEulerAngles.x, 360*y + obj.localEulerAngles.y, 360 *z + obj.localEulerAngles.x);
		
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeInQuad"){
			obj.localRotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));
		}
		if(easing=="easeOutQuad"){
			obj.localRotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));
		}
		if(easing=="easeInOutQuad"){
			obj.localRotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));
		}
		if(easing=="easeInCubic"){
			obj.localRotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));
		}
		if(easing=="easeOutCubic"){
			obj.localRotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));
		}
		if(easing=="easeInOutCubic"){
			obj.localRotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));
		}
		if(easing=="easeInQuart"){
			obj.localRotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));
		}
		if(easing=="easeOutQuart"){
			obj.localRotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));
		}
		if(easing=="easeInOutQuart"){
			obj.localRotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));
		}	
		if(easing=="easeInQuint"){
			obj.localRotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));
		}
		if(easing=="easeOutQuint"){
			obj.localRotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));
		}
		if(easing=="easeInOutQuint"){
			obj.localRotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));
		}	
		if(easing=="easeInSine"){
			obj.localRotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));
		}
		if(easing=="easeOutSine"){
			obj.localRotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));
		}
		if(easing=="easeInOutSine"){
			obj.localRotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));
		}	
		if(easing=="easeInExpo"){
			obj.localRotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));
		}
		if(easing=="easeOutExpo"){
			obj.localRotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));
		}
		if(easing=="easeInOutExpo"){
			obj.localRotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));
		}	
		if(easing=="easeInCirc"){
			obj.localRotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));
		}
		if(easing=="easeOutCirc"){
			obj.localRotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));
		}
		if(easing=="easeInOutCirc"){
			obj.localRotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));
		}
		if(easing=="linear"){
			obj.localRotation=Quaternion.Euler(linear(start.x,end.x,i),linear(start.y,end.y,i),linear(start.z,end.z,i));
		}
		if(easing=="spring"){
			obj.localRotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));
		}
		if(easing=="bounce"){
			obj.localRotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));
		}
		if(easing=="easeInBack"){
			obj.localRotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));
		}
		if(easing=="easeOutBack"){
			obj.localRotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));
		}
		if(easing=="easeInOutBack"){
			obj.localRotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));
		}
		yield;
	}
	if(loop == "loop"){
		obj.localRotation=Quaternion.Euler(start.x,start.y,start.z);
	} else if(loop == "pingpong"){
		obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
		iTween.rotateBy(obj.gameObject,-x,-y,-z,duration,delay,easing,loop);
		Destroy (this);
	} else {
		obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
		Destroy (this);
	}
}

private function rotateTo(x : float, y: float, z: float, duration: float, delay: float, easing: String,loop:String) {
	if(guiTexture){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	start = Vector3(obj.localEulerAngles.x,obj.localEulerAngles.y,obj.localEulerAngles.z);
	end = Vector3(clerp(start.x,x,1), clerp(start.y,y,1), clerp(start.z,z,1));

	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
		if(easing=="easeInQuad"){
			obj.localRotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));
		}
		if(easing=="easeOutQuad"){
			obj.localRotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));
		}
		if(easing=="easeInOutQuad"){
			obj.localRotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));
		}
		if(easing=="easeInCubic"){
			obj.localRotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));
		}
		if(easing=="easeOutCubic"){
			obj.localRotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));
		}
		if(easing=="easeInOutCubic"){
			obj.localRotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));
		}
		if(easing=="easeInQuart"){
			obj.localRotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));
		}
		if(easing=="easeOutQuart"){
			obj.localRotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));
		}
		if(easing=="easeInOutQuart"){
			obj.localRotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));
		}	
		if(easing=="easeInQuint"){
			obj.localRotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));
		}
		if(easing=="easeOutQuint"){
			obj.localRotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));
		}
		if(easing=="easeInOutQuint"){
			obj.localRotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));
		}	
		if(easing=="easeInSine"){
			obj.localRotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));
		}
		if(easing=="easeOutSine"){
			obj.localRotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));
		}
		if(easing=="easeInOutSine"){
			obj.localRotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));
		}	
		if(easing=="easeInExpo"){
			obj.localRotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));
		}
		if(easing=="easeOutExpo"){
			obj.localRotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));
		}
		if(easing=="easeInOutExpo"){
			obj.localRotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));
		}	
		if(easing=="easeInCirc"){
			obj.localRotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));
		}
		if(easing=="easeOutCirc"){
			obj.localRotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));
		}
		if(easing=="easeInOutCirc"){
			obj.localRotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));
		}
		if(easing=="linear"){
			obj.localRotation=Quaternion.Euler(clerp(start.x,end.x,i),clerp(start.y,end.y,i),clerp(start.z,end.z,i));
		}
		if(easing=="spring"){
			obj.localRotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));
		}
		if(easing=="bounce"){
			obj.localRotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));
		}
		if(easing=="easeInBack"){
			obj.localRotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));
		}
		if(easing=="easeOutBack"){
			obj.localRotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));
		}
		if(easing=="easeInOutBack"){
			obj.localRotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));
		}
		yield;
	}
	if(loop == "loop"){
		obj.localRotation=Quaternion.Euler(start.x,start.y,start.z);
	} else if(loop == "pingpong"){
		obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
		iTween.rotateTo(obj.gameObject,start.x,start.y,start.z,duration,delay,easing,loop);
		Destroy (this);
	} else {
		obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
		Destroy (this);
	}	
}

private function colorTo(r: float,g: float,b: float,duration: float, delay: float, easing: String, loop:String) {
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	obj = gameObject.transform;
	if(obj.guiTexture){
		start=obj.guiTexture.color;
		end = Color (r,g,b);
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeInQuad"){
				obj.guiTexture.color.r=easeInQuad(start.r,end.r,i);
				obj.guiTexture.color.g=easeInQuad(start.g,end.g,i);
				obj.guiTexture.color.b=easeInQuad(start.b,end.b,i);
			}
			if(easing=="easeOutQuad"){
				obj.guiTexture.color.r=easeOutQuad(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutQuad(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutQuad(start.b,end.b,i);
			}
			if(easing=="easeInOutQuad"){
				obj.guiTexture.color.r=easeInOutQuad(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutQuad(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutQuad(start.b,end.b,i);
			}
			if(easing=="easeInCubic"){
				obj.guiTexture.color.r=easeInCubic(start.r,end.r,i);
				obj.guiTexture.color.g=easeInCubic(start.g,end.g,i);
				obj.guiTexture.color.b=easeInCubic(start.b,end.b,i);
			}
			if(easing=="easeOutCubic"){
				obj.guiTexture.color.r=easeOutCubic(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutCubic(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutCubic(start.b,end.b,i);
			}
			if(easing=="easeInOutCubic"){
				obj.guiTexture.color.r=easeInOutCubic(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutCubic(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutCubic(start.b,end.b,i);
			}	
			if(easing=="easeInQuart"){
				obj.guiTexture.color.r=easeInQuart(start.r,end.r,i);
				obj.guiTexture.color.g=easeInQuart(start.g,end.g,i);
				obj.guiTexture.color.b=easeInQuart(start.b,end.b,i);
			}
			if(easing=="easeOutQuart"){
				obj.guiTexture.color.r=easeOutQuart(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutQuart(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutQuart(start.b,end.b,i);
			}
			if(easing=="easeInOutQuart"){
				obj.guiTexture.color.r=easeInOutQuart(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutQuart(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutQuart(start.b,end.b,i);
			}
			if(easing=="easeInQuint"){
				obj.guiTexture.color.r=easeInQuint(start.r,end.r,i);
				obj.guiTexture.color.g=easeInQuint(start.g,end.g,i);
				obj.guiTexture.color.b=easeInQuint(start.b,end.b,i);
			}
			if(easing=="easeOutQuint"){
				obj.guiTexture.color.r=easeOutQuint(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutQuint(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutQuint(start.b,end.b,i);
			}
			if(easing=="easeInOutQuint"){
				obj.guiTexture.color.r=easeInOutQuint(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutQuint(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutQuint(start.b,end.b,i);
			}	
			if(easing=="easeInSine"){
				obj.guiTexture.color.r=easeInSine(start.r,end.r,i);
				obj.guiTexture.color.g=easeInSine(start.g,end.g,i);
				obj.guiTexture.color.b=easeInSine(start.b,end.b,i);
			}
			if(easing=="easeOutSine"){
				obj.guiTexture.color.r=easeOutSine(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutSine(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutSine(start.b,end.b,i);
			}
			if(easing=="easeInOutSine"){
				obj.guiTexture.color.r=easeInOutSine(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutSine(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutSine(start.b,end.b,i);
			}
			if(easing=="easeInExpo"){
				obj.guiTexture.color.r=easeInExpo(start.r,end.r,i);
				obj.guiTexture.color.g=easeInExpo(start.g,end.g,i);
				obj.guiTexture.color.b=easeInExpo(start.b,end.b,i);
			}
			if(easing=="easeOutExpo"){
				obj.guiTexture.color.r=easeOutExpo(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutExpo(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutExpo(start.b,end.b,i);
			}
			if(easing=="easeInOutExpo"){
				obj.guiTexture.color.r=easeInOutExpo(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutExpo(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutExpo(start.b,end.b,i);
			}
			if(easing=="easeInCirc"){
				obj.guiTexture.color.r=easeInCirc(start.r,end.r,i);
				obj.guiTexture.color.g=easeInCirc(start.g,end.g,i);
				obj.guiTexture.color.b=easeInCirc(start.b,end.b,i);
			}
			if(easing=="easeOutCirc"){
				obj.guiTexture.color.r=easeOutCirc(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutCirc(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutCirc(start.b,end.b,i);
			}
			if(easing=="easeInOutCirc"){
				obj.guiTexture.color.r=easeInOutCirc(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutCirc(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutCirc(start.b,end.b,i);
			}		
			if(easing=="linear"){
				obj.guiTexture.color.r=linear(start.r,end.r,i);
				obj.guiTexture.color.g=linear(start.g,end.g,i);
				obj.guiTexture.color.b=linear(start.b,end.b,i);
			}
			if(easing=="spring"){
				obj.guiTexture.color.r=spring(start.r,end.r,i);
				obj.guiTexture.color.g=spring(start.g,end.g,i);
				obj.guiTexture.color.b=spring(start.b,end.b,i);
			}
			if(easing=="bounce"){
				obj.guiTexture.color.r=bounce(start.r,end.r,i);
				obj.guiTexture.color.g=bounce(start.g,end.g,i);
				obj.guiTexture.color.b=bounce(start.b,end.b,i);
			}
			if(easing=="easeInBack"){
				obj.guiTexture.color.r=easeInBack(start.r,end.r,i);
				obj.guiTexture.color.g=easeInBack(start.g,end.g,i);
				obj.guiTexture.color.b=easeInBack(start.b,end.b,i);
			}
			if(easing=="easeOutBack"){
				obj.guiTexture.color.r=easeOutBack(start.r,end.r,i);
				obj.guiTexture.color.g=easeOutBack(start.g,end.g,i);
				obj.guiTexture.color.b=easeOutBack(start.b,end.b,i);
			}	
			if(easing=="easeInOutBack"){
				obj.guiTexture.color.r=easeInOutBack(start.r,end.r,i);
				obj.guiTexture.color.g=easeInOutBack(start.g,end.g,i);
				obj.guiTexture.color.b=easeInOutBack(start.b,end.b,i);
			}			
			yield;
		}
		if(loop == "loop"){
			obj.guiTexture.color.r=start.r;
			obj.guiTexture.color.g=start.g;
			obj.guiTexture.color.b=start.b;	
		} else if(loop == "pingpong"){
			obj.guiTexture.color.r=end.r;
			obj.guiTexture.color.g=end.g;
			obj.guiTexture.color.b=end.b;
			iTween.colorTo(obj.gameObject,start.r,start.g,start.b,duration,delay,easing,loop);
			Destroy (this);
		} else {
			obj.guiTexture.color.r=end.r;
			obj.guiTexture.color.g=end.g;
			obj.guiTexture.color.b=end.b;
			Destroy (this);
		}	
	}else{
		start=obj.renderer.material.color;
		end = Color (r,g,b);
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/duration)) {
			if(easing=="easeInQuad"){
				obj.renderer.material.color.r=easeInQuad(start.r,end.r,i);
				obj.renderer.material.color.g=easeInQuad(start.g,end.g,i);
				obj.renderer.material.color.b=easeInQuad(start.b,end.b,i);
			}
			if(easing=="easeOutQuad"){
				obj.renderer.material.color.r=easeOutQuad(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutQuad(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutQuad(start.b,end.b,i);
			}
			if(easing=="easeInOutQuad"){
				obj.renderer.material.color.r=easeInOutQuad(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutQuad(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutQuad(start.b,end.b,i);
			}
			if(easing=="easeInCubic"){
				obj.renderer.material.color.r=easeInCubic(start.r,end.r,i);
				obj.renderer.material.color.g=easeInCubic(start.g,end.g,i);
				obj.renderer.material.color.b=easeInCubic(start.b,end.b,i);
			}
			if(easing=="easeOutCubic"){
				obj.renderer.material.color.r=easeOutCubic(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutCubic(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutCubic(start.b,end.b,i);
			}
			if(easing=="easeInOutCubic"){
				obj.renderer.material.color.r=easeInOutCubic(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutCubic(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutCubic(start.b,end.b,i);
			}	
			if(easing=="easeInQuart"){
				obj.renderer.material.color.r=easeInQuart(start.r,end.r,i);
				obj.renderer.material.color.g=easeInQuart(start.g,end.g,i);
				obj.renderer.material.color.b=easeInQuart(start.b,end.b,i);
			}
			if(easing=="easeOutQuart"){
				obj.renderer.material.color.r=easeOutQuart(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutQuart(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutQuart(start.b,end.b,i);
			}
			if(easing=="easeInOutQuart"){
				obj.renderer.material.color.r=easeInOutQuart(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutQuart(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutQuart(start.b,end.b,i);
			}
			if(easing=="easeInQuint"){
				obj.renderer.material.color.r=easeInQuint(start.r,end.r,i);
				obj.renderer.material.color.g=easeInQuint(start.g,end.g,i);
				obj.renderer.material.color.b=easeInQuint(start.b,end.b,i);
			}
			if(easing=="easeOutQuint"){
				obj.renderer.material.color.r=easeOutQuint(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutQuint(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutQuint(start.b,end.b,i);
			}
			if(easing=="easeInOutQuint"){
				obj.renderer.material.color.r=easeInOutQuint(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutQuint(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutQuint(start.b,end.b,i);
			}	
			if(easing=="easeInSine"){
				obj.renderer.material.color.r=easeInSine(start.r,end.r,i);
				obj.renderer.material.color.g=easeInSine(start.g,end.g,i);
				obj.renderer.material.color.b=easeInSine(start.b,end.b,i);
			}
			if(easing=="easeOutSine"){
				obj.renderer.material.color.r=easeOutSine(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutSine(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutSine(start.b,end.b,i);
			}
			if(easing=="easeInOutSine"){
				obj.renderer.material.color.r=easeInOutSine(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutSine(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutSine(start.b,end.b,i);
			}
			if(easing=="easeInExpo"){
				obj.renderer.material.color.r=easeInExpo(start.r,end.r,i);
				obj.renderer.material.color.g=easeInExpo(start.g,end.g,i);
				obj.renderer.material.color.b=easeInExpo(start.b,end.b,i);
			}
			if(easing=="easeOutExpo"){
				obj.renderer.material.color.r=easeOutExpo(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutExpo(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutExpo(start.b,end.b,i);
			}
			if(easing=="easeInOutExpo"){
				obj.renderer.material.color.r=easeInOutExpo(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutExpo(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutExpo(start.b,end.b,i);
			}
			if(easing=="easeInCirc"){
				obj.renderer.material.color.r=easeInCirc(start.r,end.r,i);
				obj.renderer.material.color.g=easeInCirc(start.g,end.g,i);
				obj.renderer.material.color.b=easeInCirc(start.b,end.b,i);
			}
			if(easing=="easeOutCirc"){
				obj.renderer.material.color.r=easeOutCirc(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutCirc(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutCirc(start.b,end.b,i);
			}
			if(easing=="easeInOutCirc"){
				obj.renderer.material.color.r=easeInOutCirc(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutCirc(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutCirc(start.b,end.b,i);
			}		
			if(easing=="linear"){
				obj.renderer.material.color.r=linear(start.r,end.r,i);
				obj.renderer.material.color.g=linear(start.g,end.g,i);
				obj.renderer.material.color.b=linear(start.b,end.b,i);
			}
			if(easing=="spring"){
				obj.renderer.material.color.r=spring(start.r,end.r,i);
				obj.renderer.material.color.g=spring(start.g,end.g,i);
				obj.renderer.material.color.b=spring(start.b,end.b,i);
			}
			if(easing=="bounce"){
				obj.renderer.material.color.r=bounce(start.r,end.r,i);
				obj.renderer.material.color.g=bounce(start.g,end.g,i);
				obj.renderer.material.color.b=bounce(start.b,end.b,i);
			}
			if(easing=="easeInBack"){
				obj.renderer.material.color.r=easeInBack(start.r,end.r,i);
				obj.renderer.material.color.g=easeInBack(start.g,end.g,i);
				obj.renderer.material.color.b=easeInBack(start.b,end.b,i);
			}
			if(easing=="easeOutBack"){
				obj.renderer.material.color.r=easeOutBack(start.r,end.r,i);
				obj.renderer.material.color.g=easeOutBack(start.g,end.g,i);
				obj.renderer.material.color.b=easeOutBack(start.b,end.b,i);
			}	
			if(easing=="easeInOutBack"){
				obj.renderer.material.color.r=easeInOutBack(start.r,end.r,i);
				obj.renderer.material.color.g=easeInOutBack(start.g,end.g,i);
				obj.renderer.material.color.b=easeInOutBack(start.b,end.b,i);
			}			
			yield;
		}
		if(loop == "loop"){
			obj.renderer.material.color.r=start.r;
			obj.renderer.material.color.g=start.g;
			obj.renderer.material.color.b=start.b;	
		} else if(loop == "pingpong"){
			obj.renderer.material.color.r=end.r;
			obj.renderer.material.color.g=end.g;
			obj.renderer.material.color.b=end.b;
			iTween.colorTo(obj.gameObject,start.r,start.g,start.b,duration,delay,easing,loop);
			Destroy (this);
		} else {
			obj.renderer.material.color.r=end.r;
			obj.renderer.material.color.g=end.g;
			obj.renderer.material.color.b=end.b;
			Destroy (this);
		}		
	}
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
private function linear(start : float, end : float, value : float) : float{
	return Mathf.Lerp(start, end, value);
}

private function clerp(start : float, end : float, value : float) : float {
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

private function spring(start : float, end : float, value : float) : float{
    value = Mathf.Clamp01(value);
    value = (Mathf.Sin(value * Mathf.PI * (0.2 + 2.5 * value * value * value)) * Mathf.Pow(1 - value, 2.2) + value) * (1 + (1.2 * (1 - value)));
    return start + (end - start) * value;
}

private function easeInQuad(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value + start;
}

private function easeOutQuad(start : float, end : float, value : float) : float{
	value /= 1;
	end -= start;
	return -end * value*(value-2) + start;
}

private function easeInOutQuad(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2*value*value + start;
	value--;
	return -end/2 * (value*(value-2) - 1) + start;
};

private function easeInCubic (start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value*value + start;
};

private function easeOutCubic (start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return end*(value*value*value + 1) + start;
};

private function easeInOutCubic (start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2*value*value*value + start;
	value -= 2;
	return end/2*(value*value*value + 2) + start;
};

private function easeInQuart (start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value*value*value + start;
};

private function easeOutQuart(start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return -end * (value*value*value*value - 1) + start;
};

private function easeInOutQuart(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2*value*value*value*value + start;
	value -= 2;
	return -end/2 * (value*value*value*value - 2) + start;
};

private function easeInQuint(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value*value*value*value + start;
};

private function easeOutQuint(start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return end*(value*value*value*value*value + 1) + start;
};

private function easeInOutQuint(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2*value*value*value*value*value + start;
	value -= 2;
	return end/2*(value*value*value*value*value + 2) + start;
};

private function easeInSine(start : float, end : float, value : float) : float {
	end -= start;
	return -end * Mathf.Cos(value/1 * (Mathf.PI/2)) + end + start;
};

private function easeOutSine(start : float, end : float, value : float) : float {
	end -= start;
	return end * Mathf.Sin(value/1 * (Mathf.PI/2)) + start;
};

private function easeInOutSine(start : float, end : float, value : float) : float {
	end -= start;
	return -end/2 * (Mathf.Cos(Mathf.PI*value/1) - 1) + start;
};

private function easeInExpo(start : float, end : float, value : float) : float {
	end -= start;
	return end * Mathf.Pow(2, 10 * (value/1 - 1) ) + start;
};

private function easeOutExpo(start : float, end : float, value : float) : float {
	end -= start;
	return end * ( -Mathf.Pow( 2, -10 * value/1 ) + 1 ) + start;
};

private function easeInOutExpo(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return end/2 * Mathf.Pow( 2, 10 * (value - 1) ) + start;
	value--;
	return end/2 * ( -Mathf.Pow( 2, -10 * value) + 2 ) + start;
};

private function easeInCirc(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return -end * (Mathf.Sqrt(1 - value*value) - 1) + start;
};

private function easeOutCirc(start : float, end : float, value : float) : float {
	value /= 1;
	value--;
	end -= start;
	return end * Mathf.Sqrt(1 - value*value) + start;
};

private function easeInOutCirc(start : float, end : float, value : float) : float {
	value /= .5;
	end -= start;
	if (value < 1) return -end/2 * (Mathf.Sqrt(1 - value*value) - 1) + start;
	value -= 2;
	return end/2 * (Mathf.Sqrt(1 - value*value) + 1) + start;
};

private function bounce (start : float, end : float, value : float) : float {
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

function punch (amplitude : float, value: float) : float {	
	var start=0;
	var end=0;
	var s : float;
	
	if (value==0){
		return start; 
	}
	
	value /= 1;
	
	if (value==1){
		return start+end;  
	}
	
	var period=1*.3;
	
	if (amplitude < Mathf.Abs(end)) { 
		amplitude=end; 
		s=period/4;
	}else {
		s = period/(2*Mathf.PI) * Mathf.Asin (end/amplitude);
		return (amplitude*Mathf.Pow(2,-10*value) * Mathf.Sin((value*1-s)*(2*Mathf.PI)/period) + end + start);
	}
}