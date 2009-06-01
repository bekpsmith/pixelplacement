//iSwap: Establishes environment for easy 'scenes' based on GO holders to maximize iPhone performance.
//Place on the first GO you want shown. To avoid errors ensure all scene GOs are active before publishing.
//NullRefrenceException errors thrown on publish can be ignored as they only mean a scene GO was disabled on compile.
//Author: Pixelplacement
//Usage: Component
//Methods: Swap

//Example:
/*
if (Input.GetKeyDown ("1")){
		iSwap.swapTo("CubeScene");
	}
	if (Input.GetKeyDown ("2")){
		iSwap.swapTo("GroundScene");
	}
*/

#pragma strict

//Init vars:
static var staticScenes : GameObject[];
static var staticInitialScene : String;
static var activeScene:String;
private var initialScene : GameObject;
private var nextSceneGO : GameObject;
var scenes : GameObject[];

//Run initialize:
function Start(){
	initialScene = gameObject;
	if(scenes.length<2){
		Debug.LogError("ERROR: Scenes array requires more than one GO!",this);
		enabled = false;
        return;
	}
	init(scenes,initialScene);
	Destroy(this);
}

//Initialize static variables:
static function init(a:Array,b:GameObject):void{
	staticScenes = a;
	activeScene=b.name;
	for(var scene in staticScenes){
		if(scene.name != activeScene){
			GameObject.Find(scene.name).SetActiveRecursively(false);
		}	
	}
}

//Swap scenes:
static function swapTo(nextScene:String):void{
	for(var i : int; i<staticScenes.length; i++){
		if(staticScenes[i].name == nextScene){
			var nextSceneGO=staticScenes[i];
		}
	}
	GameObject.Find(activeScene).SetActiveRecursively(false);
	nextSceneGO.SetActiveRecursively(true);
	activeScene=nextSceneGO.name;
}

//Depricated:
static function swap(nextScene:String):void{
	swapTo(nextScene);
}