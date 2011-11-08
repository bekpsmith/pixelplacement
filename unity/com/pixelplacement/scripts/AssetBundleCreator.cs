using UnityEngine;
using UnityEditor;

public class AssetBundleCreator
{
	[MenuItem ("Bully!/Mobile Scene Asset Bundle from Selected")]
	static void BuildAssetBundle()
	{
		string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		
		//make sure user has a scene selected:
		if ( Selection.objects.Length != 1 || !assetPath.Contains(".unity") ) {
			EditorUtility.DisplayDialog( "Scene Asset Bundle Creation", "Please select a single scene in your project.", "OK" );
			return;
		}
		
		//set a save location for the bundle:
		string savePath = EditorUtility.SaveFilePanel ("Save Scene Asset Bundle", "", "", "unity3d");
		if ( savePath == "") {
			return;
		}
		
		//save asset bundles for iphone and android:
		string[] pathPieces = savePath.Split('.');
		BuildPipeline.BuildStreamedSceneAssetBundle(new string[]{assetPath}, pathPieces[0]+"_iPhone."+pathPieces[1], BuildTarget.iPhone);
		if ( PlayerSettings.Android.licenseVerification ) {
			BuildPipeline.BuildStreamedSceneAssetBundle(new string[]{assetPath}, pathPieces[0]+"_Android"+pathPieces[1], BuildTarget.Android);
		}
		
		//complete:
		EditorApplication.Beep();
		EditorUtility.DisplayDialog( "Scene Asset Bundle Creation", "Done!", "OK" );
	}
}

