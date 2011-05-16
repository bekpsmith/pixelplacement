using UnityEditor;
using UnityEngine;

public class ImageSequenceImportSettings : AssetPostprocessor
{	
	void OnPreprocessTexture(){
		if (assetPath.Contains("-GUI")) {
			TextureImporter ti = (TextureImporter)assetImporter;
			ti.textureType = TextureImporterType.GUI;
			ti.textureFormat = TextureImporterFormat.RGBA32;
			ti.mipmapEnabled = false;
			ti.npotScale = TextureImporterNPOTScale.None;
			UnityEngine.Debug.Log(assetPath.ToString() + " was custom processed!");
		}else if (assetPath.Contains("Sprite Atlases")) {
			TextureImporter ti = (TextureImporter)assetImporter;
			ti.textureType = TextureImporterType.Advanced;
			ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
			ti.mipmapEnabled = false;
			ti.npotScale = TextureImporterNPOTScale.ToNearest;
			UnityEngine.Debug.Log(assetPath.ToString() + " was custom processed!");
		}
	}
}

