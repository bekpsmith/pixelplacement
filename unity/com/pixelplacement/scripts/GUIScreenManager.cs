//For the record, it should be MAR and VAR. (show gyro shooting - need to add next and previous controls to gyro)
//Tools used slide?
//add timer at bottom of screen

// add fade to black fade from balck transition betwen scenes

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScreenManager : MonoBehaviour {
	public float fadeSpeed = 5;
	public static int currentScreenID = 0;
	float guiAlpha = 0;
	Color guiColor = Color.white;
	System.Action MainGUI;
	System.Action[] screens;
	
	public Texture2D[] screenImages;
	
	void Start() {				
		//setup screens array:
		screens = new System.Action[] { FirstScreen, SecondScreen, ThirdScreen, FourthScreen };
		SwapGUI(currentScreenID);
	}
		
	void OnGUI() {
		if (MainGUI == null) {
			return;
		}
		
		//set gui's alpha
		guiColor.a = guiAlpha;
		GUI.color = guiColor;
		
		//run gui screens:
		MainGUI();	
	}
	
	void OnDisable(){
		//make sure we kill events that might be hanging aorund as we swap scenes:
		UserControl(false);	
	}
		
	//***********
	//* Screens *
	//***********
	
	void FirstScreen() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenImages[0]);
	}
	
	void SecondScreen() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenImages[1]);
		if (GUI.Button(new Rect(0, 0, 100, 100), "MAR")) {
			Application.LoadLevel("GyroGaming");
		}
	}	
	
	void ThirdScreen() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenImages[2]);
	}
	
	void FourthScreen() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), screenImages[3]);
		if (GUI.Button(new Rect(425, 276, 100, 100), "Play")) {
			iPhoneUtils.PlayMovie("FordFocusInteractive4.mp4", Color.black, iPhoneMovieControlMode.Minimal);
		}
	}
	
	//************
	//* Controls *
	//************
	
	void ControlPresentationSwipe(Swipe swipeDirection) {
		switch (swipeDirection) {
			
		case Swipe.Right:
			currentScreenID = Mathf.Max(--currentScreenID,0);
			SwapGUI(currentScreenID);
			break;
			
		case Swipe.Left:
			currentScreenID = Mathf.Min(++currentScreenID,screens.Length-1);
			SwapGUI(currentScreenID);
			break;
		}		
	}
	
	//only present for in-editor debug:
	void ControlPresentationKey(Swipe swipeDirection) {
		switch (swipeDirection) {
			
		case Swipe.Left:
			currentScreenID = Mathf.Max(--currentScreenID,0);
			SwapGUI(currentScreenID);
			break;
			
		case Swipe.Right:
			currentScreenID = Mathf.Min(++currentScreenID,screens.Length-1);
			SwapGUI(currentScreenID);
			break;
		}		
	}	
	
	void UserControl(bool on){
		if (on) {
			SwipeDetection.OnSwipeDetected += ControlPresentationSwipe;
			KeyDetection.OnKeyDetected += ControlPresentationKey;
		}else{
			SwipeDetection.OnSwipeDetected -= ControlPresentationSwipe;
			KeyDetection.OnKeyDetected -= ControlPresentationKey;	
		}
	}
	
	//*********
	//* Tools *
	//*********
	
	void SwapGUI(int nextScreenID){
		System.Action nextScreen = screens[nextScreenID];
		StartCoroutine(DoSwapGUI(nextScreen));
	}
	
	IEnumerator DoSwapGUI(System.Action nextScreen) {
		
		//turn off user control:
		UserControl(false);
		
		//remove previous screen (if we have any):
		if (MainGUI != null) {
			//fade out:
			while (guiAlpha>0) {
				guiAlpha -= fadeSpeed * Time.deltaTime;
				yield return null;
			}
			
			//clamp alpha:
			guiAlpha = 0;
			
			//clear the gui's invocation list:
			System.Delegate[] invocationList = MainGUI.GetInvocationList();
			foreach (System.Delegate item in invocationList) {
				MainGUI -= (System.Action)item;
			}			
		}
		
		//add new screen:
		MainGUI += nextScreen;
		
		//fade in:
		while (guiAlpha<1) {
			guiAlpha += fadeSpeed * Time.deltaTime;
			yield return null;
		}
		
		//clamp alpha:
		guiAlpha = 1;
		
		//return user control:
		UserControl(true);
	}
}
