//iThrottle: Limits editor frame rate to simulate iPhone resources.
//Author: Dad
//Usage: Component
//Methods: None 
var capFps : float = 35.0;
var canCap : String;
canCap=Application.platform.ToString();

function Update () {
	if(canCap=="IPhonePlayer"){
		System.Threading.Thread.Sleep(1000/capFps);
	}
}