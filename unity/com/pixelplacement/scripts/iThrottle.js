//iThrottle: Limits editor frame rate to simulate iPhone resources.
//Author: Dad
//Usage: Component
//Methods: None 
var capFps : float = 2;
var platform : String;
platform=Application.platform.ToString();

function Update () {
	if(platform!="IPhonePlayer"){
		System.Threading.Thread.Sleep(1000/capFps);
	}
}