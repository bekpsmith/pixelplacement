//iThrottle: Limits editor frame rate to simulate iPhone resources.
//Author: Dad
//Usage: Component
//Methods: None 
var capFps = 35.0; 

function Update () { 
   System.Threading.Thread.Sleep(1000/capFps); 
}
