using UnityEngine;
using System.Collections;


[AddComponentMenu("Bully/ScreenQuad")]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ScreenQuad : MonoBehaviour
{
	protected string shader = "Shader \"QuadGUI\" { Properties { _MainTex (\"Base (RGB)\", 2D) = \"white\" } SubShader { Pass { SetTexture [_MainTex] } } }";
	Mesh mesh;
	public Rect position = new Rect(0,0,100,100);
	Rect prevPosition;
	public Camera renderCamera;
	public Texture2D img;
	public float distanceFromCamera;
	float prevDistanceFromCamera;
		
	void Awake(){
		//grab camera reference:
		if ( renderCamera == null ) {
			renderCamera = Camera.main;
		}
		
		//parent:
		if ( transform.parent != renderCamera) {
			transform.parent = renderCamera.transform;
		}
		
		//correct distance from camera:
		distanceFromCamera = Mathf.Abs(distanceFromCamera);
		if ( distanceFromCamera == 0) {
			distanceFromCamera = 100;
		}
		transform.localPosition = Vector3.zero;
		
		//build display geometry:
		mesh = gameObject.GetComponent<MeshFilter>().mesh;
		mesh.vertices = CalculateVerts();
		mesh.uv = new Vector2[] {new Vector2(0,0), new Vector2(1,0), new Vector2(0,1), new Vector2(1,1)};
		mesh.triangles = new int[] {0,1,2,1,3,2};	
		
		//setup material:
		renderer.material = new Material(shader);
		renderer.material.mainTexture = img; //temp - put this control in subclasses
	}
	
	Vector3[] CalculateVerts(){		
		Vector3 upperLeft = renderCamera.ScreenToWorldPoint( new Vector3( position.x, position.y, distanceFromCamera) );
		Vector3 upperRight = renderCamera.ScreenToWorldPoint( new Vector3( position.x + position.width, position.y, distanceFromCamera) );
		Vector3 lowerLeft = renderCamera.ScreenToWorldPoint( new Vector3( position.x, position.y + position.height, distanceFromCamera) );
		Vector3 lowerRight = renderCamera.ScreenToWorldPoint( new Vector3( position.x + position.width, position.y + position.height, distanceFromCamera) );
		
		Vector3[] verticies = new Vector3[]{upperLeft, upperRight, lowerLeft, lowerRight};		
		return verticies;
	}
	
	void Update(){
		if ( position == prevPosition && distanceFromCamera == prevDistanceFromCamera ) {
			return;
		}
		
		mesh.vertices = CalculateVerts();
		prevPosition = position;	
	}
	
}