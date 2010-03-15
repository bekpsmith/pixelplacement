//pubs:
public var loadTexture : Texture2D;

//privs:
private var videoPath : String = "";
private var movieTexture : MovieTexture = new MovieTexture();

function Start(){
	loadVideo("Goetze_Final_small");	
}

function Update () {
	if(Input.GetKeyDown ("1")){
		loadVideo("Goetze_Final_small");
	}
	if(Input.GetKeyDown ("2")){
		loadVideo("HuggyClue30secFinalSm");
	}
	if(Input.GetKeyDown ("3")){
		loadVideo("LonghornVerCompressedSm");
	}
	if(Input.GetKeyDown ("4")){
		loadVideo("PBS_Cut1_Explore_half");
	}
	if(Input.GetKeyDown ("5")){
		loadVideo("PBS_PreKPromo_National_half");
	}
}

private function loadVideo(videoFile : String){
	guiTexture.texture = loadTexture;
	var www = new WWW(videoPath + videoFile + ".ogv");
	
	if(movieTexture){
		if(movieTexture.isPlaying){
			movieTexture.Stop();
		}
	}
	
	movieTexture = www.movie;
	
	
	
	while (!movieTexture.isReadyToPlay)
	yield;
	
	guiTexture.texture = movieTexture;
	
	//
	transform.localScale = Vector3 (0,0,0);
	transform.position = Vector3 (0.5,0.5,0);
	guiTexture.pixelInset.xMin = -movieTexture.width / 2;
	guiTexture.pixelInset.xMax = movieTexture.width / 2;
	guiTexture.pixelInset.yMin = -movieTexture.height / 2;
	guiTexture.pixelInset.yMax = movieTexture.height / 2;	
	//
	
	audio.clip = movieTexture.audioClip;
	//play:
	movieTexture.Play();
	audio.Play();
}