//VERSION: 1.0.18

/*
Copyright (c)2010 Bob Berkebile(http://www.pixelplacement.com), C# port by Patrick Corkum(http://www.insquare.com)

Permission is hereby granted, free of charge, to any person  obtaining a copy of this software and associated documentation  files (the "Software"), to deal in the Software without  restriction, including without limitation the rights to use,  copy, modify, merge, publish, distribute, sublicense, and/or sell  copies of the Software, and to permit persons to whom the  Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/* 
 
/*
TERMS OF USE - EASING EQUATIONS

Open source under the BSD License.

Copyright (c)2001 Robert Penner
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
public static var moveBezierDefaults : Hashtable = {"time":1,"delay":0,"transition":"easeInOutCubic"};
public static var punchPositionDefaults : Hashtable = {"time":1,"delay":0};
public static var punchRotationDefaults : Hashtable = {"time":1,"delay":0};
public static var colorDefaults : Hashtable = {"time":1,"delay":0,"transition":"linear"};

//Transition curve organization - David Bardos
private var TRANSITIONS : Hashtable = {"easeInQuad":easeInQuad, "easeOutQuad":easeOutQuad,"easeInOutQuad":easeInOutQuad, "easeInCubic":easeInCubic, "easeOutCubic":easeOutCubic, "easeInOutCubic":easeInOutCubic, "easeInQuart":easeInQuart, "easeOutQuart":easeOutQuart, "easeInOutQuart":easeInOutQuart, "easeInQuint":easeInQuint, "easeOutQuint":easeOutQuint, "easeInOutQuint":easeInOutQuint, "easeInSine":easeInSine, "easeOutSine":easeOutSine, "easeInOutSine":easeInOutSine, "easeInExpo":easeInExpo, "easeOutExpo":easeOutExpo, "easeInOutExpo":easeInOutExpo, "easeInCirc":easeInCirc, "easeOutCirc":easeOutCirc, "easeInOutCirc":easeInOutCirc, "linear":linear, "spring":spring, "bounce":bounce, "easeInBack":easeInBack, "easeOutBack":easeOutBack, "easeInOutBack":easeInOutBack}; 

//Check for and remove running tweens of same type:
private function checkForConflicts(type:String):void{
	var scripts = GetComponents (iTween);
	for (var script : iTween in scripts) {
		if(script.inProgress && script.tweenType==type){
			Destroy(script);
			script.StopCoroutine(script.tweenType);
		}
	}
}

//Stops (and removes) tweening on an object:
static function stop(obj: GameObject){
	var scripts = obj.GetComponents (iTween);
	for (var script : iTween in scripts) {
		Destroy(script);
		script.StopCoroutine(script.tweenType);
	}
}

//Stops (and removes) tweening of a certain type on an object derived from the root of the type (i.e. "moveTo" = "move"):
static function stopType(obj: GameObject, stopType: String){
	var scripts = obj.GetComponents (iTween);
	for (var script : iTween in scripts) {
		var currentType : String = script.tweenType;
		if(currentType.Substring(0,stopType.length)==stopType){
			Destroy(script);
			script.StopCoroutine(script.tweenType);
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

//Helper class for bezierCurves - David Bardos
class BezierPointInfo
{
	public var starting : Vector3;
	public var intermediate : Vector3;
	public var end: Vector3;
}

//Fade to static register:
static function fadeTo(target: GameObject,args: Hashtable):void{
	if(args["type"]==null){
		args.Add("type","fadeTo");	
	}
	init(target,args);
}

//Fade from static register:
static function fadeFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
	if(args["alpha"]==null){
		args["alpha"] = 0;	
	}
	
	//Handle children:
	if(args["includeChildren"] == null){
		args.Add("includeChildren",true);
	}
	
	//Sloppy but only way it ended up finally working:
	if(args["includeChildren"] == true && target.transform.childCount>0){
		var a : float;
		var t : float;
		var d : float;
		var tr : String;
		
		if(args["alpha"]==null){
			a=0;
		}else{
			a=args["alpha"];
		}
		
		if(args["time"]==null){
			t=fadeDefaults["time"];
		}else{
			t=args["time"];	
		}
		
		if(args["delay"]==null){
			d=fadeDefaults["delay"];
		}else{
			d=args["delay"];
		}
		
		if(args["transition"]==null){
			tr=fadeDefaults["transition"];
		}else{
			tr=args["transition"];
		}
		
		for (var child : Transform in target.transform) {
			iTween.fadeFrom(child.gameObject,{"alpha":a,"time":t,"delay":d,"transition":tr});
		}
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

//MoveBy static register:
static function moveBy(target: GameObject,args: Hashtable):void{
	args.Add("type","moveTo");
	
	var xValue : float;
	var yValue : float;
	var zValue : float;
	
	if(args.Contains("amount")){
		var amount : Vector3 = args["amount"];
		args["x"]=amount.x;
		args["y"]=amount.y;
		args["z"]=amount.z;			
		args["position"]=null;
	}
	
	if(args.Contains("x")){
		xValue = args["x"];
		xValue+=target.transform.localPosition.x;
		args["x"]=xValue;
	}else{
		xValue=target.transform.localPosition.x;
	}
	
	if(args.Contains("y")){
		yValue=args["y"];
		yValue+=target.transform.localPosition.y;
		args["y"]=yValue;
	}else{
		yValue=target.transform.localPosition.y;	
	}
	
	if(args.Contains("z")){
		zValue=args["z"];
		zValue+=target.transform.localPosition.z;
		args["z"]=zValue;
	}else{
		zValue=target.transform.localPosition.z;	
	}
	init(target,args);
}

//MoveByWorld static register:
static function moveByWorld(target: GameObject,args: Hashtable):void{
	args.Add("type","moveToWorld");
	
	var xValue : float;
	var yValue : float;
	var zValue : float;
	
	if(args.Contains("amount")){
		var amount : Vector3 = args["amount"];
		args["x"]=amount.x;
		args["y"]=amount.y;
		args["z"]=amount.z;			
		args["position"]=null;
	}
	
	if(args.Contains("x")){
		xValue=args["x"];
		xValue+=target.transform.position.x;
		args["x"]=xValue;
	}else{
		xValue=target.transform.position.x;
	}
	
	if(args.Contains("y")){
		yValue=args["y"];
		yValue+=target.transform.position.y;
		args["y"]=yValue;
	}else{
		yValue=target.transform.position.y;
	}
	
	if(args.Contains("z")){
		zValue=args["z"];
		zValue+=target.transform.position.z;
		args["z"]=zValue;
	}else{
		zValue=target.transform.position.z;
	}
	
	init(target,args);
}

//MoveTo static register:
static function moveTo(target: GameObject,args: Hashtable):void{
	if(args["type"]==null){
		args.Add("type","moveTo");	
	}
	init(target,args);
}

//MoveToWorld static register:
static function moveToWorld(target: GameObject,args: Hashtable):void{
	args.Add("type","moveToWorld");	
	init(target,args);
}

//MoveTo static register:
static function moveToBezier(target: GameObject,args: Hashtable):void{
	if(args["type"]==null){
		args.Add("type","moveToBezier");	
	}
	init(target,args);
}

//MoveToWorld static register:
static function moveToBezierWorld(target: GameObject,args: Hashtable):void{	
	if(args["type"] == null){
		args.Add("type","moveToBezierWorld");	
	}
	init(target,args);
}

//MoveFrom static register:
static function moveFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
	if(args.Contains("position")){
		var coordinates : Vector3 = args["position"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;			
		args["position"]=null;
	}
		
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
		target.transform.localPosition.z=args["z"];
		args["z"]=destinationHold;
	}else{
		args["z"]=target.transform.localPosition.z;
	}
	
	if(args["type"] == null){
		args.Add("type","moveTo");	
	}
	
	init(target,args);
}

//MoveFromWorld static register:
static function moveFromWorld(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
	if(args.Contains("position")){
		var coordinates : Vector3 = args["position"];
		
		destinationHold = target.transform.localPosition.x;
		target.transform.localPosition.x=coordinates.x;
		coordinates.x=destinationHold;
		
		destinationHold = target.transform.localPosition.y;
		target.transform.localPosition.y=coordinates.y;
		coordinates.y=destinationHold;
		
		destinationHold = target.transform.localPosition.z;
		target.transform.localPosition.z=coordinates.z;
		coordinates.z=destinationHold;
		
		args["position"]=coordinates;
	}else{
		if(args.Contains("x")){
			destinationHold = target.transform.position.x;
			target.transform.position.x=args["x"];
			args["x"]=destinationHold;
		}else{
			args["x"]=target.transform.position.x;
		}	

		if(args.Contains("y")){
			destinationHold = target.transform.position.y;
			target.transform.position.y=args["y"];
			args["y"]=destinationHold;
		}else{
			args["y"]=target.transform.position.y;
		}

		if(args.Contains("z")){
			destinationHold = target.transform.position.z;
			target.transform.position.z=args["z"];
			args["z"]=destinationHold;
		}else{
			args["z"]=target.transform.position.z;
		}
	}

	if(args["type"] == null){
		args.Add("type","moveToWorld");	
	}
	init(target,args);
}

//ScaleBy static register:
static function scaleBy(target: GameObject,args: Hashtable):void{
	args.Add("type","scaleTo");
	
	var xValue : float;
	var yValue : float;
	var zValue : float;
	
	if(args.Contains("amount")){
		var coordinates : Vector3 = args["amount"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;			
		args["amount"]=null;
	}
	
	if(args.Contains("x")){
		xValue=args["x"];
		xValue=target.transform.localScale.x*xValue;
		args["x"]=xValue;
	}else{
		xValue=target.transform.localScale.x;
	}
	
	if(args.Contains("y")){
		yValue=args["y"];
		yValue=target.transform.localScale.y*yValue;
		args["y"]=yValue;
	}else{
		yValue=target.transform.localScale.y;
	}
	
	if(args.Contains("z")){
		zValue=args["z"];
		zValue=target.transform.localScale.z*zValue;
		args["z"]=zValue;
	}else{
		zValue=target.transform.localScale.z;
	}
	
	init(target,args);
}

//ScaleAdd static register:
static function scaleAdd(target: GameObject,args: Hashtable):void{
	args.Add("type","scaleTo");
	
	var xValue : float;
	var yValue : float;
	var zValue : float;
	
	if(args.Contains("amount")){
		var coordinates : Vector3 = args["amount"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;			
		args["amount"]=null;
	}
	
	if(args.Contains("x")){
		xValue=args["x"];
		xValue+=target.transform.localScale.x;
		args["x"]=xValue;
	}else{
		xValue=target.transform.localScale.x;
	}
	
	if(args.Contains("y")){
		yValue=args["y"];
		yValue+=target.transform.localScale.y;
		args["y"]=yValue;
	}else{
		yValue=target.transform.localScale.y;
	}
	
	if(args.Contains("z")){
		zValue=args["z"];
		zValue+=target.transform.localScale.z;
		args["z"]=zValue;
	}else{
		zValue=target.transform.localScale.z;
	}
	
	init(target,args);
}

//Scale to static register:
static function scaleTo(target: GameObject,args: Hashtable):void{
	if(args["type"] == null){
		args.Add("type","scaleTo");	
	}
	init(target,args);
}

//Scale from static register:
static function scaleFrom(target: GameObject,args: Hashtable){
	var destinationHold : float;
	
	if(args.Contains("scale")){
		var coordinates : Vector3 = args["scale"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;			
		args["scale"]=null;
	}
	
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
	if(args["type"] == null){
		args.Add("type","rotateTo");	
	}	
	init(target,args);
}

//Rotate from static register:
static function rotateFrom(target: GameObject,args: Hashtable){
	if(target.guiTexture || target.guiText){
		Debug.LogError("ERROR: GUITextures cannot be rotated!");
        return;		
	}
	
	var destinationHold : float;
	
	if(args.Contains("rotation")){
		var coordinates : Vector3 = args["rotation"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;			
		args["rotation"]=null;
	}

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
	
	if(args.Contains("color")){
		destinationHold = args["color"];
		args["r"] = destinationHold.r;
		args["g"] = destinationHold.g;
		args["b"] = destinationHold.b;
		args["color"]=null;
	}
	
	if(args["r"]==null){
		args["r"] = 0;	
	}
	if(args["g"]==null){
		args["g"] = 0;	
	}
	if(args["b"]==null){
		args["b"] = 0;	
	}
	
	//Handle children:
	if(args["includeChildren"] == null){
		args.Add("includeChildren",true);
	}
	
	//Sloppy but only way it ended up finally working:
	if(args["includeChildren"] == true && target.transform.childCount>0){
		var c : Color = Color(args["r"],args["g"],args["b"]);
		var t : float;
		var d : float;
		var tr : String;
				
		if(args["time"]==null){
			t=fadeDefaults["time"];
		}else{
			t=args["time"];	
		}
		
		if(args["delay"]==null){
			d=fadeDefaults["delay"];
		}else{
			d=args["delay"];
		}
		
		if(args["transition"]==null){
			tr=fadeDefaults["transition"];
		}else{
			tr=args["transition"];
		}
		
		for (var child : Transform in target.transform) {
			iTween.colorFrom(child.gameObject,{"color":c,"time":t,"delay":d,"transition":tr});
		}
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
	if(args["type"] == null){
		args.Add("type","colorTo");	
	}
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
			StartCoroutine("fadeTo",args);
			break;
			fadeTo({}); //fake call to avoid editor warnings 
			
		case "moveTo":
			StartCoroutine("moveTo",args);
			break;
			moveTo({}); //fake call to avoid editor warnings 
			
		case "moveToWorld":
			StartCoroutine("moveToWorld",args);
			break;
			moveToWorld({}); //fake call to avoid editor warnings 
			
		case "moveToBezier":
			args["isLocal"] = "true";			
			StartCoroutine("moveToBezier",args);
			break;
			moveToBezier({}); //fake call to avoid editor warnings 
			
		case "moveToBezierWorld":
			args["isLocal"] = "false";			
			StartCoroutine("moveToBezier",args);			
			break;
			moveToBezier({}); //fake call to avoid editor warnings 
			
		case "scaleTo":
			StartCoroutine("scaleTo",args);			
			break;
			scaleTo({}); //fake call to avoid editor warnings 
			
		case "rotateTo":
			StartCoroutine("rotateTo",args);
			break;
			rotateTo({}); //fake call to avoid editor warnings 
			
		case "rotateBy":
			StartCoroutine("rotateBy",args);
			break;	
			rotateBy({}); //fake call to avoid editor warnings 
			
		case "colorTo":
			StartCoroutine("colorTo",args);
			break;
			colorTo({}); //fake call to avoid editor warnings 
			
		case "punchPosition":
			StartCoroutine("punchPosition",args);
			break;
			punchPosition({}); //fake call to avoid editor warnings 
			
		case "punchRotation":
			StartCoroutine("punchRotation",args);
			break;
			punchRotation({}); //fake call to avoid editor warnings 
			
		case "shake":
			StartCoroutine("shake",args);
			break;
			shake({}); //fake call to avoid editor warnings 
			
		case "audioTo":
			StartCoroutine("audioTo",args);
			break;
			audioTo({}); //fake call to avoid editor warnings 
			
		case "stab":
			StartCoroutine("stab",args);
			break;
			stab({}); //fake call to avoid editor warnings 
			
		default:
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
	if(args["audioSource"]==null){
		args.Add("audioSource",audio);
	}
	if(args["volume"]==null){
		args.Add("volume",stabDefaults["volume"]);
	}
	if(args["pitch"]==null){
		args.Add("pitch",stabDefaults["pitch"]);
	}
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		
		sound.volume =easingFunc(startV,endV,i);
		sound.pitch =easingFunc(startP,endP,i);
				
		yield;
	}
	
	sound.volume = endV;
	sound.pitch = endP;
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	var start : Vector3=obj.localPosition;

	//coordiantes:
	if(args["amount"] != null){
		var coordinates : Vector3 = args["amount"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",0);
		}
		if(args["y"]==null){
			args.Add("y",0);
		}
		if(args["z"]==null){
			args.Add("z",0);
		}
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
			obj.localPosition.x =start.x + Random.Range(-shakeMagnitude.x, shakeMagnitude.x);
			obj.localPosition.y =start.y + Random.Range(-shakeMagnitude.y, shakeMagnitude.y);
			obj.localPosition.z =start.z + Random.Range(-shakeMagnitude.z, shakeMagnitude.z);
		}
		if(impact){
			impact = false;
			obj.localPosition.x += shakeMagnitude.x;
			obj.localPosition.y += shakeMagnitude.y;
			obj.localPosition.z += shakeMagnitude.z;
		}
		shakeMagnitude.x=xValue-(i*xValue);
		shakeMagnitude.y=yValue-(i*yValue);
		shakeMagnitude.z=zValue-(i*zValue);
		yield;
	}
	obj.localPosition=start;
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	Destroy (this);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
	}
	
	//coordiantes:
	if(args["rotation"] != null){
		var coordinates : Vector3 = args["rotation"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",0);
		}
		if(args["y"]==null){
			args.Add("y",0);
		}
		if(args["z"]==null){
			args.Add("z",0);
		}
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
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	var pos : Vector3 = obj.localPosition;
		
	//coordiantes:
	if(args["position"] != null){
		var coordinates : Vector3 = args["position"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",0);
		}
		if(args["y"]==null){
			args.Add("y",0);
		}
		if(args["z"]==null){
			args.Add("z",0);
		}
	}
	
	//define targets:
	var runTime : float = args["time"];
	
	//run tween:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		if(xValue>0){
			obj.localPosition.x=punch(xValue,i) + pos.x;
		}else if(xValue<0){
			obj.localPosition.x=-punch(Mathf.Abs(xValue),i) + pos.x;	
		}
		if(yValue>0){
			obj.localPosition.y=punch(yValue,i) + pos.y;
		}else if(yValue<0){
			obj.localPosition.y=-punch(Mathf.Abs(yValue),i) + pos.y;	
		}
		if(zValue>0){
			obj.localPosition.z=punch(zValue,i) + pos.z;
		}else if(zValue<0){
			obj.localPosition.z=-punch(Mathf.Abs(zValue),i) + pos.z;	
		}
		yield;
	}
	obj.localPosition=pos;
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	
	//Handle children:
	if(args["includeChildren"] == null){
		args.Add("includeChildren",true);
	}
	
	if(args["includeChildren"] == true && obj.childCount>0){
		//Remove callback to avoid multiple firing from children:
		var argsCopy : Hashtable = args.Clone();
		if(argsCopy.Contains("onComplete")){
			argsCopy.Remove("onComplete");
		}
		for (var child : Transform in obj) {
			iTween.fadeTo(child.gameObject,argsCopy);
		}
	}
	
	//coordiantes:
	if(args["alpha"]==null){
		args.Add("alpha",0);
	}
	
	//define targets:
	var endA  : float = args["alpha"];
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	var target : GameObject;
	
	//just so we know the type
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	if(obj.guiTexture){
		start=obj.guiTexture.color.a;
		
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {			
			obj.guiTexture.color.a=easingFunc(start,endA,i);			
			yield;
		}
		guiTexture.color.a= endA;
		if(args["onComplete"]){
			target = args["onCompleteTarget"];
			target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		
		switch(args["loopType"]){
		case "loop":
			guiTexture.color.a= start;
			iTween.fadeTo(gameObject,args);
			break;
		case "pingPong":
			args["alpha"]=start;
			iTween.fadeTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
	}	
	}else{
		start=obj.renderer.material.color.a;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {			
			obj.renderer.material.color.a=easingFunc(start,endA,i);			
			yield;
		}
		obj.renderer.material.color.a= endA;
		if(args["onComplete"]){
			target = args["onCompleteTarget"];
			target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		
		switch(args["loopType"]){
		case "loop":
			obj.renderer.material.color.a=start;
			iTween.fadeTo(gameObject,args);
			break;
		case "pingPong":
			args["alpha"]=start;
			iTween.fadeTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;
		}
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	if(args["position"] != null){
		var coordinates : Vector3 = args["position"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",start.x);
		}
		if(args["y"]==null){
			args.Add("y",start.y);
		}
		if(args["z"]==null){
			args.Add("z",start.z);
		}
	}
	
	//define targets:
	var end : Vector3 = Vector3(args["x"], args["y"], args["z"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {		
		obj.localPosition.x=easingFunc(start.x,end.x,i);
		obj.localPosition.y=easingFunc(start.y,end.y,i);
		obj.localPosition.z=easingFunc(start.z,end.z,i);
		yield;
	}
	obj.localPosition=end;
	
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	
	switch(args["loopType"]){
		case "loop":
			obj.localPosition.x=start.x;
			obj.localPosition.y=start.y;
			obj.localPosition.z=start.z;
			iTween.moveTo(gameObject,args);
			break;
		case "pingPong":
			args["x"]=start.x;
			args["y"]=start.y;
			args["z"]=start.z;
			iTween.moveTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
	}	
}

//Move to application:
private function moveToWorld(args:Hashtable){	
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	var start : Vector3 = obj.position;
	
	//coordiantes:
	if(args["position"] != null){
		var coordinates : Vector3 = args["position"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",start.x);
		}
		if(args["y"]==null){
			args.Add("y",start.y);
		}
		if(args["z"]==null){
			args.Add("z",start.z);
		}
	}
	
	//define targets:
	var end : Vector3 = Vector3(args["x"], args["y"], args["z"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {		
		obj.position.x=easingFunc(start.x,end.x,i);
		obj.position.y=easingFunc(start.y,end.y,i);
		obj.position.z=easingFunc(start.z,end.z,i);
		yield;
	}
	
	obj.position=end;
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	
	switch(args["loopType"]){
		case "loop":
			obj.position.x=start.x;
			obj.position.y=start.y;
			obj.position.z=start.z;
			iTween.moveTo(gameObject,args);
			break;
		case "pingPong":
			args["x"]=start.x;
			args["y"]=start.y;
			args["z"]=start.z;
			iTween.moveTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
	}	
}

//Bezier move to application: - David Bardos
private function moveToBezier(args:Hashtable){	
	//construct args:
	if(args["time"]==null){
		args.Add("time",moveBezierDefaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",moveBezierDefaults["delay"]);
	}
	if(args["transition"]==null){
		args.Add("transition",moveBezierDefaults["transition"]);
	}
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
	}
	if(args["orientToPath"] == null){
		args.Add("orientToPath",true);
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
	var obj : GameObject = gameObject;
		
	//define targets:
	var easing : String = args["transition"];
	var runTime : float = args["time"];

	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//init bezier
	var n : float;
	var segments : float;
	var beziers : Array = Array(args["bezier"]);
	var _beziersPI: Array;
	var props : Hashtable = {};
    var i : float;
    var ii : float;
    var b : Object;
    var p : DictionaryEntry;
    var start : Vector3;
	
	if (args["isLocal"] == true) {
		beziers.splice(0,0, obj.transform.localPosition);
		start=obj.transform.localPosition;
	}
	else {
		beziers.splice(0,0, obj.transform.position);
		start=obj.transform.position;
	}

	_beziersPI = ParseBeziers(beziers);
	var iNumPoints : int = _beziersPI.length;
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		//get the easing as a percentage... not * 100, of course
		var virtTimePart : float = easingFunc(0, 1, i);

		//get the array position of the intermediate point that we want
		var iCurAxisPoint : int;
		if (virtTimePart <= 0)
		{
			//first array position
			iCurAxisPoint = 0;
		}
		else if (virtTimePart >= 1)
		{
			//last array position
			iCurAxisPoint = iNumPoints - 1;
		}
		else
		{
			//the transition is > 0 and less than 1. get the position we're looking for.
			iCurAxisPoint = Mathf.Floor(iNumPoints * virtTimePart);
		}

		//we are getting how far past the current point we are.
		var timeFract : float = iNumPoints * virtTimePart - iCurAxisPoint;
		
		//get the point info that we are interested in dealing with.
		var bpi : BezierPointInfo = _beziersPI[iCurAxisPoint];

		//get the new vector... I love vector math!
		var newVector : Vector3 = bpi.starting + timeFract * (2 * (1 - timeFract) * (bpi.intermediate - bpi.starting) + timeFract * (bpi.end - bpi.starting));

		//orientToPath - cutting off outer ends of curve percentage to avoid lookAt jitters:
		if(args["orientToPath"] == true && i <.98 && i>.02){
			obj.transform.LookAt(newVector);
		}
		
		//look at target
		if(args["lookAt"] != null){
			args["orientToPath"] == false;
			var lookTarget : Vector3 = args["lookAt"];
			obj.transform.LookAt(lookTarget);
		}

		//move object				
		if (args["isLocal"] == "true") 
		{
			obj.transform.localPosition = newVector;			
		}
		else
		{
			obj.transform.position = newVector; 			
		}		
		yield;
	}
	
	var bpiEnd : BezierPointInfo = _beziersPI[_beziersPI.Count - 1];
	
	//get the object to it's final resting position
	if (args["isLocal"] == "true") 
	{
		obj.transform.localPosition = bpiEnd.end;
		
		switch(args["loopType"]){
	  	 	case "loop":
				obj.transform.localPosition.x=start.x;
				obj.transform.localPosition.y=start.y;
				obj.transform.localPosition.z=start.z;
				iTween.moveToBezier(gameObject,args);
				break;
			case "pingPong":
				Debug.LogError("WARNING: Ping-ponging a bezier move will create undesired results!  Consider using a call back method and launching a new bezier.");
				args["x"]=start.x;
				args["y"]=start.y;
				args["z"]=start.z;
				beziers.Reverse();
				args["bezier"]=beziers;
				iTween.moveToBezier(gameObject,args);
				break;
			default:
				Destroy (this);
				break;	
		}
	}
	else
	{
		obj.transform.position = bpiEnd.end;    
		
		switch(args["loopType"]){
	  	 	case "loop":
				obj.transform.position.x=start.x;
				obj.transform.position.y=start.y;
				obj.transform.position.z=start.z;
				iTween.moveToBezier(gameObject,args);
				break;
			case "pingPong":
				Debug.LogError("WARNING: Ping-ponging a bezier move will create undesired results!  Consider using a call back method and launching a new bezier.");
				args["x"]=start.x;
				args["y"]=start.y;
				args["z"]=start.z;
				beziers.Reverse();
				args["bezier"]=beziers;
				iTween.moveToBezier(gameObject,args);
				break;
			default:
				Destroy (this);
				break;	
		}		                
	}
	
		
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	
	
}

//Helper method for translating control points into bezier information. - David Bardos
private function ParseBeziers(points: Array) : Array
	{
		var returnPoints : Array= new Array();

		if (points.Count > 2)
		{
			//the first item is the starting position of the current point for that axis. So, we are storing off the following values:
			//The starting position for the current point for the axis
			//A smoothing point for the curve
			//The next major point		
			var iCurPoint : int;
			
			for (iCurPoint = 0; iCurPoint < points.Count - 1; iCurPoint++)
			{
				var curPoint : Vector3 = points[iCurPoint];

				//I know I am going to store exactly 3, the starting, intermediate and end.
				var curSetofPoints: BezierPointInfo = new BezierPointInfo();

				curSetofPoints.starting = curPoint;
				if (iCurPoint == 0)
				{
					var p1 : Vector3 = points[1];
					var p2 : Vector3 = points[2];
					curSetofPoints.intermediate = p1 - ((p2 - curPoint) / 4);					
				}
				else
				{
					//double the current point minus the prior point's intermediate position
					var bpiint: BezierPointInfo = returnPoints[iCurPoint - 1];
					curSetofPoints.intermediate = 2 * curPoint - bpiint.intermediate;
				}
				//This is fine because we end at the next to last item.
				curSetofPoints.end = points[iCurPoint + 1];

				returnPoints.push(curSetofPoints);
			}
		}
		else
		{
			var curSetofPoints2: BezierPointInfo = new BezierPointInfo();
			curSetofPoints2.starting = points[0];
			curSetofPoints2.end = points[1];
			curSetofPoints2.intermediate = ((curSetofPoints2.starting + curSetofPoints2.end) / 2);
			
			returnPoints.push(curSetofPoints2);
		}

		return returnPoints;
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	if(args["scale"] != null){
		var coordinates : Vector3;
		coordinates = args["scale"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",start.x);
		}
		if(args["y"]==null){
			args.Add("y",start.y);
		}
		if(args["z"]==null){
			args.Add("z",start.z);
		}
	}
	
	//define targets:
	var end : Vector3 = Vector3(args["x"], args["y"], args["z"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		obj.localScale.x=easingFunc(start.x,end.x,i);
		obj.localScale.y=easingFunc(start.y,end.y,i);
		obj.localScale.z=easingFunc(start.z,end.z,i);
				
		yield;
	}
	obj.localScale=end;	
	
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	
	switch(args["loopType"]){
		case "loop":
			obj.localScale.x=start.x;
			obj.localScale.y=start.y;
			obj.localScale.z=start.z;
			iTween.scaleTo(gameObject,args);
			break;
		case "pingPong":
			args["x"]=start.x;
			args["y"]=start.y;
			args["z"]=start.z;
			iTween.scaleTo(gameObject,args);
			break;
		default:
			Destroy (this);
	}	
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	if(args["rotation"] != null){
		var coordinates : Vector3;
		coordinates = args["rotation"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",start.x);
		}
		if(args["y"]==null){
			args.Add("y",start.y);
		}
		if(args["z"]==null){
			args.Add("z",start.z);
		}
	}
		
	//define targets:
	var end = Vector3(clerp(start.x,args["x"],1), clerp(start.y,args["y"],1), clerp(start.z,args["z"],1));
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		obj.localRotation=Quaternion.Euler(easingFunc(start.x,end.x,i),easingFunc(start.y,end.y,i),easingFunc(start.z,end.z,i));		
		yield;
	}
	
	obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
	
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
	}
	
	switch(args["loopType"]){
		case "loop":
			obj.localRotation=Quaternion.Euler(start);
			iTween.rotateTo(gameObject,args);
			break;
		case "pingPong":
			args["x"]=start.x;
			args["y"]=start.y;
			args["z"]=start.z;
			iTween.rotateTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
	}	
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	if(args["amount"] != null){
		var coordinates : Vector3;
		coordinates = args["amount"];
		args["x"]=coordinates.x;
		args["y"]=coordinates.y;
		args["z"]=coordinates.z;
	}else{
		if(args["x"]==null){
			args.Add("x",start.x);
		}
		if(args["y"]==null){
			args.Add("y",start.y);
		}
		if(args["z"]==null){
			args.Add("z",start.z);
		}
	}
	
	//define targets:
	var xValue : float = args["x"];
	var yValue : float = args["y"];
	var zValue : float = args["z"];
	
	var	end = Vector3(360*xValue + obj.localEulerAngles.x, 360*yValue + obj.localEulerAngles.y, 360 *zValue + obj.localEulerAngles.x);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	//run tween:
	for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
		obj.localRotation=Quaternion.Euler(easingFunc(start.x,end.x,i),easingFunc(start.y,end.y,i),easingFunc(start.z,end.z,i));		
		yield;
	}
	
	obj.localRotation=Quaternion.Euler(end.x,end.y,end.z);
	
	if(args["onComplete"]){
		var target : GameObject = args["onCompleteTarget"];
		target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
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
	if(args["onCompleteTarget"]==null){
		args.Add("onCompleteTarget",gameObject);
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
	
	//Handle children:
	if(args["includeChildren"] == null){
		args.Add("includeChildren",true);
	}
	
	if(args["includeChildren"] == true && obj.childCount>0){
		//Remove callback to avoid multiple firing from children:
		var argsCopy : Hashtable = args.Clone();
		if(argsCopy.Contains("onComplete")){
			argsCopy.Remove("onComplete");
		}
		for (var child : Transform in obj) {
			iTween.colorTo(child.gameObject,argsCopy);
		}
	}
	
	//coordiantes:
	if(args["color"] != null){
		var coordinates : Color;
		coordinates = args["color"];
		args["r"]=coordinates.r;
		args["g"]=coordinates.g;
		args["b"]=coordinates.b;
	}else{
		if(args["r"]==null){
			args.Add("r",0);
		}
		if(args["g"]==null){
			args.Add("g",0);
		}
		if(args["b"]==null){
			args.Add("b",0);
		}
	}
	
	//define targets:
	end = Color (args["r"],args["g"],args["b"]);
	var easing : String = args["transition"];
	var runTime : float = args["time"];
	var target : GameObject;
	
	var easingFunc = easeInQuad;
	easingFunc = TRANSITIONS[easing];
	
	if(obj.guiTexture){
		start=obj.guiTexture.color;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {			
			obj.guiTexture.color.r=easingFunc(start.r,end.r,i);
			obj.guiTexture.color.g=easingFunc(start.g,end.g,i);
			obj.guiTexture.color.b=easingFunc(start.b,end.b,i);
			yield;
		}
		
		obj.guiTexture.color.r=end.r;
		obj.guiTexture.color.g=end.g;
		obj.guiTexture.color.b=end.b;
		if(args["onComplete"]){
			target = args["onCompleteTarget"];
			target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);
		}
		
		switch(args["loopType"]){
		case "loop":
			obj.guiTexture.color.r=start.r;
			obj.guiTexture.color.g=start.g;
			obj.guiTexture.color.b=start.b;
			iTween.colorTo(gameObject,args);
			break;
		case "pingPong":
			args["r"]=start.r;
			args["g"]=start.g;
			args["b"]=start.b;
			iTween.colorTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
		}	
	}else{
		start=obj.renderer.material.color;
		for (i = 0.0; i < 1.0; i += Time.deltaTime*(1/runTime)) {
			obj.renderer.material.color.r=easingFunc(start.r,end.r,i);
			obj.renderer.material.color.g=easingFunc(start.g,end.g,i);
			obj.renderer.material.color.b=easingFunc(start.b,end.b,i);
			yield;
		}
		
		obj.renderer.material.color.r=end.r;
		obj.renderer.material.color.g=end.g;
		obj.renderer.material.color.b=end.b;
		
		if(args["onComplete"]){
			target = args["onCompleteTarget"];
			target.SendMessage(args["onComplete"], args["onCompleteParams"], SendMessageOptions.DontRequireReceiver);	
		}
		
		switch(args["loopType"]){
		case "loop":
			obj.renderer.material.color.r=start.r;
			obj.renderer.material.color.g=start.g;
			obj.renderer.material.color.b=start.b;
			iTween.colorTo(gameObject,args);
			break;
		case "pingPong":
			args["r"]=start.r;
			args["g"]=start.g;
			args["b"]=start.b;
			iTween.colorTo(gameObject,args);
			break;
		default:
			Destroy (this);
			break;	
		}		
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