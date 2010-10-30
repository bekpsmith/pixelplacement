using UnityEngine;
using System.Collections;

public class GUIUtils : MonoBehaviour
{
	public static Vector2 DefaultPlayerSize = new Vector2(1280f,800f);
		
	public static Matrix4x4 GUIMatrix{
        get{
			return(Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(GUIRatio.x, GUIRatio.y, 1)));
		}
    }
	
	public static Vector2 GUIRatio{
		get{
			float horzRatio = Screen.width/DefaultPlayerSize.x;
			float vertRatio = Screen.height/DefaultPlayerSize.y;
			return(new Vector2(horzRatio,vertRatio));
		}
	}
}

