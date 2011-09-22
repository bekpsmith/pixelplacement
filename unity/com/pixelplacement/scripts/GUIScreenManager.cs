using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScreenManager : MonoBehaviour {
	float guiAlpha = 1, fadeSpeed = 6;
	Color guiColor = Color.white;
	int screenCount=0;
	System.Action MainGUI;
	System.Action[] screens;
	
	void Start() {
		SwipeDetection.OnSwipeDetected += ControlPresentation;
		//NextGUI(FirstButton);
		
		//setup screens array:
		screens = new System.Action[] { FirstScreen, SecondScreen, ThirdScreen, FourthScreen };
		MainGUI += screens[screenCount];
	}
	
	void ControlPresentation(Swipe swipeDirection) {
		switch (swipeDirection) {
			
		case Swipe.Left:
			//reduce count and transition to previous gui:
			screenCount = Mathf.Max(--screenCount,0);
			StartCoroutine(DoSwapGUI(screens[screenCount]));
			break;
			
		case Swipe.Right:
			//increment count and transition to next gui:
			screenCount = Mathf.Min(++screenCount,screens.Length-1);
			StartCoroutine(DoSwapGUI(screens[screenCount]));
			break;
		}
	}
	
	void OnGUI() {
		if (MainGUI != null) {
			
			//set gui's alpha
			guiColor.a = guiAlpha;
			GUI.color = guiColor;
			
			//run gui screens:
			MainGUI();	
		}	
	}
		
	//***********
	//* Screens *
	//***********
	
	void FirstScreen() {
		if (GUILayout.Button("One",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("1");
		}
	}
	
	void SecondScreen() {
		if (GUILayout.Button("Two!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("penis 2");
		}
	}	
	
	void ThirdScreen() {
		if (GUILayout.Button("Three!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("3 vagina");
		}
	}
	
	void FourthScreen() {
		if (GUILayout.Button("Four!",GUILayout.Width(100),GUILayout.Height(100))) {
			Debug.Log("4 cooks");
		}
	}
	
	//********************
	//* Transition tools *
	//********************
		
	IEnumerator DoSwapGUI(System.Action nextScreen) {
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
	}
}
