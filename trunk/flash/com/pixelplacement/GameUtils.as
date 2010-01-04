﻿package com.pixelplacement{
	import flash.utils.getQualifiedClassName;
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.display.Sprite;
	import flash.display.DisplayObject;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.utils.getTimer;

	public class GameUtils {
		//Private properties:
		private static var runningClasses:Array=new Array();
		private static var gameTimer:Sprite;
		private static var fpsDisplay:Sprite;
		private static var fpsHolder:Sprite;
		private static var fpsText:TextField;
		private static var fpsTicks:uint=0;
		private static var fpsLast:uint;
		private static var prevTime:uint;

		//Public properties:
		public static var deltaTime:Number;
		
		//Starts an ENTER_FRAME that will keep track of deltaTime to allow FRIA(Frame Rate Independant Animation):
		public static function startTimer():void {
			if (! gameTimer) {
				trace("GAMEUTILS: [ + ] Global game timer started; deltaTime is now available.  If leveraing deltaTime set FPS to 120.");
				gameTimer=new Sprite();
				gameTimer.addEventListener(Event.ENTER_FRAME,updateDeltaTime,false,0,true);
				prevTime=getTimer();
			}
		}

		//Stops the ENTER_FRAME that keeps track of deltaTime:
		public static function stopTimer():void {
			if (gameTimer) {
				trace("GAMEUTILS: [ - ] Global game timer stopped. deltaTime is now invalidated.");
				gameTimer.removeEventListener(Event.ENTER_FRAME,updateDeltaTime);
				gameTimer=null;
				prevTime=0;
				deltaTime=0;
			}
		}

		//Adds an FPS display:

		public static function addFPS(holder:DisplayObject):void {
			if (! fpsDisplay) {
				trace("GAMEUTILS: [ + ] FPS added.");
				fpsHolder=Sprite(holder);
				//FPS display:
				fpsDisplay=new Sprite();
				fpsDisplay.alpha=.9;
				fpsDisplay.addEventListener(Event.ENTER_FRAME,fpsTick,false,0,true);

				//FPS display text:
				fpsText=new TextField();
				fpsText.selectable=false;
				fpsText.background=true;
				fpsText.backgroundColor=0xFF0000;
				fpsText.autoSize=TextFieldAutoSize.LEFT;
				fpsDisplay.addChild(fpsText);					
			}
		}
		
		//Removes the FPS display and all associated properties and listeners:
		public static function removeFPS():void {
			if (fpsDisplay) {
				trace("GAMEUTILS: [ + ] FPS removed.");
				fpsDisplay.removeEventListener(Event.ENTER_FRAME,fpsTick);
				fpsDisplay.removeChild(fpsText);
				fpsHolder.removeChild(fpsDisplay);
				fpsText=null;
				fpsDisplay=null
			}
		}

		//Calculates FPS:
		public static function fpsTick(e:Event):void {
			fpsTicks++;
			var fpsNow:uint=getTimer();
			var fpsDelta:uint=fpsNow-fpsLast;
			if (fpsDelta>=500) {
				//Push fpsDisplay to top of holder's visual stack:
				fpsHolder.addChild(fpsDisplay);
				var fpsCalculatedValue:Number=fpsTicks/fpsDelta*1000;
				fpsText.text=fpsCalculatedValue.toFixed(1)+" fps";
				fpsTicks=0;
				fpsLast=fpsNow;
			}
		}

		//Updates deltaTime:
		private static function updateDeltaTime(e:Event):void {
			deltaTime=(getTimer()-prevTime)/1000;
			prevTime=getTimer();
		}

		//Returns a class status and keeps track of added classes:
		//Intended usage: Screen constructors.
		public static function startState(c:*):void {
			var cName:String=getQualifiedClassName(c);
			trace("GAMEUTILS: [ + ] " + cName + " started.");
			runningClasses.push(cName);
		}

		//Returns a class status and keeps track of removed classes:
		//Intended usage: Screen clean up functions.
		public static function stopState(c:*):void {
			var cName:String=getQualifiedClassName(c);
			trace("GAMEUTILS: [ - ] " + cName + " stopped.");
			var cIndex:int=runningClasses.indexOf(cName);
			if (cIndex<0) {
				trace("GAMEUTILS: [ x ] " + cName + " was not active!");
				return;
			}
			runningClasses.splice(cIndex,1);
		}

		//Returns a properly formated number (i.e. 5,000,000):
		public static function formatNumber(number:Number):String {
			var numString:String=number.toString();
			var result:String='';
			while (numString.length > 3) {
				var chunk:String=numString.substr(-3);
				numString=numString.substr(0,numString.length-3);
				result=','+chunk+result;
			}

			if (numString.length>0) {
				result=numString+result;
			}
			return result;
		}

		// Returns a random float:
		public static function randomFloat(min:Number,max:Number=NaN):Number {
			if (isNaN(max)) {
				max=min;
				min=0;
			}
			return Math.random() * (max - min) + min;
		}

		// Returns a random integer:
		public static function randomInteger(min:Number,max:Number=NaN):int {
			if (isNaN(max)) {
				max=min;
				min=0;
			}
			return Math.floor(randomFloat(min, max));
		}

		// Converts radians to degrees:
		public static function toDeg(rad:Number):Number {
			return rad/Math.PI*180;
		}

		// Converts degrees to radians:
		public static function toRad( deg:Number ):Number {
			return deg/180*Math.PI;
		}
		
		// Converts radians to degrees:
		public static function toQB(value:Number):Number {
			return value/30;
		}

		// Converts degrees to radians:
		public static function fromQB( value:Number ):Number {
			return value*30;
		}
	}
}