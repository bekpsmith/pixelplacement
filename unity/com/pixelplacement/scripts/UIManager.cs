using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour{
	//image loads:
	enum Images {};
	
	#region utility
	//properties:
	GUISkin blankSkin;
	private static System.Action UI;
	string sdTexturePath = "SD", hdTexturePath = "HD", loadPath;	
	int cutoffResolutionCombination = 800;
	Dictionary<Images, Texture2D> imageAssets = new Dictionary<Images, Texture2D>();
	Texture2D currentTexture;
	#endregion utility
	
	void Awake(){		
		#region utility
		iTween.Init(gameObject);
		
		//turn off gui layout:
		useGUILayout=false;
		
		//set images resolution:
		if (Screen.width + Screen.height <= cutoffResolutionCombination) {
			loadPath=sdTexturePath+"/";
		}else{
			loadPath=hdTexturePath+"/";	
		}
		
		//populate assets:
		foreach(Images imageAssetName in System.Enum.GetValues(typeof(Images))){
			imageAssets.Add(imageAssetName, LoadTexture(imageAssetName.ToString())); 
		}
		
		blankSkin = new GUISkin();
		#endregion utility
	}	
	
	void Start(){
		//initial ui layers:
		AddUILayer(TestLayer);
	}
		
	
	void OnGUI(){
		#region utility
		if(UI != null){
			UI(); 
		}
		#endregion utility
	}
	
	#region utility
	public static void AddUILayer(System.Action layer){
		RemoveUILayer(layer); //duplicate layer fault tolerance
		UI+=layer;
	}
	
	public static void RemoveUILayer(System.Action layer){
		UI-=layer;
	}
	
	public static void ClearUI(){
		foreach (System.Action item in UI.GetInvocationList()) {
			UI-=item;
		}
	}
	
	Texture2D GetImage(Images image){
		return imageAssets[image];
	}
	
	Texture2D LoadTexture(string image){
		return (Texture2D)Resources.Load(loadPath + image);
	}
	
	bool ImageButton(float xpos, float ypos, Images image){
		Texture2D imageData = GetImage(image);
		if (GUI.Button(xpos,ypos,imageData.width,imageData.height)) {
			return true;
		}else{
			return false;	
		}
	}
	#endregion utility
	
	//ui layers:
	void TestLayer(){
		GUI.skin = blankSkin;
		GUI.Button(new Rect(0,0,200,200), "Ass");
	}
}