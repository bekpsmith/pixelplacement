using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class EditorGames : EditorWindow
{
	static TimeSpan prevTime;
	static float deltaTime;
	
	static Texture2D ball;
	static float xPos = 0;
	static float yPos = 0;
	static float Xspeed = 1;
	static float Yspeed = 1;
			
	[MenuItem("Games/Ball Bounce")]
	static void Init(){
		GetWindow<EditorGames>();
	}
	
	void OnEnable(){
		ball = (Texture2D)Resources.Load("ball");
		prevTime = DateTime.Now.TimeOfDay;
	}
	
	void OnGUI(){
		GetDeltaTime();
		//calc and compare step change before setting
		xPos += Xspeed*(.2f*deltaTime);
		yPos += Yspeed*(.2f*deltaTime);
		
		if(xPos + ball.width > position.width){
			xPos = position.width - ball.width;
			Xspeed*=-1;
		}
		if(xPos < 0){
			xPos = 0;
			Xspeed*=-1;
		}
		
		if(yPos < 0){
			yPos = 0;
			Yspeed*=-1;
		}
		if(yPos + ball.height > position.height){
			yPos = position.height - ball.height;
			Yspeed*=-1;
		}
		
		GUI.DrawTexture(new Rect(xPos,yPos,ball.width,ball.height),ball);
		
		Repaint();			
	}
	
	void GetDeltaTime(){
		TimeSpan now = DateTime.Now.TimeOfDay;
		deltaTime = now.Subtract(prevTime).Milliseconds;
		prevTime = now;
	}	
}




/*
 onClipEvent (load) {
	// gravity is what I called g in the tutorial. The
	// higher g the harder the ball will fall.
	// gravity = 0 can be set, as an experiment, but 
	// it will in fact create a "zero gravity" effect
	// gravity < 0 will create an inverted gravity effect
	gravity = 2 ;

	// We get the time when the ball is released for
	// the first time
	time = getTimer () ;
	time /= 100 ;
	
	// This sets the _y position of the floor
	floor = 500 ;
	
	// Bounce is a number < 1 but close to 1
	// The closer to 1, the higher the ball will bounce 
	bounce = 0.92 ;
	
	// We set the speed of the ball when it is released.
	speedx = 0 ;
	speedystart = 0 ;
}

onClipEvent (enterFrame) {

	// We calculate the increase of speed
	// speedx doesn't change
	timenow = getTimer () ;
	timenow /= 100 ;
	speedy = gravity * (timenow - time) + speedystart ;
	
	//We move the ball
	this._x += speedx/5 ;
	this._y += speedy/5 ;
	
	if (this._y > floor) {
		this._y = floor ;
		speedy *= -bounce ;
		time = getTimer () ;
		time /= 100 ;
		speedystart = speedy ;
	}
}
*/