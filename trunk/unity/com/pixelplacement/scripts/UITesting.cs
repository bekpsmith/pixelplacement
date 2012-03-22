using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UITesting : MonoBehaviour {
	
	public GUISkin skin;
	public Vector2 button1Pos, button2Pos;
	public int defaultScreenWidth = 1024;
	public int defaultScreenHeight = 768;
	
	Matrix4x4 GUIMatrix(){
		return Matrix4x4.Scale( new Vector3( (Screen.width*1f)/defaultScreenWidth, (Screen.width*1f)/defaultScreenWidth, 1 ) );
	}
	
	void OnGUI(){
		GUI.skin = skin;
		GUI.matrix = GUIMatrix();
	}
	
} 