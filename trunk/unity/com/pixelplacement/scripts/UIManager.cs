using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour{
	//image loads:
	enum Images {};
	
	#region utility
	//properties:
	private static System.Action UI;
	GUIStyle blankStyle = new GUIStyle();	
	string sdTexturePath = "sd", hdTexturePath = "hd", loadPath;	
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
		
		//populate image assets:
		foreach(Images imageAssetName in System.Enum.GetValues(typeof(Images))){
			imageAssets.Add(imageAssetName, LoadTexture(imageAssetName.ToString())); 
		}
		#endregion utility
		
		//initial ui layers:
		//AddUILayer(HeaderLayer);
	}	
	
	#region utility
	void OnGUI(){
		if(UI != null){
			UI(); 
		}
	}
	
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
	#endregion utility
	
	//ui layers:
}