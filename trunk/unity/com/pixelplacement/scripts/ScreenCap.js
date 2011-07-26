import System.IO;

private var screenShot : Texture2D; 
private var rt : RenderTexture; 
var res : int = 4096; 
    
function Start () { 
	rt = new RenderTexture(res, res, 24); 
	screenShot = new Texture2D(res, res); 
	camera.targetTexture = rt;
	Take();
}

function Take () { 
	yield WaitForEndOfFrame(); 
	RenderTexture.active = rt; 
	screenShot.ReadPixels( new Rect(0, 0, res, res), 0, 0 ); 
	screenShot.Apply(); 
	 
	var bytes = screenShot.EncodeToPNG(); 
	Destroy( screenShot ); 
	File.WriteAllBytes(Application.dataPath + "/../SavedScreen9.png", bytes); 
}