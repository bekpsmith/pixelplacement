using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ExplorationSegment))]
public class ExplorationSegmentEditor : Editor
{
	ExplorationSegment script;
	
	void OnEnable(){
		script = (ExplorationSegment)target;	
	}
	
	public override void OnInspectorGUI () {
		script.position = EditorGUILayout.Vector3Field("Position 1: ",script.position);
		script.position2 = EditorGUILayout.Vector3Field("Position 2: ",script.position2);
		script.position3 = EditorGUILayout.Vector3Field("Position 3: ",script.position3);
		if(GUI.changed){
			EditorUtility.SetDirty(script);
		}
    }

    void OnSceneGUI () {
		Handles.color = Color.cyan;
		
		script.position = Handles.PositionHandle(script.position,Quaternion.identity);
		Handles.Label(script.position,"Point 1");
		
		script.position2 = Handles.PositionHandle(script.position2,Quaternion.identity);
		Handles.Label(script.position2,"Point 2");
		
		script.position3 = Handles.PositionHandle(script.position3,Quaternion.identity);
		Handles.Label(script.position3,"Point 3");
		
		iTween.DrawPathHandles(new Vector3[]{script.position,script.position2,script.position3});
		
		if(GUI.changed){
			EditorUtility.SetDirty(script);
		}
    }
}