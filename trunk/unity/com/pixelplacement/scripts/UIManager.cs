using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour{
	//images:
	enum Images {menuBackground, newsButton, header, arInstructions};
	
	//properties:
	private static System.Action UI;
	GUIStyle blankStyle = new GUIStyle();	
	string sdTexturePath = "sd", hdTexturePath = "hd", loadPath;	
	int cutoffResolutionCombination = 800;
	Texture2D blank;
	Dictionary<Images, Texture2D> imageAssets = new Dictionary<Images, Texture2D>();
	Texture2D currentTexture;
	public Rect headerContent;
	public Rect menuContent;
	
	void Awake(){
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
		
		//gui content groups:
		headerContent = new Rect( (Screen.width/2) - (GetImage(Images.header).width/2), 0, GetImage(Images.header).width, GetImage(Images.header).height);
		menuContent = new Rect(0, Screen.height-GetImage(Images.menuBackground).height, Screen.width, GetImage(Images.menuBackground).height);
		
		//listeners:
		TrackerBehaviour.OnMarkersFound += HideGUI;
		TrackerBehaviour.OnMarkersLost += ShowGUI;
		
		//initial ui layers:
		AddUILayer(HeaderLayer);
		AddUILayer(MainMenuLayer);
		AddUILayer(InstructionsLayer);
	}
	
	void HideGUI(){
		RemoveUILayer(InstructionsLayer);
		iTween.ValueTo(gameObject, iTween.Hash("from", headerContent.y, "to", -GetImage(Images.header).height, " time", .3f, "easeType", iTween.EaseType.easeOutCubic, "onUpdate", "SlideHeader"));
		//iTween.ValueTo(gameObject, iTween.Hash("from", menuContent.y, "to", Screen.height+GetImage(Images.menuBackground).height, " time", .3f, "easeType", iTween.EaseType.easeOutCubic, "onUpdate", "SlideMenu"));
	}
	
	void ShowGUI(){
		AddUILayer(InstructionsLayer);
		iTween.ValueTo(gameObject, iTween.Hash("from", headerContent.y, "to", 0, " time", .3f, "easeType", iTween.EaseType.easeInCubic, "onUpdate", "SlideHeader"));
		//iTween.ValueTo(gameObject, iTween.Hash("from", menuContent.y, "to", Screen.height-GetImage(Images.menuBackground).height, " time", .3f, "easeType", iTween.EaseType.easeInCubic, "onUpdate", "SlideMenu"));
	}
	
	void SlideHeader(float val){
		headerContent.y = val;
	}
	
	void SlideMenu(float val){
		menuContent.y = val;
	}
	
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
	
	void InstructionsLayer(){
		GUI.DrawTexture(new Rect((Screen.width/2) - (GetImage(Images.arInstructions).width/2), (Screen.height/2) - (GetImage(Images.arInstructions).height/2), GetImage(Images.arInstructions).width, GetImage(Images.arInstructions).height), GetImage(Images.arInstructions));	
	}
	
	void HeaderLayer(){
		currentTexture = GetImage(Images.header);
		GUI.BeginGroup(headerContent);
		GUI.DrawTexture(new Rect(0,0,currentTexture.width,currentTexture.height), currentTexture);
		GUI.EndGroup();
	}
	
	void MainMenuLayer(){
		currentTexture = GetImage(Images.menuBackground);
		
		GUI.BeginGroup(menuContent);
		GUI.DrawTexture(new Rect(0,0,Screen.width,currentTexture.height), currentTexture);
		
		currentTexture = GetImage(Images.newsButton);
		
		if (GUI.Button(new Rect((Screen.width/2) - (currentTexture.width/2),0,currentTexture.width, currentTexture.height), currentTexture, blankStyle)) {
			NativeToolkitBinding.activateUIWithController( "NewsViewController" );
		}
		
		GUI.EndGroup();
	}
}