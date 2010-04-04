//VERSION: 1.0.7

/*
 Copyright (c) 2010 Bob Berkebile(http://www.pixelplacement.com), C# port by Patrick Corkum(http://www.insquare.com)

 Permission is hereby granted, free of charge, to any person  obtaining a copy of this software and associated documentation  files (the "Software"), to deal in the Software without  restriction, including without limitation the rights to use,  copy, modify, merge, publish, distribute, sublicense, and/or sell  copies of the Software, and to permit persons to whom the  Software is furnished to do so, subject to the following conditions:
 The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/* 
 
/*
TERMS OF USE - EASING EQUATIONS

Open source under the BSD License.

Copyright © 2001 Robert Penner
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of the author nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

static var registers : Array = new Array();
static var params : Array = new Array();
public var tweenType: String;
public var id : int = 0;
public var inProgress : boolean;

//Defaults:
public static var rotateToDefaults : Hashtable = {"time":1,"delay":0,"transition":"easeInOutCubic"};
public static var rotateByDefaults : Hashtable = {"time":1,"delay":0,"transition":"easeInOutCubic"};
public static var stabDefaults : Hashtable = {"volume":1,"pitch":1,"delay":0};
public static var shakeDefaults : Hashtable = {"time":1,"delay":0};
public static var audioDefaults : Hashtable = {"time":1,"delay":0,"volume":1,"pitch":1,"transition":"linear"};
public static var scaleDefaults : Hashtable = {"time":1,"delay":0,"transition":"easeInOutCubic"};
public static var fadeDefaults : Hashtable = {"time":1,"delay":0,"transition":"linear"};
public static var moveDefaults : Hashtable = {"time":1,"delay":0,"transition":"easeInOutCubic"};
public static var punchPositionDefaults : Hashtable = {"time":1,"delay":0};
public static var punchRotationDefaults : Hashtable = {"time":1,"delay":0};
public static var colorDefaults : Hashtable = {"time":1,"delay":0,"transition":"linear"};

//Check for and remove running tweens of same type:
private function checkForConflicts(type:String):void{
	var scripts = GetComponents (iTween);
	if(scripts.Length>1){
		for (var script : iTween in scripts) {
			if(script.inProgress && script.tweenType==type){
				Destroy(script);
			}
		}
	}
}

//Stops (and removes) tweening on an object:
static function stop(obj: GameObject){
        var scripts = obj.GetComponents (iTween);
        for (var script : iTween in scripts) {
                Destroy(script);
        }
}

//Stops (and removes) tweening of a certain type on an object derived from the root of the type (i.e. "moveTo" = "move"):
static function stopType(obj: GameObject, stopType: String){
	var scripts = obj.GetComponents (iTween);
	for (var script : iTween in scripts) {
		var currentType : String = script.tweenType;
		print(stopType.length);
		if(currentType.Substring(0,stopType.length)==stopType){
			Destroy(script);
		}
	}
}

//Tween count retrieval:
static function tweenCount(obj: GameObject) : int{
	var scripts = obj.GetComponents (iTween);
	return scripts.Length;
}

//Retrieves register ID:
private function findRegister(){
	for(i = 0; i < registers.length; i++){
		if(registers[i]==gameObject){
			id =  i;
		}
	}
}

//Registers and preps for static to component swap:
static function init(target: GameObject,args: Hashtable):void{
	registers.push(target);
	params.push(args);
	target.AddComponent ("iTween");
}

//Fade to static register:
static function fadeTo(target: GameObject,args: Hashtable):void{
	args.Add("type","fadeTo");	
	init(target,args);
}

//Fade from static register:
static function fadeFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
	if(args["alpha"]==null){
		args["alpha"] = 0;	
	}
	
	if(target.guiTexture){
		destinationHold=target.guiTexture.color.a;
		target.guiTexture.color.a=args["alpha"];
		args["alpha"]=destinationHold;
	}else{
		destinationHold=target.renderer.material.color.a;
		target.renderer.material.color.a=args["alpha"];
		args["alpha"]=destinationHold;
	}				
	args.Add("type","fadeTo");	
	init(target,args);
}

//Move by static register:
static function moveBy(target: GameObject,args: Hashtable):void{
	args.Add("type","moveTo");
	
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	if(args.Contains("x")){
		xValue+=target.transform.localPosition.x;
		args["x"]=xValue;
	}
	if(args.Contains("y")){
		yValue+=target.transform.localPosition.y;
		args["y"]=yValue;
	}
	if(args.Contains("z")){
		zValue+=target.transform.localPosition.z;
		args["z"]=zValue;
	}
	init(target,args);
}

//Move to static register:
static function moveTo(target: GameObject,args: Hashtable):void{
	args.Add("type","moveTo");	
	init(target,args);
}

//Move from static register:
static function moveFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
	if(args.Contains("x")){
		destinationHold = target.transform.localPosition.x;
		target.transform.localPosition.x=args["x"];
		args["x"]=destinationHold;
	}else{
		args["x"]=target.transform.localPosition.x;
	}

	if(args.Contains("y")){
		destinationHold = target.transform.localPosition.y;
		target.transform.localPosition.y=args["y"];
		args["y"]=destinationHold;
	}else{
		args["y"]=target.transform.localPosition.y;
	}

	if(args.Contains("z")){
		destinationHold = target.transform.localPosition.z;
		target.transform.position.z=args["z"];
		args["z"]=destinationHold;
	}else{
		args["z"]=target.transform.localPosition.z;
	}
	args.Add("type","moveTo");
	init(target,args);
}

//Scale by static register:
static function scaleBy(target: GameObject,args: Hashtable):void{
	args.Add("type","scaleTo");
	
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	if(args.Contains("x")){
		xValue*=target.transform.localScale.x;
		args["x"]=xValue;
	}
	if(args.Contains("y")){
		yValue*=target.transform.localScale.y;
		args["y"]=yValue;
	}
	if(args.Contains("z")){
		zValue*=target.transform.localScale.z;
		args["z"]=zValue;
	}
	init(target,args);
}

//Scale to static register:
static function scaleTo(target: GameObject,args: Hashtable):void{
	args.Add("type","scaleTo");	
	init(target,args);
}

//Scale from static register:
static function scaleFrom(target: GameObject,args: Hashtable){
	var destinationHold : float;
	
	if(args.Contains("x")){
		destinationHold = target.transform.localScale.x;
		target.transform.localScale.x=args["x"];
		args["x"]=destinationHold;
	}else{
		args["x"]=target.transform.localScale.x;
	}
	
	if(args.Contains("y")){
		destinationHold = target.transform.localScale.y;
		target.transform.localScale.y=args["y"];
		args["y"]=destinationHold;
	}else{
		args["y"]=target.transform.localScale.y;
	}
	
	if(args.Contains("z")){
		destinationHold = target.transform.localScale.z;
		target.transform.localScale.z=args["z"];
		args["z"]=destinationHold;
	}else{
		args["z"]=target.transform.localScale.z;
	}
	args.Add("type","scaleTo");	
	init(target,args);
}

//Rotate to static register:
static function rotateTo(target: GameObject,args: Hashtable):void{
	args.Add("type","rotateTo");	
	init(target,args);
}

//Rotate from static register:
static function rotateFrom(target: GameObject,args: Hashtable){
	if(target.guiTexture || target.guiText){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	
	var destinationHold : float;

	if(args.Contains("x")){
		destinationHold = target.transform.eulerAngles.x;
		target.transform.eulerAngles.x=args["x"];
		args["x"]=destinationHold;
	}else{
		args["x"]=target.transform.eulerAngles.x;
	}
	
	if(args.Contains("y")){
		destinationHold = target.transform.eulerAngles.y;
		target.transform.eulerAngles.y=args["y"];
		args["y"]=destinationHold;
	}else{
		args["y"]=target.transform.eulerAngles.y;
	}
	
	if(args.Contains("z")){
		destinationHold = target.transform.eulerAngles.z;
		target.transform.eulerAngles.z=args["z"];
		args["z"]=destinationHold;
	}else{
		args["z"]=target.transform.eulerAngles.z;
	}
	args.Add("type","rotateTo");	
	init(target,args);
}

//Rotate by static register:
static function rotateBy(target: GameObject,args: Hashtable):void{
	args.Add("type","rotateBy");	
	init(target,args);
}

//Color from static register:
static function colorFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : Color;
	
	if(args["r"]==null){
		args["r"] = 0;	
	}
	if(args["g"]==null){
		args["g"] = 0;	
	}
	if(args["b"]==null){
		args["b"] = 0;	
	}
	
	if(target.guiTexture){
		destinationHold=target.guiTexture.color;
		target.guiTexture.color.r=args["r"];
		target.guiTexture.color.g=args["g"];
		target.guiTexture.color.b=args["b"];
		args["r"]=destinationHold.r;
		args["g"]=destinationHold.g;
		args["b"]=destinationHold.b;
	}else{
		destinationHold=target.renderer.material.color;
		target.renderer.material.color.r=args["r"];
		target.renderer.material.color.g=args["g"];
		target.renderer.material.color.b=args["b"];
		args["r"]=destinationHold.r;
		args["g"]=destinationHold.g;
		args["b"]=destinationHold.b;
	}				
	args.Add("type","colorTo");	
	init(target,args);
}

//Color to static register:
static function colorTo(target: GameObject,args: Hashtable):void{
	args.Add("type","colorTo");	
	init(target,args);
}

//Punch position static register:
static function punchPosition(target: GameObject,args: Hashtable):void{
	args.Add("type","punchPosition");	
	init(target,args);
}

//Punch rotation static register:
static function punchRotation(target: GameObject,args: Hashtable):void{
	args.Add("type","punchRotation");	
	init(target,args);
}

//Shake static register:
static function shake(target: GameObject,args: Hashtable):void{
	args.Add("type","shake");	
	init(target,args);
}

//Stab static register:
static function stab(target: GameObject,args: Hashtable):void{
	args.Add("type","stab");	
	init(target,args);
}

//Audio to static register:
static function audioTo(target: GameObject,args: Hashtable):void{
	args.Add("type","audioTo");	
	init(target,args);
}

//Audio from static register:
static function audioFrom(target: GameObject,args: Hashtable):void{
	var destinationVolume : float;
	var destinationPitch : float;
	
	if(args["audioSource"]==null){
		args["audioSource"]=target.audio;
	}
	if(args["volume"]==null){
		args.Add("volume",audioDefaults["volume"]);
	}
	if(args["pitch"]==null){
		args.Add("pitch",audioDefaults["pitch"]);
	}
	
	var sound : AudioSource = args["audioSource"];
	
	destinationVolume = sound.volume;
	destinationPitch = sound.pitch;
	
	sound.volume=args["volume"];
	sound.pitch=args["pitch"];
	
	args["volume"]=destinationVolume;
	args["pitch"]=destinationPitch;
	
	args.Add("type","audioTo");	
	init(target,args);
}

function Start(){
	findRegister();
	var args : Hashtable = params[id];
	registers.RemoveAt(id);
	params.RemoveAt(id);
	tweenType=args["type"];
	switch(args["type"]){
		case "fadeTo":
			while (true){
				yield fadeTo(args);
			}
			break;
		case "moveTo":
			while (true){
				yield moveTo(args);
			}
			break;
		case "scaleTo":
			while (true){
				yield scaleTo(args);
			}
			break;
		case "rotateTo":
			while (true){
				yield rotateTo(args);
			}
			break;
		case "rotateBy":
			while (true){
				yield rotateBy(args);
			}
			break;	
		case "colorTo":
			while (true){
				yield colorTo(args);
			}
			break;
		case "punchPosition":
			while (true){
				yield punchPosition(args);
			}
			break;
		case "punchRotation":
			while (true){
				yield punchRotation(args);
			}
			break;
		case "shake":
			while (true){
				yield shake(args);
			}
			break;
		case "audioTo":
			while (true){
				yield audioTo(args);
			}
			break;
		case "stab":
			stab(args);
			break;
	}
}


//Audio to application:
private function audioTo(args:Hashtable) {
	//construct args:
	if(args["time"]==null){
		args.Add("time",audioDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",audioDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",audioDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	if(args["audioSource"]==null){
		args.Add("audioSource",audio);
	}
	if(args["volume"]==null){
		args.Add("volume",stabDefaults["volume"]);
	}
	if(args["pitch"]==null){
		args.Add("pitch",stabDefaults["pitch"]);
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var sound : AudioSource = args["audioSource"];
	var startV : float = sound.volume;
	var startP : float = sound.pitch;
	
	//define targets:
	var endV : float = args["volume"];
	var endP : float = args["pitch"];
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		switch (easing){
			case "easeInQuad":
				sound.volume =easeInQuad(startV,endV,i);
				sound.pitch =easeInQuad(startP,endP,i);
				break;
			case "easeOutQuad":
				sound.volume =easeOutQuad(startV,endV,i);
				sound.pitch =easeOutQuad(startP,endP,i);
				break;
			case "easeInOutQuad":
				sound.volume =easeInOutQuad(startV,endV,i);
				sound.pitch =easeInOutQuad(startP,endP,i);
				break;
			case "easeInCubic":
				sound.volume =easeInCubic(startV,endV,i);
				sound.pitch =easeInCubic(startP,endP,i);			
				break;
			case "easeOutCubic":
				sound.volume =easeOutCubic(startV,endV,i);
				sound.pitch =easeOutCubic(startP,endP,i);			
				break;
			case "easeInOutCubic":
				sound.volume =easeInOutCubic(startV,endV,i);
				sound.pitch =easeInOutCubic(startP,endP,i);			
				break;
			case "easeInQuart":
				sound.volume =easeInQuart(startV,endV,i);
				sound.pitch =easeInQuart(startP,endP,i);			
				break;
			case "easeOutQuart":
				sound.volume =easeOutQuart(startV,endV,i);
				sound.pitch =easeOutQuart(startP,endP,i);
				break;
			case "easeInOutQuart":
				sound.volume =easeInOutQuart(startV,endV,i);
				sound.pitch =easeInOutQuart(startP,endP,i);
				break;
			case "easeInQuint":
				sound.volume =easeInQuint(startV,endV,i);
				sound.pitch =easeInQuint(startP,endP,i);
				break;
			case "easeOutQuint":
				sound.volume =easeOutQuint(startV,endV,i);
				sound.pitch =easeOutQuint(startP,endP,i);			
				break;
			case "easeInOutQuint":
				sound.volume =easeInOutQuint(startV,endV,i);
				sound.pitch =easeInOutQuint(startP,endP,i);
				break;
			case "easeInSine":
				sound.volume =easeInSine(startV,endV,i);
				sound.pitch =easeInSine(startP,endP,i);
				break;
			case "easeOutSine":
				sound.volume =easeOutSine(startV,endV,i);
				sound.pitch =easeOutSine(startP,endP,i);
				break;
			case "easeInOutSine":
				sound.volume =easeInOutSine(startV,endV,i);
				sound.pitch =easeInOutSine(startP,endP,i);
				break;
			case "easeInExpo":
				sound.volume =easeInExpo(startV,endV,i);
				sound.pitch =easeInExpo(startP,endP,i);
				break;
			case "easeOutExpo":
				sound.volume =easeOutExpo(startV,endV,i);
				sound.pitch =easeOutExpo(startP,endP,i);
				break;
			case "easeInOutExpo":
				sound.volume =easeInOutExpo(startV,endV,i);
				sound.pitch =easeInOutExpo(startP,endP,i);
				break;
			case "easeInCirc":
				sound.volume =easeInCirc(startV,endV,i);
				sound.pitch =easeInCirc(startP,endP,i);
				break;	
			case "easeOutCirc":
				sound.volume =easeOutCirc(startV,endV,i);
				sound.pitch =easeOutCirc(startP,endP,i);
				break;
			case "easeInOutCirc":
				sound.volume =easeInOutCirc(startV,endV,i);
				sound.pitch =easeInOutCirc(startP,endP,i);
				break;
			case "easeInBack":
				sound.volume =easeInBack(startV,endV,i);
				sound.pitch =easeInBack(startP,endP,i);
				break;
			case "easeOutBack":
				sound.volume =easeOutBack(startV,endV,i);
				sound.pitch =easeOutBack(startP,endP,i);
				break;
			case "easeInOutBack":
				sound.volume =easeInOutBack(startV,endV,i);
				sound.pitch =easeInOutBack(startP,endP,i);
				break;				
			case "spring":
				sound.volume =spring(startV,endV,i);
				sound.pitch =spring(startP,endP,i);
				break;
			case "bounce":
				sound.volume =bounce(startV,endV,i);
				sound.pitch =bounce(startP,endP,i);
				break;	
			case "linear":
				sound.volume =linear(startV,endV,i);
				sound.pitch =linear(startP,endP,i);
				break;
		}		
		yield;
	}
	sound.volume = endV;
	sound.pitch = endP;
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
}

//Stab application:
function stab(args:Hashtable){
	if(!gameObject.GetComponent(AudioSource)){
		gameObject.AddComponent ("AudioSource");
		audio.playOnAwake=false;
	}
	
	//construct args:
	if(args["delay"]==null){
		args.Add("delay",stabDefaults["delay"]);
	}
	if(args["volume"]==null){
		args.Add("volume",stabDefaults["volume"]);
	}
	if(args["pitch"]==null){
		args.Add("pitch",stabDefaults["pitch"]);
	}
	if(args["clip"]==null){
		args.Add("clip",gameObject.audio.clip);
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//
	inProgress=true;
	
	//target:
	obj = gameObject.audio;
	obj.clip=args["clip"];
	obj.volume=args["volume"];
	obj.pitch=args["pitch"];
	obj.PlayOneShot(args["clip"]);
	
	if(args["onComplete"]){
		var pitchArg : float = args["pitch"];
		yield WaitForSeconds(obj.clip.length/pitchArg);
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		Destroy (this);
	}else{
		Destroy (this);	
	}
	
}

//Shake application:
function shake(args:Hashtable){
	//construct args:
	if(args["time"]==null){
		args.Add("time",shakeDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",shakeDefaults["delay"]);
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3=obj.position;

	//coordiantes:
	if(args["x"]==null){
		args.Add("x",0);
	}
	if(args["y"]==null){
		args.Add("y",0);
	}
	if(args["z"]==null){
		args.Add("z",0);
	}
	
	//define targets:
	var runTime : float = args["time"];
	var shakeMagnitude : Vector3 = Vector3(args["x"],args["y"],args["z"]);
	var impact : boolean=true;
	
	//run tween:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
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
		shakeMagnitude.x=xValue-(i*xValue);
		shakeMagnitude.y=yValue-(i*yValue);
		shakeMagnitude.z=zValue-(i*zValue);
		yield;
	}
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	obj.position=start;
	
	//DestroyImmediate is needed to avoid shake restarts (odd, but needed):
	DestroyImmediate (this);
}


//Punch rotation application:
private function punchRotation(args:Hashtable) {
	if(guiTexture || guiText){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
		Destroy(this);
        return;		
	}
	
	//construct args:
	if(args["time"]==null){
		args.Add("time",punchRotationDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",punchRotationDefaults["delay"]);
	}
	
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",0);
	}
	if(args["y"]==null){
		args.Add("y",0);
	}
	if(args["z"]==null){
		args.Add("z",0);
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var pos : Vector3 = obj.localEulerAngles;
	var posAug : Vector3;	
	
	//coordiantes:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	if(xValue==null){
		args.Add("x",0);
	}else{
		xValue*=360;
	}
	
	if(yValue==null){
		args.Add("y",0);
	}else{
		yValue*=360;
	}
	
	if(zValue==null){
		args.Add("z",0);
	}else{
		zValue*=360;
	}
	
	//define targets:
	var runTime : float = args["time"];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		if(xValue>0){
			posAug.x = punch(xValue,i) + pos.x;
		}else if(xValue<0){
			posAug.x=-punch(Mathf.Abs(xValue),i) + pos.x;	
		}
		if(yValue>0){
			posAug.y=punch(yValue,i) + pos.y;
		}else if(yValue<0){
			posAug.y=-punch(Mathf.Abs(yValue),i) + pos.y;	
		}
		if(zValue>0){
			posAug.z=punch(zValue,i) + pos.z;
		}else if(zValue<0){
			posAug.z=-punch(Mathf.Abs(zValue),i) + pos.z;	
		}
		obj.localRotation=Quaternion.Euler(posAug.x,posAug.y,posAug.z);
		yield;
	}
	obj.localRotation=Quaternion.Euler(pos.x,pos.y,pos.z);
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
}

//Punch Position application:
private function punchPosition(args:Hashtable) {
	//construct args:
	if(args["time"]==null){
		args.Add("time",punchPositionDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",punchPositionDefaults["delay"]);
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var pos : Vector3 = obj.position;
		
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",0);
	}
	if(args["y"]==null){
		args.Add("y",0);
	}
	if(args["z"]==null){
		args.Add("z",0);
	}
	
	//define targets:
	var runTime : float = args["time"];
	
	//run tween:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		if(xValue>0){
			obj.position.x=punch(xValue,i) + pos.x;
		}else if(xValue<0){
			obj.position.x=-punch(Mathf.Abs(xValue),i) + pos.x;	
		}
		if(yValue>0){
			obj.position.y=punch(yValue,i) + pos.y;
		}else if(yValue<0){
			obj.position.y=-punch(Mathf.Abs(yValue),i) + pos.y;	
		}
		if(zValue>0){
			obj.position.z=punch(zValue,i) + pos.z;
		}else if(zValue<0){
			obj.position.z=-punch(Mathf.Abs(zValue),i) + pos.z;	
		}
		yield;
	}
	obj.position=pos;
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);	
}


//Fade to application:
private function fadeTo(args:Hashtable){
	//construct args:
	if(args["time"]==null){
		args.Add("time",fadeDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",fadeDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",fadeDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	
	//coordiantes:
	if(args["alpha"]==null){
		args.Add("alpha",0);
	}
	
	//define targets:
	var endA  : float = args["alpha"];
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	//run tween:
	if(obj.guiTexture){
		start=obj.guiTexture.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
			switch (easing){
				case "easeInQuad":
					obj.guiTexture.color.a=easeInQuad(start,endA,i);
					break;
				case "easeOutQuad":
					obj.guiTexture.color.a=easeOutQuad(start,endA,i);
					break;
				case "easeInOutQuad":
					obj.guiTexture.color.a=easeInOutQuad(start,endA,i);
					break;
				case "easeInCubic":
					obj.guiTexture.color.a=easeInCubic(start,endA,i);
					break;
				case "easeOutCubic":
					obj.guiTexture.color.a=easeOutCubic(start,endA,i);
					break;
				case "easeInOutCubic":
					obj.guiTexture.color.a=easeInOutCubic(start,endA,i);
					break;
				case "easeInQuart":
					obj.guiTexture.color.a=easeInQuart(start,endA,i);
					break;
				case "easeOutQuart":
					obj.guiTexture.color.a=easeOutQuart(start,endA,i);
					break;
				case "easeInOutQuart":
					obj.guiTexture.color.a=easeOutQuart(start,endA,i);
					break;
				case "easeInQuint":
					obj.guiTexture.color.a=easeInQuint(start,endA,i);
					break;
				case "easeOutQuint":
					obj.guiTexture.color.a=easeOutQuint(start,endA,i);
					break;
				case "easeInOutQuint":
					obj.guiTexture.color.a=easeInOutQuint(start,endA,i);
					break;
				case "easeInSine":
					obj.guiTexture.color.a=easeInSine(start,endA,i);
					break;
				case "easeOutSine":
					obj.guiTexture.color.a=easeOutSine(start,endA,i);
					break;
				case "easeInOutSine":
					obj.guiTexture.color.a=easeInOutSine(start,endA,i);
					break;
				case "easeInExpo":
					obj.guiTexture.color.a=easeInExpo(start,endA,i);
					break;
				case "easeOutExpo":
					obj.guiTexture.color.a=easeOutExpo(start,endA,i);
					break;
				case "easeInOutExpo":
					obj.guiTexture.color.a=easeInOutExpo(start,endA,i);
					break;
				case "easeInCirc":
					obj.guiTexture.color.a=easeInCirc(start,endA,i);
					break;
				case "easeOutCirc":
					obj.guiTexture.color.a=easeOutCirc(start,endA,i);
					break;
				case "easeInOutCirc":
					obj.guiTexture.color.a=easeInOutCirc(start,endA,i);
					break;
				case "linear":
					obj.guiTexture.color.a=linear(start,endA,i);
					break;
				case "spring":
					obj.guiTexture.color.a=spring(start,endA,i);
					break;
				case "bounce":
					obj.guiTexture.color.a=bounce(start,endA,i);
					break;
				case "easeInBack":
					obj.guiTexture.color.a=easeInBack(start,endA,i);
					break;
				case "easeOutBack":
					obj.guiTexture.color.a=easeOutBack(start,endA,i);
					break;
				case "easeInOutBack":
					obj.guiTexture.color.a=easeInOutBack(start,endA,i);
					break;
			}		
			yield;
		}
		guiTexture.color.a= endA;
		if(args["onComplete"]){
			SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		Destroy (this);	
	}else{
		start=obj.renderer.material.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
			switch (easing){
				case "easeInQuad":
					obj.renderer.material.color.a=easeInQuad(start,endA,i);
					break;
				case "easeOutQuad":
					obj.renderer.material.color.a=easeOutQuad(start,endA,i);
					break;
				case "easeInOutQuad":
					obj.renderer.material.color.a=easeInOutQuad(start,endA,i);
					break;
				case "easeInCubic":
					obj.renderer.material.color.a=easeInCubic(start,endA,i);
					break;
				case "easeOutCubic":
					obj.renderer.material.color.a=easeOutCubic(start,endA,i);
					break;
				case "easeInOutCubic":
					obj.renderer.material.color.a=easeInOutCubic(start,endA,i);
					break;
				case "easeInQuart":
					obj.renderer.material.color.a=easeInQuart(start,endA,i);
					break;
				case "easeOutQuart":
					obj.renderer.material.color.a=easeOutQuart(start,endA,i);
					break;
				case "easeInOutQuart":
					obj.renderer.material.color.a=easeInOutQuart(start,endA,i);
					break;
				case "easeInQuint":
					obj.renderer.material.color.a=easeInQuint(start,endA,i);
					break;
				case "easeOutQuint":
					obj.renderer.material.color.a=easeOutQuint(start,endA,i);
					break;
				case "easeInOutQuint":
					obj.renderer.material.color.a=easeInOutQuint(start,endA,i);
					break;
				case "easeInSine":
					obj.renderer.material.color.a=easeInSine(start,endA,i);
					break;
				case "easeOutSine":
					obj.renderer.material.color.a=easeOutSine(start,endA,i);
					break;
				case "easeInOutSine":
					obj.renderer.material.color.a=easeInOutSine(start,endA,i);
					break;
				case "easeInExpo":
					obj.renderer.material.color.a=easeInExpo(start,endA,i);
					break;
				case "easeOutExpo":
					obj.renderer.material.color.a=easeOutExpo(start,endA,i);
					break;
				case "easeInOutExpo":
					obj.renderer.material.color.a=easeInOutExpo(start,endA,i);
					break;
				case "easeInCirc":
					obj.renderer.material.color.a=easeInCirc(start,endA,i);
					break;
				case "easeOutCirc":
					obj.renderer.material.color.a=easeOutCirc(start,endA,i);
					break;
				case "easeInOutCirc":
					obj.renderer.material.color.a=easeInOutCirc(start,endA,i);
					break;
				case "linear":
					obj.renderer.material.color.a=linear(start,endA,i);
					break;
				case "spring":
					obj.renderer.material.color.a=spring(start,endA,i);
					break;
				case "bounce":
					obj.renderer.material.color.a=bounce(start,endA,i);
					break;
				case "easeInBack":
					obj.renderer.material.color.a=easeInBack(start,endA,i);
					break;
				case "easeOutBack":
					obj.renderer.material.color.a=easeOutBack(start,endA,i);
					break;
				case "easeInOutBack":
					obj.renderer.material.color.a=easeInOutBack(start,endA,i);
					break;
				
			}		
			yield;
		}
		obj.renderer.material.color.a= endA;
		if(args["onComplete"]){
			SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		Destroy (this);
	}
}

//Move to application:
private function moveTo(args:Hashtable){	
	//construct args:
	if(args["time"]==null){
		args.Add("time",moveDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",moveDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",moveDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3 = obj.localPosition;
	
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",start.x);
	}
	if(args["y"]==null){
		args.Add("y",start.y);
	}
	if(args["z"]==null){
		args.Add("z",start.z);
	}
	
	//define targets:
	var end : Vector3 = Vector3(args["x"], args["y"], args["z"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		switch(easing){
			case "easeInQuad":
				obj.localPosition.x=easeInQuad(start.x,end.x,i);
				obj.localPosition.y=easeInQuad(start.y,end.y,i);
				obj.localPosition.z=easeInQuad(start.z,end.z,i);
				break;
			case "easeOutQuad":
				obj.localPosition.x=easeOutQuad(start.x,end.x,i);
				obj.localPosition.y=easeOutQuad(start.y,end.y,i);
				obj.localPosition.z=easeOutQuad(start.z,end.z,i);
				break;
			case "easeInOutQuad":
				obj.localPosition.x=easeInOutQuad(start.x,end.x,i);
				obj.localPosition.y=easeInOutQuad(start.y,end.y,i);
				obj.localPosition.z=easeInOutQuad(start.z,end.z,i);
				break;
			case "easeInCubic":
				obj.localPosition.x=easeInCubic(start.x,end.x,i);
				obj.localPosition.y=easeInCubic(start.y,end.y,i);
				obj.localPosition.z=easeInCubic(start.z,end.z,i);
				break;
			case "easeOutCubic":
				obj.localPosition.x=easeOutCubic(start.x,end.x,i);
				obj.localPosition.y=easeOutCubic(start.y,end.y,i);
				obj.localPosition.z=easeOutCubic(start.z,end.z,i);
				break;
			case "easeInOutCubic":
				obj.localPosition.x=easeInOutCubic(start.x,end.x,i);
				obj.localPosition.y=easeInOutCubic(start.y,end.y,i);
				obj.localPosition.z=easeInOutCubic(start.z,end.z,i);
				break;
			case "easeInQuart":
				obj.localPosition.x=easeInQuart(start.x,end.x,i);
				obj.localPosition.y=easeInQuart(start.y,end.y,i);
				obj.localPosition.z=easeInQuart(start.z,end.z,i);
				break;
			case "easeOutQuart":
				obj.localPosition.x=easeOutQuart(start.x,end.x,i);
				obj.localPosition.y=easeOutQuart(start.y,end.y,i);
				obj.localPosition.z=easeOutQuart(start.z,end.z,i);
				break;
			case "easeInOutQuart":
				obj.localPosition.x=easeInOutQuart(start.x,end.x,i);
				obj.localPosition.y=easeInOutQuart(start.y,end.y,i);
				obj.localPosition.z=easeInOutQuart(start.z,end.z,i);
				break;
			case "easeInQuint":
				obj.localPosition.x=easeInQuint(start.x,end.x,i);
				obj.localPosition.y=easeInQuint(start.y,end.y,i);
				obj.localPosition.z=easeInQuint(start.z,end.z,i);
				break;
			case "easeOutQuint":
				obj.position.x=easeOutQuint(start.x,end.x,i);
				obj.position.y=easeOutQuint(start.y,end.y,i);
				obj.position.z=easeOutQuint(start.z,end.z,i);
				break;
			case "easeInOutQuint":
				obj.localPosition.x=easeInOutQuint(start.x,end.x,i);
				obj.localPosition.y=easeInOutQuint(start.y,end.y,i);
				obj.localPosition.z=easeInOutQuint(start.z,end.z,i);
				break;
			case "easeInSine":
				obj.localPosition.x=easeInSine(start.x,end.x,i);
				obj.localPosition.y=easeInSine(start.y,end.y,i);
				obj.localPosition.z=easeInSine(start.z,end.z,i);
				break;
			case "easeOutSine":
				obj.localPosition.x=easeOutSine(start.x,end.x,i);
				obj.localPosition.y=easeOutSine(start.y,end.y,i);
				obj.localPosition.z=easeOutSine(start.z,end.z,i);
				break;
			case "easeInOutSine":
				obj.localPosition.x=easeInOutSine(start.x,end.x,i);
				obj.localPosition.y=easeInOutSine(start.y,end.y,i);
				obj.localPosition.z=easeInOutSine(start.z,end.z,i);
				break;
			case "easeInExpo":
				obj.localPosition.x=easeInExpo(start.x,end.x,i);
				obj.localPosition.y=easeInExpo(start.y,end.y,i);
				obj.localPosition.z=easeInExpo(start.z,end.z,i);
				break;
			case "easeOutExpo":
				obj.localPosition.x=easeOutExpo(start.x,end.x,i);
				obj.localPosition.y=easeOutExpo(start.y,end.y,i);
				obj.localPosition.z=easeOutExpo(start.z,end.z,i);
				break;
			case "easeInOutExpo":
				obj.localPosition.x=easeInOutExpo(start.x,end.x,i);
				obj.localPosition.y=easeInOutExpo(start.y,end.y,i);
				obj.localPosition.z=easeInOutExpo(start.z,end.z,i);
				break;
			case "easeInCirc":
				obj.localPosition.x=easeInCirc(start.x,end.x,i);
				obj.localPosition.y=easeInCirc(start.y,end.y,i);
				obj.localPosition.z=easeInCirc(start.z,end.z,i);
				break;
			case "easeOutCirc":
				obj.localPosition.x=easeOutCirc(start.x,end.x,i);
				obj.localPosition.y=easeOutCirc(start.y,end.y,i);
				obj.localPosition.z=easeOutCirc(start.z,end.z,i);
				break;
			case "easeInOutCirc":
				obj.localPosition.x=easeInOutCirc(start.x,end.x,i);
				obj.localPosition.y=easeInOutCirc(start.y,end.y,i);
				obj.localPosition.z=easeInOutCirc(start.z,end.z,i);
				break;
			case "linear":
				obj.localPosition.x=linear(start.x,end.x,i);
				obj.localPosition.y=linear(start.y,end.y,i);
				obj.localPosition.z=linear(start.z,end.z,i);
				break;
			case "spring":
				obj.localPosition.x=spring(start.x,end.x,i);
				obj.localPosition.y=spring(start.y,end.y,i);
				obj.localPosition.z=spring(start.z,end.z,i);
				break;
			case "bounce":
				obj.localPosition.x=bounce(start.x,end.x,i);
				obj.localPosition.y=bounce(start.y,end.y,i);
				obj.localPosition.z=bounce(start.z,end.z,i);
				break;
			case "easeInBack":
				obj.localPosition.x=easeInBack(start.x,end.x,i);
				obj.localPosition.y=easeInBack(start.y,end.y,i);
				obj.localPosition.z=easeInBack(start.z,end.z,i);
				break;
			case "easeOutBack":
				obj.localPosition.x=easeOutBack(start.x,end.x,i);
				obj.localPosition.y=easeOutBack(start.y,end.y,i);
				obj.localPosition.z=easeOutBack(start.z,end.z,i);
				break;
			case "easeInOutBack":
				obj.localPosition.x=easeInOutBack(start.x,end.x,i);
				obj.localPosition.y=easeInOutBack(start.y,end.y,i);
				obj.localPosition.z=easeInOutBack(start.z,end.z,i);
				break;			
		}
		yield;
	}
	obj.localPosition=end;
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
}

//Scale to application:
private function scaleTo(args:Hashtable){	
	//construct args:
	if(args["time"]==null){
		args.Add("time",scaleDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",scaleDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",scaleDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3 = obj.localScale;
	
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",start.x);
	}
	if(args["y"]==null){
		args.Add("y",start.y);
	}
	if(args["z"]==null){
		args.Add("z",start.z);
	}
	
	//define targets:
	var end : Vector3 = Vector3(args["x"], args["y"], args["z"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		switch(easing){
			case "easeInQuad":
				obj.localScale.x=easeInQuad(start.x,end.x,i);
				obj.localScale.y=easeInQuad(start.y,end.y,i);
				obj.localScale.z=easeInQuad(start.z,end.z,i);
				break;
			case "easeOutQuad":
				obj.localScale.x=easeOutQuad(start.x,end.x,i);
				obj.localScale.y=easeOutQuad(start.y,end.y,i);
				obj.localScale.z=easeOutQuad(start.z,end.z,i);
				break;
			case "easeInOutQuad":
				obj.localScale.x=easeInOutQuad(start.x,end.x,i);
				obj.localScale.y=easeInOutQuad(start.y,end.y,i);
				obj.localScale.z=easeInOutQuad(start.z,end.z,i);
				break;
			case "easeInCubic":
				obj.localScale.x=easeInCubic(start.x,end.x,i);
				obj.localScale.y=easeInCubic(start.y,end.y,i);
				obj.localScale.z=easeInCubic(start.z,end.z,i);
				break;
			case "easeOutCubic":
				obj.localScale.x=easeOutCubic(start.x,end.x,i);
				obj.localScale.y=easeOutCubic(start.y,end.y,i);
				obj.localScale.z=easeOutCubic(start.z,end.z,i);
				break;
			case "easeInOutCubic":
				obj.localScale.x=easeInOutCubic(start.x,end.x,i);
				obj.localScale.y=easeInOutCubic(start.y,end.y,i);
				obj.localScale.z=easeInOutCubic(start.z,end.z,i);
				break;
			case "easeInQuart":
				obj.localScale.x=easeInQuart(start.x,end.x,i);
				obj.localScale.y=easeInQuart(start.y,end.y,i);
				obj.localScale.z=easeInQuart(start.z,end.z,i);
				break;
			case "easeOutQuart":
				obj.localScale.x=easeOutQuart(start.x,end.x,i);
				obj.localScale.y=easeOutQuart(start.y,end.y,i);
				obj.localScale.z=easeOutQuart(start.z,end.z,i);
				break;
			case "easeInOutQuart":
				obj.localScale.x=easeInOutQuart(start.x,end.x,i);
				obj.localScale.y=easeInOutQuart(start.y,end.y,i);
				obj.localScale.z=easeInOutQuart(start.z,end.z,i);
				break;
			case "easeInQuint":
				obj.localScale.x=easeInQuint(start.x,end.x,i);
				obj.localScale.y=easeInQuint(start.y,end.y,i);
				obj.localScale.z=easeInQuint(start.z,end.z,i);
				break;
			case "easeOutQuint":
				obj.localScale.x=easeOutQuint(start.x,end.x,i);
				obj.localScale.y=easeOutQuint(start.y,end.y,i);
				obj.localScale.z=easeOutQuint(start.z,end.z,i);
				break;
			case "easeInOutQuint":
				obj.localScale.x=easeInOutQuint(start.x,end.x,i);
				obj.localScale.y=easeInOutQuint(start.y,end.y,i);
				obj.localScale.z=easeInOutQuint(start.z,end.z,i);
				break;
			case "easeInSine":
				obj.localScale.x=easeInSine(start.x,end.x,i);
				obj.localScale.y=easeInSine(start.y,end.y,i);
				obj.localScale.z=easeInSine(start.z,end.z,i);
				break;
			case "easeOutSine":
				obj.localScale.x=easeOutSine(start.x,end.x,i);
				obj.localScale.y=easeOutSine(start.y,end.y,i);
				obj.localScale.z=easeOutSine(start.z,end.z,i);
				break;
			case "easeInOutSine":
				obj.localScale.x=easeInOutSine(start.x,end.x,i);
				obj.localScale.y=easeInOutSine(start.y,end.y,i);
				obj.localScale.z=easeInOutSine(start.z,end.z,i);
				break;
			case "easeInExpo":
				obj.localScale.x=easeInExpo(start.x,end.x,i);
				obj.localScale.y=easeInExpo(start.y,end.y,i);
				obj.localScale.z=easeInExpo(start.z,end.z,i);
				break;
			case "easeOutExpo":
				obj.localScale.x=easeOutExpo(start.x,end.x,i);
				obj.localScale.y=easeOutExpo(start.y,end.y,i);
				obj.localScale.z=easeOutExpo(start.z,end.z,i);
				break;
			case "easeInOutExpo":
				obj.localScale.x=easeInOutExpo(start.x,end.x,i);
				obj.localScale.y=easeInOutExpo(start.y,end.y,i);
				obj.localScale.z=easeInOutExpo(start.z,end.z,i);
				break;
			case "easeInCirc":
				obj.localScale.x=easeInCirc(start.x,end.x,i);
				obj.localScale.y=easeInCirc(start.y,end.y,i);
				obj.localScale.z=easeInCirc(start.z,end.z,i);
				break;
			case "easeOutCirc":
				obj.localScale.x=easeOutCirc(start.x,end.x,i);
				obj.localScale.y=easeOutCirc(start.y,end.y,i);
				obj.localScale.z=easeOutCirc(start.z,end.z,i);
				break;
			case "easeInOutCirc":
				obj.localScale.x=easeInOutCirc(start.x,end.x,i);
				obj.localScale.y=easeInOutCirc(start.y,end.y,i);
				obj.localScale.z=easeInOutCirc(start.z,end.z,i);
				break;
			case "linear":
				obj.localScale.x=linear(start.x,end.x,i);
				obj.localScale.y=linear(start.y,end.y,i);
				obj.localScale.z=linear(start.z,end.z,i);
				break;
			case "spring":
				obj.localScale.x=spring(start.x,end.x,i);
				obj.localScale.y=spring(start.y,end.y,i);
				obj.localScale.z=spring(start.z,end.z,i);
				break;
			case "bounce":
				obj.localScale.x=bounce(start.x,end.x,i);
				obj.localScale.y=bounce(start.y,end.y,i);
				obj.localScale.z=bounce(start.z,end.z,i);
				break;
			case "easeInBack":
				obj.localScale.x=easeInBack(start.x,end.x,i);
				obj.localScale.y=easeInBack(start.y,end.y,i);
				obj.localScale.z=easeInBack(start.z,end.z,i);
				break;
			case "easeOutBack":
				obj.localScale.x=easeOutBack(start.x,end.x,i);
				obj.localScale.y=easeOutBack(start.y,end.y,i);
				obj.localScale.z=easeOutBack(start.z,end.z,i);
				break;
			case "easeInOutBack":
				obj.localScale.x=easeInOutBack(start.x,end.x,i);
				obj.localScale.y=easeInOutBack(start.y,end.y,i);
				obj.localScale.z=easeInOutBack(start.z,end.z,i);
				break;	
		}
		yield;
	}
	obj.localScale=end;	
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);	
}

//Rotate to application:
private function rotateTo(args:Hashtable) {
	if(guiTexture || guiText){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
		Destroy(this);
        return;		
	}
	
	//construct args:
	if(args["time"]==null){
		args.Add("time",rotateToDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",rotateToDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",rotateToDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}	
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3=Vector3(obj.localEulerAngles.x,obj.localEulerAngles.y,obj.localEulerAngles.z);
	
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",start.x);
	}
	if(args["y"]==null){
		args.Add("y",start.y);
	}
	if(args["z"]==null){
		args.Add("z",start.z);
	}
	
	//define targets:
	var end = Vector3(clerp(start.x,args["x"],1), clerp(start.y,args["y"],1), clerp(start.z,args["z"],1));
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		switch (easing){
			case "easeInQuad":
				obj.localRotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));
				break;
			case "easeOutQuad":
				obj.localRotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));
				break;
			case "easeInOutQuad":
				obj.localRotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));
				break;
			case "easeInCubic":
				obj.localRotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));
				break;
			case "easeOutCubic":
				obj.localRotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));
				break;
			case "easeInOutCubic":
				obj.localRotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));
				break;
			case "easeInQuart":
				obj.localRotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));
				break;
			case "easeOutQuart":
				obj.localRotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));
				break;
			case "easeInOutQuart":
				obj.localRotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));
				break;
			case "easeInQuint":
				obj.localRotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));
				break;
			case "easeOutQuint":
				obj.localRotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));
				break;
			case "easeInOutQuint":
				obj.localRotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));
				break;
			case "easeInSine":
				obj.localRotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));
				break;
			case "easeOutSine":
				obj.localRotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));
				break;
			case "easeInOutSine":
				obj.localRotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));
				break;
			case "easeInExpo":
				obj.localRotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));
				break;
			case "easeOutExpo":
				obj.localRotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));
				break;
			case "easeInOutExpo":
				obj.localRotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));
				break;
			case "easeInCirc":
				obj.localRotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));
				break;
			case "easeOutCirc":
				obj.localRotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));
				break;
			case "easeInOutCirc":
				obj.localRotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));
				break;
			case "linear":
				obj.localRotation=Quaternion.Euler(clerp(start.x,end.x,i),clerp(start.y,end.y,i),clerp(start.z,end.z,i));
				break;
			case "spring":
				obj.localRotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));
				break;
			case "bounce":
				obj.localRotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));
				break;
			case "easeInBack":
				obj.localRotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));
				break;
			case "easeOutBack":
				obj.localRotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));
				break;
			case "easeInOutBack":
				obj.localRotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));
				break;
		}
		yield;
	}
	obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
}

//Rotate by application:
private function rotateBy(args:Hashtable) {
	if(guiTexture || guiText){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
		Destroy(this);
        return;		
	}
	
	//construct args:
	if(args["time"]==null){
		args.Add("time",rotateByDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",rotateByDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",rotateByDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}	
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3=Vector3(obj.localEulerAngles.x,obj.localEulerAngles.y,obj.localEulerAngles.z);
	
	//coordiantes:
	if(args["x"]==null){
		args.Add("x",start.x);
	}
	if(args["y"]==null){
		args.Add("y",start.y);
	}
	if(args["z"]==null){
		args.Add("z",start.z);
	}
	
	//define targets:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	var	end = Vector3(360*xValue + obj.localEulerAngles.x, 360*yValue + obj.localEulerAngles.y, 360 *zValue + obj.localEulerAngles.x);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
		
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		switch (easing){
			case "easeInQuad":
				obj.localRotation=Quaternion.Euler(easeInQuad(start.x,end.x,i),easeInQuad(start.y,end.y,i),easeInQuad(start.z,end.z,i));
				break;
			case "easeOutQuad":
				obj.localRotation=Quaternion.Euler(easeOutQuad(start.x,end.x,i),easeOutQuad(start.y,end.y,i),easeOutQuad(start.z,end.z,i));
				break;
			case "easeInOutQuad":
				obj.localRotation=Quaternion.Euler(easeInOutQuad(start.x,end.x,i),easeInOutQuad(start.y,end.y,i),easeInOutQuad(start.z,end.z,i));
				break;
			case "easeInCubic":
				obj.localRotation=Quaternion.Euler(easeInCubic(start.x,end.x,i),easeInCubic(start.y,end.y,i),easeInCubic(start.z,end.z,i));
				break;
			case "easeOutCubic":
				obj.localRotation=Quaternion.Euler(easeOutCubic(start.x,end.x,i),easeOutCubic(start.y,end.y,i),easeOutCubic(start.z,end.z,i));
				break;
			case "easeInOutCubic":
				obj.localRotation=Quaternion.Euler(easeInOutCubic(start.x,end.x,i),easeInOutCubic(start.y,end.y,i),easeInOutCubic(start.z,end.z,i));
				break;
			case "easeInQuart":
				obj.localRotation=Quaternion.Euler(easeInQuart(start.x,end.x,i),easeInQuart(start.y,end.y,i),easeInQuart(start.z,end.z,i));
				break;
			case "easeOutQuart":
				obj.localRotation=Quaternion.Euler(easeOutQuart(start.x,end.x,i),easeOutQuart(start.y,end.y,i),easeOutQuart(start.z,end.z,i));
				break;
			case "easeInOutQuart":
				obj.localRotation=Quaternion.Euler(easeInOutQuart(start.x,end.x,i),easeInOutQuart(start.y,end.y,i),easeInOutQuart(start.z,end.z,i));
				break;
			case "easeInQuint":
				obj.localRotation=Quaternion.Euler(easeInQuint(start.x,end.x,i),easeInQuint(start.y,end.y,i),easeInQuint(start.z,end.z,i));
				break;
			case "easeOutQuint":
				obj.localRotation=Quaternion.Euler(easeOutQuint(start.x,end.x,i),easeOutQuint(start.y,end.y,i),easeOutQuint(start.z,end.z,i));
				break;
			case "easeInOutQuint":
				obj.localRotation=Quaternion.Euler(easeInOutQuint(start.x,end.x,i),easeInOutQuint(start.y,end.y,i),easeInOutQuint(start.z,end.z,i));
				break;
			case "easeInSine":
				obj.localRotation=Quaternion.Euler(easeInSine(start.x,end.x,i),easeInSine(start.y,end.y,i),easeInSine(start.z,end.z,i));
				break;
			case "easeOutSine":
				obj.localRotation=Quaternion.Euler(easeOutSine(start.x,end.x,i),easeOutSine(start.y,end.y,i),easeOutSine(start.z,end.z,i));
				break;
			case "easeInOutSine":
				obj.localRotation=Quaternion.Euler(easeInOutSine(start.x,end.x,i),easeInOutSine(start.y,end.y,i),easeInOutSine(start.z,end.z,i));
				break;
			case "easeInExpo":
				obj.localRotation=Quaternion.Euler(easeInExpo(start.x,end.x,i),easeInExpo(start.y,end.y,i),easeInExpo(start.z,end.z,i));
				break;
			case "easeOutExpo":
				obj.localRotation=Quaternion.Euler(easeOutExpo(start.x,end.x,i),easeOutExpo(start.y,end.y,i),easeOutExpo(start.z,end.z,i));
				break;
			case "easeInOutExpo":
				obj.localRotation=Quaternion.Euler(easeInOutExpo(start.x,end.x,i),easeInOutExpo(start.y,end.y,i),easeInOutExpo(start.z,end.z,i));
				break;
			case "easeInCirc":
				obj.localRotation=Quaternion.Euler(easeInCirc(start.x,end.x,i),easeInCirc(start.y,end.y,i),easeInCirc(start.z,end.z,i));
				break;
			case "easeOutCirc":
				obj.localRotation=Quaternion.Euler(easeOutCirc(start.x,end.x,i),easeOutCirc(start.y,end.y,i),easeOutCirc(start.z,end.z,i));
				break;
			case "easeInOutCirc":
				obj.localRotation=Quaternion.Euler(easeInOutCirc(start.x,end.x,i),easeInOutCirc(start.y,end.y,i),easeInOutCirc(start.z,end.z,i));
				break;
			case "linear":
				obj.localRotation=Quaternion.Euler(clerp(start.x,end.x,i),clerp(start.y,end.y,i),clerp(start.z,end.z,i));
				break;
			case "spring":
				obj.localRotation=Quaternion.Euler(spring(start.x,end.x,i),spring(start.y,end.y,i),spring(start.z,end.z,i));
				break;
			case "bounce":
				obj.localRotation=Quaternion.Euler(bounce(start.x,end.x,i),bounce(start.y,end.y,i),bounce(start.z,end.z,i));
				break;
			case "easeInBack":
				obj.localRotation=Quaternion.Euler(easeInBack(start.x,end.x,i),easeInBack(start.y,end.y,i),easeInBack(start.z,end.z,i));
				break;
			case "easeOutBack":
				obj.localRotation=Quaternion.Euler(easeOutBack(start.x,end.x,i),easeOutBack(start.y,end.y,i),easeOutBack(start.z,end.z,i));
				break;
			case "easeInOutBack":
				obj.localRotation=Quaternion.Euler(easeInOutBack(start.x,end.x,i),easeInOutBack(start.y,end.y,i),easeInOutBack(start.z,end.z,i));
				break;
		}
		yield;
	}
	obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
	if(args["onComplete"]){
		SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
}

//Color to application:
private function colorTo(args:Hashtable) {
	//construct args:
	if(args["time"]==null){
		args.Add("time",colorDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",colorDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",colorDefaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	
	//delay:
	var delay : float = args["delay"];
	if(delay > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts(tweenType);
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	
	//coordiantes:
	if(args["r"]==null){
		args.Add("r",0);
	}
	if(args["g"]==null){
		args.Add("g",0);
	}
	if(args["b"]==null){
		args.Add("b",0);
	}
	
	//define targets:
	end = Color (args["r"],args["g"],args["b"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	if(obj.guiTexture){
		start=obj.guiTexture.color;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
			switch (easing){
				case "easeInQuad":
					obj.guiTexture.color.r=easeInQuad(start.r,end.r,i);
					obj.guiTexture.color.g=easeInQuad(start.g,end.g,i);
					obj.guiTexture.color.b=easeInQuad(start.b,end.b,i);
					break;
				case "easeOutQuad":
					obj.guiTexture.color.r=easeOutQuad(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutQuad(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutQuad(start.b,end.b,i);
					break;
				case "easeInOutQuad":
					obj.guiTexture.color.r=easeInOutQuad(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutQuad(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutQuad(start.b,end.b,i);
					break;
				case "easeInCubic":
					obj.guiTexture.color.r=easeInCubic(start.r,end.r,i);
					obj.guiTexture.color.g=easeInCubic(start.g,end.g,i);
					obj.guiTexture.color.b=easeInCubic(start.b,end.b,i);
					break;
				case "easeOutCubic":
					obj.guiTexture.color.r=easeOutCubic(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutCubic(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutCubic(start.b,end.b,i);
					break;
				case "easeInOutCubic":
					obj.guiTexture.color.r=easeInOutCubic(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutCubic(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutCubic(start.b,end.b,i);
					break;
				case "easeInQuart":
					obj.guiTexture.color.r=easeInQuart(start.r,end.r,i);
					obj.guiTexture.color.g=easeInQuart(start.g,end.g,i);
					obj.guiTexture.color.b=easeInQuart(start.b,end.b,i);
					break;
				case "easeOutQuart":
					obj.guiTexture.color.r=easeOutQuart(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutQuart(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutQuart(start.b,end.b,i);
					break;
				case "easeInOutQuart":
					obj.guiTexture.color.r=easeInOutQuart(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutQuart(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutQuart(start.b,end.b,i);
					break;
				case "easeInQuint":
					obj.guiTexture.color.r=easeInQuint(start.r,end.r,i);
					obj.guiTexture.color.g=easeInQuint(start.g,end.g,i);
					obj.guiTexture.color.b=easeInQuint(start.b,end.b,i);
					break;
				case "easeOutQuint":
					obj.guiTexture.color.r=easeOutQuint(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutQuint(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutQuint(start.b,end.b,i);
					break;
				case "easeInOutQuint":
					obj.guiTexture.color.r=easeInOutQuint(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutQuint(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutQuint(start.b,end.b,i);
					break;
				case "easeInSine":
					obj.guiTexture.color.r=easeInSine(start.r,end.r,i);
					obj.guiTexture.color.g=easeInSine(start.g,end.g,i);
					obj.guiTexture.color.b=easeInSine(start.b,end.b,i);
					break;
				case "easeOutSine":
					obj.guiTexture.color.r=easeOutSine(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutSine(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutSine(start.b,end.b,i);
					break;
				case "easeInOutSine":
					obj.guiTexture.color.r=easeInOutSine(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutSine(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutSine(start.b,end.b,i);
					break;
				case "easeInExpo":
					obj.guiTexture.color.r=easeInExpo(start.r,end.r,i);
					obj.guiTexture.color.g=easeInExpo(start.g,end.g,i);
					obj.guiTexture.color.b=easeInExpo(start.b,end.b,i);
					break;
				case "easeOutExpo":
					obj.guiTexture.color.r=easeOutExpo(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutExpo(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutExpo(start.b,end.b,i);
					break;
				case "easeInOutExpo":
					obj.guiTexture.color.r=easeInOutExpo(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutExpo(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutExpo(start.b,end.b,i);
					break;
				case "easeInCirc":
					obj.guiTexture.color.r=easeInCirc(start.r,end.r,i);
					obj.guiTexture.color.g=easeInCirc(start.g,end.g,i);
					obj.guiTexture.color.b=easeInCirc(start.b,end.b,i);
					break;
				case "easeOutCirc":
					obj.guiTexture.color.r=easeOutCirc(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutCirc(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutCirc(start.b,end.b,i);
					break;
				case "easeInOutCirc":
					obj.guiTexture.color.r=easeInOutCirc(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutCirc(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutCirc(start.b,end.b,i);
					break;
				case "linear":
					obj.guiTexture.color.r=linear(start.r,end.r,i);
					obj.guiTexture.color.g=linear(start.g,end.g,i);
					obj.guiTexture.color.b=linear(start.b,end.b,i);
					break;
				case "spring":
					obj.guiTexture.color.r=spring(start.r,end.r,i);
					obj.guiTexture.color.g=spring(start.g,end.g,i);
					obj.guiTexture.color.b=spring(start.b,end.b,i);
					break;
				case "bounce":
					obj.guiTexture.color.r=bounce(start.r,end.r,i);
					obj.guiTexture.color.g=bounce(start.g,end.g,i);
					obj.guiTexture.color.b=bounce(start.b,end.b,i);
					break;
				case "easeInBack":
					obj.guiTexture.color.r=easeInBack(start.r,end.r,i);
					obj.guiTexture.color.g=easeInBack(start.g,end.g,i);
					obj.guiTexture.color.b=easeInBack(start.b,end.b,i);
					break;
				case "easeOutBack":
					obj.guiTexture.color.r=easeOutBack(start.r,end.r,i);
					obj.guiTexture.color.g=easeOutBack(start.g,end.g,i);
					obj.guiTexture.color.b=easeOutBack(start.b,end.b,i);
					break;
				case "easeInOutBack":
					obj.guiTexture.color.r=easeInOutBack(start.r,end.r,i);
					obj.guiTexture.color.g=easeInOutBack(start.g,end.g,i);
					obj.guiTexture.color.b=easeInOutBack(start.b,end.b,i);
					break;
			}		
			yield;
		}
		obj.guiTexture.color.r=end.r;
		obj.guiTexture.color.g=end.g;
		obj.guiTexture.color.b=end.b;
		if(args["onComplete"]){
			SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		Destroy (this);	
	}else{
		start=obj.renderer.material.color;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
			switch(easing){
								case "easeInQuad":
					obj.renderer.material.color.r=easeInQuad(start.r,end.r,i);
					obj.renderer.material.color.g=easeInQuad(start.g,end.g,i);
					obj.renderer.material.color.b=easeInQuad(start.b,end.b,i);
					break;
				case "easeOutQuad":
					obj.renderer.material.color.r=easeOutQuad(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutQuad(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutQuad(start.b,end.b,i);
					break;
				case "easeInOutQuad":
					obj.renderer.material.color.r=easeInOutQuad(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutQuad(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutQuad(start.b,end.b,i);
					break;
				case "easeInCubic":
					obj.renderer.material.color.r=easeInCubic(start.r,end.r,i);
					obj.renderer.material.color.g=easeInCubic(start.g,end.g,i);
					obj.renderer.material.color.b=easeInCubic(start.b,end.b,i);
					break;
				case "easeOutCubic":
					obj.renderer.material.color.r=easeOutCubic(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutCubic(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutCubic(start.b,end.b,i);
					break;
				case "easeInOutCubic":
					obj.renderer.material.color.r=easeInOutCubic(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutCubic(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutCubic(start.b,end.b,i);
					break;
				case "easeInQuart":
					obj.renderer.material.color.r=easeInQuart(start.r,end.r,i);
					obj.renderer.material.color.g=easeInQuart(start.g,end.g,i);
					obj.renderer.material.color.b=easeInQuart(start.b,end.b,i);
					break;
				case "easeOutQuart":
					obj.renderer.material.color.r=easeOutQuart(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutQuart(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutQuart(start.b,end.b,i);
					break;
				case "easeInOutQuart":
					obj.renderer.material.color.r=easeInOutQuart(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutQuart(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutQuart(start.b,end.b,i);
					break;
				case "easeInQuint":
					obj.renderer.material.color.r=easeInQuint(start.r,end.r,i);
					obj.renderer.material.color.g=easeInQuint(start.g,end.g,i);
					obj.renderer.material.color.b=easeInQuint(start.b,end.b,i);
					break;
				case "easeOutQuint":
					obj.renderer.material.color.r=easeOutQuint(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutQuint(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutQuint(start.b,end.b,i);
					break;
				case "easeInOutQuint":
					obj.renderer.material.color.r=easeInOutQuint(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutQuint(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutQuint(start.b,end.b,i);
					break;
				case "easeInSine":
					obj.renderer.material.color.r=easeInSine(start.r,end.r,i);
					obj.renderer.material.color.g=easeInSine(start.g,end.g,i);
					obj.renderer.material.color.b=easeInSine(start.b,end.b,i);
					break;
				case "easeOutSine":
					obj.renderer.material.color.r=easeOutSine(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutSine(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutSine(start.b,end.b,i);
					break;
				case "easeInOutSine":
					obj.renderer.material.color.r=easeInOutSine(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutSine(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutSine(start.b,end.b,i);
					break;
				case "easeInExpo":
					obj.renderer.material.color.r=easeInExpo(start.r,end.r,i);
					obj.renderer.material.color.g=easeInExpo(start.g,end.g,i);
					obj.renderer.material.color.b=easeInExpo(start.b,end.b,i);
					break;
				case "easeOutExpo":
					obj.renderer.material.color.r=easeOutExpo(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutExpo(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutExpo(start.b,end.b,i);
					break;
				case "easeInOutExpo":
					obj.renderer.material.color.r=easeInOutExpo(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutExpo(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutExpo(start.b,end.b,i);
					break;
				case "easeInCirc":
					obj.renderer.material.color.r=easeInCirc(start.r,end.r,i);
					obj.renderer.material.color.g=easeInCirc(start.g,end.g,i);
					obj.renderer.material.color.b=easeInCirc(start.b,end.b,i);
					break;
				case "easeOutCirc":
					obj.renderer.material.color.r=easeOutCirc(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutCirc(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutCirc(start.b,end.b,i);
					break;
				case "easeInOutCirc":
					obj.renderer.material.color.r=easeInOutCirc(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutCirc(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutCirc(start.b,end.b,i);
					break;
				case "linear":
					obj.renderer.material.color.r=linear(start.r,end.r,i);
					obj.renderer.material.color.g=linear(start.g,end.g,i);
					obj.renderer.material.color.b=linear(start.b,end.b,i);
					break;
				case "spring":
					obj.renderer.material.color.r=spring(start.r,end.r,i);
					obj.renderer.material.color.g=spring(start.g,end.g,i);
					obj.renderer.material.color.b=spring(start.b,end.b,i);
					break;
				case "bounce":
					obj.renderer.material.color.r=bounce(start.r,end.r,i);
					obj.renderer.material.color.g=bounce(start.g,end.g,i);
					obj.renderer.material.color.b=bounce(start.b,end.b,i);
					break;
				case "easeInBack":
					obj.renderer.material.color.r=easeInBack(start.r,end.r,i);
					obj.renderer.material.color.g=easeInBack(start.g,end.g,i);
					obj.renderer.material.color.b=easeInBack(start.b,end.b,i);
					break;
				case "easeOutBack":
					obj.renderer.material.color.r=easeOutBack(start.r,end.r,i);
					obj.renderer.material.color.g=easeOutBack(start.g,end.g,i);
					obj.renderer.material.color.b=easeOutBack(start.b,end.b,i);
					break;
				case "easeInOutBack":
					obj.renderer.material.color.r=easeInOutBack(start.r,end.r,i);
					obj.renderer.material.color.g=easeInOutBack(start.g,end.g,i);
					obj.renderer.material.color.b=easeInOutBack(start.b,end.b,i);
					break;
			}
			/*
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
			*/		
			yield;
		}
		obj.renderer.material.color.r=end.r;
		obj.renderer.material.color.g=end.g;
		obj.renderer.material.color.b=end.b;
		if(args["onComplete"]){
			SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);	
		}
		Destroy (this);		
	}
}

//Easing curves - credits: Robert Penner and a few other that I can't recall right now with a few additions and tweaks:
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