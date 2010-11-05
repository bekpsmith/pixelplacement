using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DocumentationSearch : EditorWindow{
	string searchURL = "http://unity3d.com/support/documentation/ScriptReference/30_search.html?q=";
	string term = "";
	int recentID = -1;
	bool showRecent = false;
	Vector2 recentScroll;
	List<string> recentTerms = new List<string>();
	bool clearSearchBox = false;
	
	[MenuItem("Window/Doc Search")]
	public static void LaunchWindow(){
		GetWindow<DocumentationSearch>(false,"Documentation");
	}
	
	void OnGUI(){
		SearchBox();		
		ShowRecent();	
	}
	
	void SearchBox(){
		GUILayout.BeginHorizontal();
		if(clearSearchBox){
			term = EditorGUILayout.TextField("");
			clearSearchBox = false;
		}else{
			term = EditorGUILayout.TextField(term);	
		}
		
		if(GUILayout.Button("Search") && term !=""){
			DoSearch(term,true);
		}
		GUILayout.EndHorizontal();
	}
		
	void ShowRecent(){
		if(recentTerms.Count > 0){
			showRecent = EditorGUILayout.Foldout(showRecent,"Recent Searches");
			if(showRecent){
				recentScroll = EditorGUILayout.BeginScrollView(recentScroll);
				recentID = GUILayout.SelectionGrid(recentID,recentTerms.ToArray(),1);
				EditorGUILayout.EndScrollView();
				if(recentID != -1){
					DoSearch(recentTerms.ToArray()[recentID],false);
					recentID=-1;
				}
				ClearRecent();
			}
		}
	}
	
	void DoSearch(string term, bool addToRecentList){
		Application.OpenURL (searchURL + term);
		if(addToRecentList && recentTerms.IndexOf(term) == -1){
			recentTerms.Reverse();
			recentTerms.Add(term);
			recentTerms.Reverse();	
			clearSearchBox=true;
		}
	}

	void ClearRecent(){
		EditorGUILayout.Separator();
		if(GUILayout.Button("Clear Recent Searches")){
			recentTerms=new List<string>();
		}	
	}
}

