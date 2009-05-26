//FPS: Utilizes a GUIText object to display rough frames per second.
//Author: Aras Pranckevicius
//Usage: Component
//Methods: None

#pragma strict

//Init vars:
var updateInterval = 0.5;
private var accum = 0.0;
private var frames = 0;
private var timeleft : float;

//Verify GUIText and initialize:
function Start()
{
    if( !guiText )
    {	
    	Debug.LogError("ERROR: FramesPerSecond needs a GUIText component!",this);
        enabled = false;
        return;
    }
    timeleft = updateInterval; 
}

//Calculate and display FPS:
function Update()
{
    timeleft -= Time.deltaTime;
    accum += Time.timeScale/Time.deltaTime;
    ++frames;
    if( timeleft <= 0.0 )
    {
        guiText.text = "" + (accum/frames).ToString("f2");
        timeleft = updateInterval;
        accum = 0.0;
        frames = 0;
    }
}
 