using UnityEditor;
using UnityEngine;

public class ImportPostProcessor : AssetPostprocessor
{	
	void OnPreprocessTexture(){
		if (assetPath.Contains("Resources") || assetPath.Contains("ArtAssets") || assetPath.Contains("StreamingAssets")) {
			TextureImporter ti = (TextureImporter)assetImporter;
			ti.wrapMode = TextureWrapMode.Clamp;
			ti.textureType = TextureImporterType.GUI;
			ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
			ti.mipmapEnabled = false;
			ti.npotScale = TextureImporterNPOTScale.None;
			UnityEngine.Debug.Log(assetPath.ToString() + " was custom processed!");
		}
	}
}