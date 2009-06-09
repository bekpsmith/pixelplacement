//iTween: Easy movement, rotation and fading of GUITextures and meshes.

//To do animations: volumeTo(),volumeFrom(),fadeAway(duration,delay),colorTo(),colorFrom(),twinkle(),strobe()
//To do methods: stop()
//Clean up: Handle destruction of lingering iTweens after object has been disabled - i.e. avoid issues with iSwap


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




static function scaleTo(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){

static function scaleFrom(obj: GameObject,x: float,y: float,z: float,duration: float,delay: float, easing: String){

	if(paramList[paramList.length-1]=="stab"){
	if(paramList[paramList.length-1]=="scaleTo"){
	if(paramList[paramList.length-1]=="scaleFrom"){
	

//Scale applied code:
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){
		if(easing=="easeOutBack"){
		if(easing=="easeInOutBack"){
	if(delay>0){
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){

//Stab applied code:
function stab(sound: AudioSource,volume: float,pitch: float,delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	sound.volume=volume;
	sound.pitch=pitch;
	sound.PlayOneShot(sound.clip);
	yield;

//Shake applied code:
//Negative values will designate initial impact direction
function shake(x : float, y: float, z: float, duration: float, delay: float){
	if(delay > 0){
		yield WaitForSeconds (delay);
	}
	var shakeMagnitude : Vector3 = Vector3(x,y,z);
	start=obj.transform.position;
	var impact : boolean=true;
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
		
	obj.position=start;

//Fade applied code:
		yield WaitForSeconds (delay);
	}
		yield WaitForSeconds (delay);
	}
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){
		if(easing=="easeOutBack"){
		if(easing=="easeInOutBack"){
	if(delay>0){
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){
		if(easing=="easeOutBack"){
		if(easing=="easeInOutBack"){
		yield WaitForSeconds (delay);
	}
		if(easing=="easeInQuart"){
		if(easing=="easeInQuint"){
		if(easing=="easeInSine"){
		if(easing=="easeInExpo"){
		if(easing=="easeInCirc"){
		if(easing=="bounce"){
		if(easing=="easeInBack"){
		if(easing=="easeOutBack"){
		if(easing=="easeInOutBack"){

function easeInQuint(start : float, end : float, value : float) : float {
	value /= 1;
	end -= start;
	return end*value*value*value*value*value + start;
};

	value /= 1;
	value--;
	end -= start;
	return end*(value*value*value*value*value + 1) + start;
};

function easeInOutQuint(start : float, end : float, value : float) : float {
	value /= .5;
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