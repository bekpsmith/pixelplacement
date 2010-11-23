using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;

public class DocSearch : EditorWindow{
	string searchURL = "http://unity3d.com/support/documentation/ScriptReference/30_search.html?q=";
	string term = "";
	int recentID = -1;
	bool showRecent = false;
	Vector2 recentScroll;
	List<string> recentTerms = new List<string>();
	bool clearSearchBox = false;
	string historyFile = "/Pixelplacement/DocSearch/Editor/history.txt";
	
	[MenuItem("Pixelplacement/Doc Search")]
	public static void LaunchWindow(){
		GetWindow(typeof(DocSearch),false,"Doc Search");
	}
	
	void OnEnable(){
		if(File.Exists(Application.dataPath + historyFile)){
			StreamReader sr = new StreamReader(Application.dataPath + historyFile);
			string history = sr.ReadToEnd();
			string[] terms = history.Split(',');
			for (int i = 0; i < terms.Length-1; i++) {
				recentTerms.Add(terms[i]);
			}
			sr.Close();
		}
	}
	
	void OnDisable(){
		StreamWriter sw = new StreamWriter(Application.dataPath + historyFile,false);	
			foreach (string a in recentTerms) {
				sw.Write(a + ",");
			}
			sw.Close();
		AssetDatabase.Refresh();
	}
	
	void OnGUI(){
		SearchBox();		
		ShowRecent();	
	}
	
	void SearchBox(){
		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		
		if(clearSearchBox){
			term = EditorGUILayout.TextField("");
			clearSearchBox = false;
		}else{
			term = EditorGUILayout.TextField(term);	
		}
		
		GUI.SetNextControlName ("searchButton");
		if(GUILayout.Button("Search") && term !=""){
			clearSearchBox=true;
			GUI.FocusControl ("searchButton"); 
			DoSearch(term,true);
		}
		
		Event e = Event.current;
		if(e.isKey && e.keyCode == KeyCode.Return && term !=""){
			clearSearchBox=true;
			GUI.FocusControl ("searchButton"); 
			DoSearch(term,true);
			e.Use();
		}
		
		GUILayout.EndHorizontal();
	}
		
	void ShowRecent(){
		if(recentTerms.Count > 0){
			showRecent = EditorGUILayout.Foldout(showRecent,"Recent Searches");
			if(showRecent){
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				recentScroll = EditorGUILayout.BeginScrollView(recentScroll);
				recentID = GUILayout.SelectionGrid(recentID,recentTerms.ToArray(),1);
				EditorGUILayout.EndScrollView();
				EditorGUILayout.Space();
				EditorGUILayout.EndHorizontal();
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
		if(addToRecentList && recentTerms.IndexOf(term.ToLower()) == -1){
			recentTerms.Reverse();
			recentTerms.Add(term.ToLower());
			recentTerms.Reverse();	
		}
	}

	void ClearRecent(){
		EditorGUILayout.Space();
		if(GUILayout.Button("Clear Recent Searches")){
			recentTerms=new List<string>();
		}	
	}
}

