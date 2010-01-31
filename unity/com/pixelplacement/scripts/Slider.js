//calculates out a percentage from negative to positive and sends it to the linkage
public var linkage : GameObject;
public var dragBounds : Rect = new Rect();

private var activeTouchId : int;
private var touchGap : Vector2;
private var touched : boolean;
private var centerPoint : Vector2;
private var maxTravel : Vector2;
private var travelPercentage : Vector2;
private var prevTravelPercentage : Vector2 = new Vector2(0,0);

function Awake(){
	centerPoint = new Vector2(dragBounds.x+((dragBounds.width-guiTexture.pixelInset.width)/2),dragBounds.y+((dragBounds.height-guiTexture.pixelInset.height)/2));
	maxTravel = new Vector2((dragBounds.width-guiTexture.pixelInset.width)/2,(dragBounds.height-guiTexture.pixelInset.height)/2);
}

function Update () {
	checkForTouch();
	if(touched){
		drag();
		calculatePercentage();
		communicate();
	}
}

private function drag():void{
	var dragTargetPosition : Vector2 = new Vector2(iPhoneInput.touches[activeTouchId].position.x+touchGap.x,iPhoneInput.touches[activeTouchId].position.y+touchGap.y);
	
	//lock in bounds:
	if(dragTargetPosition.x<dragBounds.x){
		dragTargetPosition.x=dragBounds.x;
	}
	if(dragTargetPosition.x>dragBounds.xMax-guiTexture.pixelInset.width){
		dragTargetPosition.x=dragBounds.xMax-guiTexture.pixelInset.width;
	}
	if(dragTargetPosition.y<dragBounds.y){
		dragTargetPosition.y=dragBounds.y;
	}
	if(dragTargetPosition.y>dragBounds.yMax-guiTexture.pixelInset.height){
		dragTargetPosition.y=dragBounds.yMax-guiTexture.pixelInset.height;
	}
	
	//apply new position:
	guiTexture.pixelInset.x=dragTargetPosition.x;
	guiTexture.pixelInset.y=dragTargetPosition.y;
}

private function calculatePercentage():void{
	var currentPosition : Vector2 = new Vector2(guiTexture.pixelInset.x,guiTexture.pixelInset.y);
	var travelDistance : Vector2 = currentPosition - centerPoint;
	
	//ensure nothing is devided by 0:
	if(maxTravel.x==0){
		maxTravel.x=1;
	}
	if(maxTravel.y==0){
		maxTravel.y=1;
	}
	
	//calculate percentage of travel from mid points:
	travelPercentage = new Vector2(travelDistance.x/maxTravel.x,travelDistance.y/maxTravel.y);
}

private function checkForTouch() : void{
	var touchRect : Rect = guiTexture.GetScreenRect();
	for (var touch : iPhoneTouch in iPhoneInput.touches) {
		
		//check for and identify touch:
		if(touch.phase == iPhoneTouchPhase.Began && touchRect.Contains(touch.position)){
			activeTouchId = touch.fingerId;
			touchGap = new Vector2(guiTexture.pixelInset.x-touch.position.x,guiTexture.pixelInset.y-touch.position.y);
			prevVelocity=touch.position;
			touched=true;
			return;
		}
		
		//check for and nullify release:
		if(touch.phase == iPhoneTouchPhase.Ended && touch.fingerId==activeTouchId){
			touched=false;
			return;
		}			
	}		
}

private function communicate():void{
	//send percentages to link only if they are new: 
	if(travelPercentage != prevTravelPercentage){
		prevTravelPercentage=travelPercentage;
		linkage.SendMessage ("updateSpeedPercent", travelPercentage);
	}
}