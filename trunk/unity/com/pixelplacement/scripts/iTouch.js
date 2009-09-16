static var count : int = 0;
static var touched : boolean;
static var released : boolean;
static var position : Vector2;

private static var lastPosition : Vector2;
private static var touchInfo : iPhoneTouch;

function Update () {
	//Prevent false release reporting:
	released=false;
	
	//Touch checking:
	var fingers : int = 0;
	for (var touch : iPhoneTouch in iPhoneInput.touches) {
		if (touch.phase != iPhoneTouchPhase.Ended && touch.phase != iPhoneTouchPhase.Canceled){
			fingers++;
			touchInfo = touch;
		}
		if(touch.phase == iPhoneTouchPhase.Ended && fingers==0){
			released=true;
			lastPosition=touch.position;
		}
	}
	
	//Handle touches:
	if(fingers > 0){
		//Keep track of total touches:
		count=fingers;
		//Fullscreen checking:
		if(count==1){
			if(touchInfo.phase == iPhoneTouchPhase.Began){
				touched=true;
				position=touchInfo.position;
				
				//clear lastPosition:
				lastPosition=new Vector2();
			}else{
				touched=false;	
			}
		}
		
	}else{
		position = new Vector2();
		count=0;
	}
}


static function touchedIn(area : Rect):boolean{
	if(area.Contains(position) && touched){
		return true;
	}else{
		return false;	
	}
}

static function releasedIn(area : Rect):boolean{
	if(area.Contains(lastPosition) && released){
		return true;
	}else{
		return false;	
	}
}