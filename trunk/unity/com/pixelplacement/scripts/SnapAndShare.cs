using UnityEngine;
using System.Collections;
using System.IO;

public class SnapAndShare : MonoBehaviour {
	
	int screenWidth;
	int screenHeight;
	Texture2D screenShotImage;
	string screenShotFile;
	bool fileSaved;
	byte[] bytes;
	
	void Start(){
		screenShotFile = Application.persistentDataPath + "/ScreenShot.png";	
		screenShotImage = new Texture2D( 0, 0, TextureFormat.RGB24, false );
	}
	
	public IEnumerator TakeScreenShot()
	{	
		yield return new WaitForEndOfFrame();
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		screenShotImage.Resize( screenWidth, screenHeight );
		screenShotImage.ReadPixels( new Rect(0,0,screenWidth,screenHeight), 0, 0 );
		screenShotImage.Apply();
		bytes = screenShotImage.EncodeToPNG();
		if ( File.Exists( screenShotFile ) ) {
			File.Delete( screenShotFile );
		}
		File.WriteAllBytes( screenShotFile, bytes );
		fileSaved = false;
		while ( !fileSaved ) {
			if ( File.Exists( screenShotFile ) ) {
				fileSaved = true;
				yield return null;
			}
		}
		EtceteraBinding.saveImageToPhotoAlbum( screenShotFile );
	}
	
	void OnGUI(){
		if ( GUILayout.Button( "SnapShot") ) {
			StartCoroutine( TakeScreenShot() );
		}
		GUILayout.Label( screenShotImage );	
	}
}
