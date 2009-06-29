#pragma strict

private var obj : GameObject;
private var dampen : float = .95;
private var maxZ : float = 1.3;
private var minZ : float = .3;
private var vx : float;
private var vy : float;
private var vz : float;
private var vrx : float;
private var vry : float;
private var vrz : float;
private var randomRange=.0005;

obj = gameObject;

function Update(){
	vx+=Random.Range(-randomRange,randomRange);
	vy+=Random.Range(-randomRange,randomRange);
	vz+=Random.Range(-randomRange,randomRange);
	vrx+=Random.Range(-randomRange,randomRange);
	vry+=Random.Range(-randomRange,randomRange);
	vrz+=Random.Range(-randomRange,randomRange);	

	obj.transform.position.x+=vx;
	obj.transform.position.y+=vy;
	obj.transform.position.z+=vz;
	
	obj.transform.rotation.x+=vrx;
	obj.transform.rotation.y+=vry;
	obj.transform.rotation.z+=vrz;		
		
	vx *= dampen;	vy *= dampen;	vz *= dampen;
	vrx *= dampen;	vry *= dampen;	vrz *= dampen;
}