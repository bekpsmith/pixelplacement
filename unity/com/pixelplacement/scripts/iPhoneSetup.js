#pragma strict

//Vars:
var verticalOrientation:boolean=false;
var screenCanDarken:boolean=true;

//Modify iPhone settings:
function Start (){
	iPhoneSettings.verticalOrientation = verticalOrientation;
	iPhoneSettings.screenCanDarken = screenCanDarken;
}