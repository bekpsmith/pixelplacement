//iTextureAnimatorEventSheet: Simple component to cycle through a sprite sheet to allow texture animations.
//Current version requires the sheet to have an equal number of rows and columns.  
//Author: Pixelplacement
//Usage: Component
//Methods: None

var tileSize : Vector2;
var columns : float = 6;
var rows : float = 7;
var startPosition : Vector2; 
var framesPerSecond : float = 41;

function Start (){
	tileSize=Vector2(1/columns,1/rows);
	startPosition=Vector2(0,tileSize.y*(rows-1));
	renderer.material.mainTextureOffset = startPosition;
	renderer.material.mainTextureScale=Vector2(1/columns,1/rows);
}

function Update(){
	var index : int = Time.time * framesPerSecond;
	var offset : Vector2 = Vector2(tileSize.x*(index%columns),startPosition.y-(Mathf.Floor((index/columns)%rows)*tileSize.y));
	renderer.material.mainTextureOffset = offset;
}
