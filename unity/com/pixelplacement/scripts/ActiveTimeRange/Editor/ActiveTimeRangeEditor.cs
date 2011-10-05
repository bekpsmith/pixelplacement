using UnityEngine;
using UnityEditor;
using System.Collections;

public enum Meridiem{ AM, PM };
	
[CustomEditor(typeof(ActiveTimeRange))]
public class ActiveTimeRangeEditor : Editor{
	ActiveTimeRange _target;
	
	void OnEnable(){
		_target = (ActiveTimeRange)target;
	}
		
	public override void OnInspectorGUI (){
		_target.Calculate();
		
		//ui:
		EditorGUILayout.LabelField("Start Time:", _target.startTime.ToString("hh:mm tt"));
        EditorGUILayout.LabelField("End Time:", _target.endTime.ToString("hh:mm tt"));
		EditorGUILayout.LabelField("Duration:", _target.duration.TotalHours.ToString() + (_target.duration.TotalHours == 1 ? " hour" : " hours"));
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Active:");
		EditorGUILayout.Toggle(_target.status);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Active Range:");
		EditorGUILayout.MinMaxSlider(ref _target.minValue, ref _target.maxValue, _target.minLimit, _target.maxLimit);
		EditorGUILayout.EndHorizontal();
	}
}