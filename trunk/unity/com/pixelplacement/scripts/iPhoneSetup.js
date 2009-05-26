//iPhoneSetup: Sets iPhone environemnt.
//Author: Pixelplacement
//Usage: Component
//Methods: None

#pragma strict

//Init vars:
var verticalOrientation:boolean=false;
var screenCanDarken:boolean=false;

//Modify iPhone settings:
function Start (){
	iPhoneSettings.verticalOrientation = verticalOrientation;
	iPhoneSettings.screenCanDarken = screenCanDarken;
}