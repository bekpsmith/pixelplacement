static var registers : Array = new Array();
static var params : Array = new Array();
public var tweenType: String;
public var id : int = 0;
public var inProgress : boolean;

static function init(target: GameObject,args: Hashtable):void{
	registers.push(target);
	params.push(args);
	target.AddComponent ("iTween2");
}

function checkForConflicts(){
	var scripts = GetComponents (iTween2);
	for (var script : iTween2 in scripts) {
		if(script.inProgress){
			Destroy(script);
		}
	}
}

static function moveTo(target: GameObject,args: Hashtable):void{
	args.Add("type","moveTo");	
	init(target,args);
}

static function moveFrom(target: GameObject,args: Hashtable):void{
	var destinationHold : float;
	
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
	args.Add("type","moveTo");
}

function Start(){
	findRegister();
	var args : Hashtable = params[id];
	registers.RemoveAt(id);
	params.RemoveAt(id);
	tweenType=args["type"];
	if(args["type"]=="moveTo"){
		while (true){
			yield moveTo(args);
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

private function moveTo(args:Hashtable){
	//predefined defaults:
	var defaults : Hashtable = {"pos":gameObject.transform.position,"time":1,"delay":0,"transition":"easeInOutCubic"};
	
	//construct args:
	if(args["time"]==null){
		args.Add("time",defaults["time"]);
	}
	if(args["delay"]==null){
		args.Add("delay",defaults["delay"]);
	}
	if(args["time"]==null){
		args.Add("time",defaults["time"]);
	}
	if(args["transition"]==null){
		args.Add("transition",defaults["transition"]);
	}
	if(args["onComplete"]==null){
		args.Add("onComplete","null");
	}
	
	//delay:
	if(args["delay"] > 0){
		yield WaitForSeconds (args["delay"]);
	}
	
	//Look for conflicts:
	checkForConflicts();
	inProgress=true;
	
	//define object:
	var obj : Transform = gameObject.transform;
	var start : Vector3 = obj.position;
	
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
	obj.position=end;
	Destroy (this);	
}

//Easing curves
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