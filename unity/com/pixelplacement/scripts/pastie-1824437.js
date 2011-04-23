// PRE-processes all textures that are placed in a folder 
class MakeGUIImage extends AssetPostprocessor
{
    function OnPreprocessTexture () {
        if (assetPath.Contains("Image Assets")) {
            var myTextureImporter : TextureImporter = assetImporter;
            myTextureImporter.textureType = TextureImporterType.Advanced;
            myTextureImporter.textureFormat = TextureImporterFormat.ARGB32;
            myTextureImporter.convertToNormalmap = false;
            myTextureImporter.maxTextureSize = 1024;
            myTextureImporter.grayscaleToAlpha = false;
            myTextureImporter.generateCubemap = TextureImporterGenerateCubemap.None;
            myTextureImporter.npotScale = TextureImporterNPOTScale.None;
            myTextureImporter.isReadable = true;
            myTextureImporter.mipmapEnabled = false;
//            myTextureImporter.borderMipmap = false;
//            myTextureImporter.correctGamma = false;
            myTextureImporter.mipmapFilter = TextureImporterMipFilter.BoxFilter;
            myTextureImporter.fadeout = false;
//            myTextureImporter.mipmapFadeDistanceStart;
//            myTextureImporter.mipmapFadeDistanceEnd;
            myTextureImporter.convertToNormalmap = false;
//            myTextureImporter.normalmap;
//            myTextureImporter.normalmapFilter;
//            myTextureImporter.heightmapScale;
            myTextureImporter.lightmap = false;
            
        }
		
    }
}