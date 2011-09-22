using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScreenManager : MonoBehaviour {
	public float fadeSpeed = 5;
	float guiAlpha = 0;
	Color guiColor = Color.white;
	int screenCount=0;
	System.Action MainGUI;
	System.Action[] screens;
		
	void Awake() {		
		//setup screens array:
		screens = new System.Action[] { FirstScreen, SecondScreen, ThirdScreen, FourthScreen };
		SwapGUI(0);
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
		
	//***********
	//* Screens *
	//***********
	
	void FirstScreen() {
		if (GUILayout.Button("One",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("1 pressed");
		}
	}
	
	void SecondScreen() {
		if (GUILayout.Button("Two!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("2 pressed");
		}
	}	
	
	void ThirdScreen() {
		if (GUILayout.Button("Three!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("3 pressed");
		}
	}
	
	void FourthScreen() {
		if (GUILayout.Button("Four!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("4 pressed");
		}
	}
	
	//************
	//* Controls *
	//************
	
	void ControlPresentation(Swipe swipeDirection) {
		switch (swipeDirection) {
			
		case Swipe.Right:
			screenCount = Mathf.Max(--screenCount,0);
			SwapGUI(screenCount);
			break;
			
		case Swipe.Left:
			screenCount = Mathf.Min(++screenCount,screens.Length-1);
			SwapGUI(screenCount);
			break;
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
		SwipeDetection.OnSwipeDetected -= ControlPresentation;
		KeyDetection.OnKeyDetected -= ControlPresentation;
		
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
		SwipeDetection.OnSwipeDetected += ControlPresentation;
		KeyDetection.OnKeyDetected += ControlPresentation;
	}
}
