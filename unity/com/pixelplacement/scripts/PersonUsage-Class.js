/*
var minAngle = 0.0;
var maxAngle = 90.0;
var yVelocity = 0;
var smooth = 0.3;

// Fades from minimum to maximum in one second
function Update () {
	//var angle = Mathf.SmoothStep(minAngle, maxAngle, Time.time);
	
	var yAngle = Mathf.SmoothDampAngle(minAngle,maxAngle, yVelocity, smooth);
	
	transform.eulerAngles = Vector3(0, angle, 0);
}
*/

/*
var angle : float;

function Update () {

	if(iPhoneInput.touchCount>0){
		var currentTouch : iPhoneTouch = iPhoneInput.GetTouch(0);
		angle = Mathf.Atan2(currentTouch.position.y-160,currentTouch.position.x-240);
	}
	
	var targetAngle : float = angle*180/Mathf.PI;
	transform.eulerAngles = Vector3(0, angle, 0);
	
}
*/

/*
var angle = Math.atan2(this._parent._ymouse-this._y,this._parent._xmouse-this._x);		// apply rotation to handle by converting angle into degrees		this._rotation = angle*180/Math.PI;
*/

public var screenRect : Rect;
private var screenRectCenter : Vector2;
private var touchId : int;

//Testing:
public var blk : GUITexture;
public var wht : GUITexture;

public function Awake() : void{
	screenRectCenter=Vector2(screenRect.x+(screenRect.width/2),screenRect.x+(screenRect.width/2));
	
	//Testing:
	blk.guiTexture.pixelInset=screenRect;
	wht.pixelInset.x=screenRectCenter.x;
	wht.pixelInset.y=screenRectCenter.y;	
	
	var bitch : Person = new Person(18,"female");
}

public function Update() : void{
	if(touchCheck()>-1){
		//var activeTouch : iPhoneTouch = iPhoneInput.GetTouch(touchId);
		//print(activeTouch.position.x + " " + activeTouch.position.y);
	}
}

private function touchCheck() : int{
	for(var i:int=0; i<iPhoneInput.touchCount; i++){
		if(iPhoneInput.GetTouch(i).phase == iPhoneTouchPhase.Began){
			touchId=i;
			return touchId;
		}
		
		if(iPhoneInput.GetTouch(touchId).phase == iPhoneTouchPhase.Ended){
			touchId=-1;
			return -1;
		}
	}
}