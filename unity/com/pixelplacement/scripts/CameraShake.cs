using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public float moveSpeed=.4f;
	public Vector3 moveRange=new Vector3(.3f,.3f,.3f);
	public float rotationSpeed=.55f;
	public Vector3 rotationRange=new Vector3(4,4,4);
	private Vector3 position;
	private Vector3 rotation;
	private FractalNoise s_Noise;
	
	void Start(){
		position=transform.position;
		rotation=transform.eulerAngles;
	}
	
	void Update(){
		transform.position = position + Vector3.Scale(GetVector3(moveSpeed), moveRange);	
		transform.eulerAngles = rotation + Vector3.Scale(GetVector3(rotationSpeed), rotationRange);
	}
			
	Vector3 GetVector3(float speed){
		float time = Time.time*.01f*speed;
		return new Vector3(Get().HybridMultifractal(time, 15.73F, 0.58F), Get().HybridMultifractal(time, 63.94F, 0.58F), Get().HybridMultifractal(time, 0.2F, 0.58F));
	}
	
	FractalNoise Get() { 
		if (s_Noise == null)
			s_Noise = new FractalNoise (1.27F, 2.04F, 8.36F);
		return s_Noise;		
 	}
}