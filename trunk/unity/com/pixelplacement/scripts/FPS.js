#pragma strict

//Vars:
var updateInterval : float = 0.5;
private var accum : float;
private var frames : float;
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
 