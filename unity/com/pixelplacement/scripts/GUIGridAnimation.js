#pragma strict

//Vars:
private var obj : GameObject;
obj = gameObject;
private var tileSize : Vector2;
var columns : float = 0;
var rows : float = 0;
private var pixelSize : Vector2;
private var startPosition : Vector2; 
var framesPerSecond : float = 30;


//Animations:
function Start (){
	tileSize=Vector2(1/columns,1/rows);
	pixelSize=Vector2(obj.guiTexture.pixelInset.width,obj.guiTexture.pixelInset.height);
	startPosition=Vector2(0,tileSize.y*(rows-1));
}

function Update(){
	var index : int = Time.time * framesPerSecond;
	obj.guiTexture.pixelInset = Rect (-(pixelSize.x/columns)*(index%columns), -(pixelSize.y/rows)*(Mathf.Floor((index/columns)%rows)), pixelSize.x, pixelSize.y);
}
