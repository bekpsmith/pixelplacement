private var swipeStart : Vector2;
private var swipeEnd : Vector2;
static var direction : String;

function Update () {
	//Swipe checking:
	if(iTouchGUI.onFingerTouch(new Rect(0,0,480,320))){
		swipeStart=iTouchGUI.position;
	}
	
	if(iTouchGUI.onFingerUp()){
		swipeEnd=iTouchGUI.position;
		calculateAngle();
	}
}

//Swipe angle calculation:
function calculateAngle(){
	var dx : float = swipeEnd.x - swipeStart.x;
	var dy : float = swipeEnd.y - swipeStart.y;
	var radians : float = Mathf.Atan2(dy,dx);
	var degrees : int = Mathf.Round(radians * 180 / Mathf.PI);
	calculateDirection(degrees);
}

//Swipe direction calculation:
function calculateDirection(degrees: float){
	if(degrees>=0 && degrees<90){
		direction="up";
	}
	if(degrees>=90 && degrees<=180){
		direction="left";
	}
	if(degrees<0 && degrees>=-90){
		direction="right";
	}
	if(degrees<-90 && degrees>-179){
		direction="down";
	}
}
