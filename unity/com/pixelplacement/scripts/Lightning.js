var obj : GameObject;
obj = gameObject;
obj.guiTexture.color.a=0;

wait();

function strike(){
	iTween.fadeFrom(obj,.5,.15,.73,"linear");
	//iTween.stab(obj,lightningAudio,5,1,.01);	
	iTween.fadeFrom(obj,.5,1.2,.88,"linear");
	wait();	
}

function wait(){
	yield WaitForSeconds(Random.value*5);
	strike();
}