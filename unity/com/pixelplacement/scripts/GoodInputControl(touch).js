public var touchRect : Rect = new Rect(0,0,240,320);
public var status : GUIText;
private var initTouchPosition : Vector2 = Vector2(-1,-1);
private var touchPosition : Vector2 = Vector2(-1,-1);
private var touchId : int = -1;

public var s : GUITexture;
public var c : GUITexture;

function Update(){
	getTouch();
	if(touchId>-1){
		s.pixelInset.x=initTouchPosition.x;
		s.pixelInset.y=initTouchPosition.y;
		
		c.pixelInset.x=touchPosition.x;
		c.pixelInset.y=touchPosition.y;
	}
	status.text=touchId + " " + initTouchPosition + " " + touchPosition;
	
}

private function getTouch() : void{
	var i : int;
	var currentTouch : iPhoneTouch;
	
	for(i=0; i<iPhoneInput.touchCount; i++){
		currentTouch =  iPhoneInput.GetTouch(i);
		
		if(currentTouch.phase == iPhoneTouchPhase.Began && touchRect.Contains(currentTouch.position) && touchId<0){
			initTouchPosition = currentTouch.position;
			touchPosition=currentTouch.position;
			touchId=currentTouch.fingerId;
		}
		
		if(currentTouch.phase == iPhoneTouchPhase.Moved){
			if(currentTouch.fingerId == touchId){
				touchPosition=currentTouch.position;
			}else if(touchRect.Contains(currentTouch.position) && touchId<0){
				initTouchPosition = currentTouch.position;
				touchId=currentTouch.fingerId;
				touchPosition=currentTouch.position;
			}
		}
				
		if(currentTouch.phase == iPhoneTouchPhase.Ended && currentTouch.fingerId == touchId){
			touchId=-1;
			cleanUp();
		}
	}	
}

private function cleanUp(){
	print("Clean up after release")	;
	initTouchPosition=Vector2(-1,-1);
	touchPosition=Vector2(-1,-1);
}