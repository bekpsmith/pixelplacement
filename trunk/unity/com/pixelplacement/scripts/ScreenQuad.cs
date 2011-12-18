using UnityEngine;
using System.Collections;

public enum Pivot{ UpperLeft, UpperMiddle, UpperRight, Left, Middle, Right, LowerLeft, LowerMiddle, LowerRight };

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public abstract class ScreenQuad : MonoBehaviour {

	public Camera renderCamera;
	public Pivot pivot;
	public Vector2 screenPosition;
	public float width = 100;
	public float height = 100;
	public float distance = 10;
	
	Vector2 prevScreenPosition;
	Vector3 prevPosition;
	float prevDistance;
	float prevHeight;
	Pivot prevPivot;
	float prevWidth;

	string shader = "Shader \"ScreenQuad\" {Properties { _Color (\"Main Color\", Color) = (1,1,1,1) _MainTex (\"Base (RGB) Trans (A)\", 2D) = \"white\" {} } Category { Tags { \"Queue\"=\"Transparent\" \"RenderType\"=\"Transparent\"} Blend SrcAlpha OneMinusSrcAlpha SubShader { Pass { Lighting Off SetTexture [_MainTex] { constantColor [_Color] Combine texture * constant, texture * constant } } } } }";
	Vector2 screenDimensions;
	Transform _transform;
	Vector3[] verticies;
	Mesh mesh;
	
	public virtual void Awake(){
		renderer.material = new Material(shader);
		_transform = transform;
		screenDimensions = new Vector2( Screen.width, Screen.height );
		
		//grab camera reference:
		if ( renderCamera == null ) {
			renderCamera = Camera.main;
		}
		
		//parent:
		if ( transform.parent != renderCamera) {
			transform.parent = renderCamera.transform;
		}
		
		//build geometry:
		mesh = gameObject.GetComponent<MeshFilter>().mesh;
		CalculateVerticies();
		
		mesh.uv = new Vector2[] { new Vector2(-1,0), new Vector2(0,0), new Vector2(0,1), new Vector2(-1,1) };
		mesh.triangles = new int[] {0,2,1,0,3,2};
	}
	
	public virtual void Update(){
		//only update geometry if parameters have changed:
		if ( Screen.width != screenDimensions.x || Screen.height != screenDimensions.y || pivot != prevPivot || screenPosition != prevScreenPosition || width != prevWidth || height != prevHeight || distance != prevDistance ) {
			CalculateVerticies();
		}	
		
		//if transform is adjusted recalculate screenPosition and distance:
		if ( _transform.position != prevPosition ) {
			//TODO: if the transform is moved along Z this will cause the object to break from the screen position and drift down screen
			distance = _transform.localPosition.z;
			Vector3 screenPos = renderCamera.WorldToScreenPoint( _transform.position );
			screenPosition = prevScreenPosition = new Vector2( screenPos.x, screenDimensions.y- screenPos.y );
		}
	}
	
	void CalculateVerticies(){
		Vector3 upperLeft = new Vector3();
		Vector3 upperRight = new Vector3();
		Vector3 lowerRight = new Vector3();
		Vector3 lowerLeft = new Vector3();
		Quaternion prevRotation = _transform.rotation;
		
		//transform adjustments:
		_transform.rotation = Quaternion.identity;
		_transform.position = renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y, distance ) );
		prevPosition = _transform.position;

		switch (pivot) {
		
		case Pivot.UpperLeft:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y-height, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y-height, distance ) ) );
			break;
		
		case Pivot.UpperMiddle:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y-height, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y-height, distance ) ) );
			break;	
			
		case Pivot.UpperRight:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y-height, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y-height, distance ) ) );
			break;
			
		case Pivot.Left:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			break;
			
		case Pivot.Middle:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			break;
		
		case Pivot.Right:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y+height/2, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y-height/2, distance ) ) );
			break;
			
		case Pivot.LowerLeft:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y+height, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y+height, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y, distance ) ) );
			break;
			
		case Pivot.LowerMiddle:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y+height, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y+height, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x + width/2, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width/2, screenDimensions.y-screenPosition.y, distance ) ) );
			break;
		
		case Pivot.LowerRight:
			upperLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y+height, distance ) ) );
			upperRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y+height, distance ) ) );
			lowerRight = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x, screenDimensions.y-screenPosition.y, distance ) ) );
			lowerLeft = _transform.InverseTransformPoint( renderCamera.ScreenToWorldPoint( new Vector3( screenPosition.x - width, screenDimensions.y-screenPosition.y, distance ) ) );
			break;
		}
		
		//rest rotation:
		_transform.rotation = prevRotation;
		
		//update verticies:
		mesh.vertices = new Vector3[] { lowerLeft, lowerRight, upperRight, upperLeft };
		
		//catalog parameters:
		prevPivot = pivot;
		prevScreenPosition = screenPosition;
		prevWidth = width;
		prevHeight = height;
		prevDistance = distance;
		screenDimensions = new Vector2(Screen.width, Screen.height);
	}
}
