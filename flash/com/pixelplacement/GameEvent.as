﻿package {	import flash.events.Event;	public class GameEvent extends Event {		public var value:String;		public static const DEATH:String="death"		public function GameEvent(type:String,value:String=null):void {			var bubbles:Boolean=true;			var cancelable:Boolean=false;			super(type,bubbles,cancelable);			this.value=value;		}	}}