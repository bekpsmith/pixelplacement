using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
public class DragRotate : MonoBehaviour{
	float dragMultiplier = 300;
	float forceMultiplier = 45;
	float motorTargetVelocity = 15;
	float motorForce = 0;
	float angularDrag = .85f;
	Transform _transform = null;
	Rigidbody _rigidbody;	
	float rotForce = 0;
	HingeJoint _joint;
	
	public Vector3 dragAxis = new Vector3(0,1,0);
	
	void Awake(){		
		//grab pieces:
		_transform = transform;
		_rigidbody = rigidbody;
		_rigidbody.angularDrag = angularDrag;
		_rigidbody.useGravity = false;
		_joint = hingeJoint;
		_joint.axis = dragAxis;
		_joint.anchor = dragAxis*.5f;
		
		//setup a connected body anchor so movement isn't locked to the world:
		GameObject jointAnchor = new GameObject("JointAnchor");
		jointAnchor.transform.parent = _transform;
		Rigidbody jointAnchorRigidbody = jointAnchor.AddComponent<Rigidbody>();
		jointAnchorRigidbody.isKinematic=true;
		jointAnchorRigidbody.useGravity=false;
		_joint.connectedBody=jointAnchorRigidbody;
		
		//setup motor options:
		JointMotor _jointMotor = _joint.motor;
		_jointMotor.force = motorForce;
		_jointMotor.targetVelocity = motorTargetVelocity;
		_joint.motor = _jointMotor;
	}
	
	void Update(){
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
				
				case TouchPhase.Began:
					_joint.useMotor=false;	
					_rigidbody.angularVelocity = Vector3.zero;
				break;
					
				case TouchPhase.Moved:
					//rotate if a finger is dragging:
					Vector2 normalizedDrag = new Vector2(touch.deltaPosition.x/Screen.width, touch.deltaPosition.y/Screen.height);
					float controllingDirection = Mathf.Abs(normalizedDrag.x) > Mathf.Abs(normalizedDrag.y) ? normalizedDrag.x : normalizedDrag.y;
					_transform.Rotate(new Vector3(dragAxis.x*(-controllingDirection*dragMultiplier),dragAxis.y*(-controllingDirection*dragMultiplier),dragAxis.z*(-controllingDirection*dragMultiplier)),Space.Self);
					rotForce = -controllingDirection*dragMultiplier;
				break;
					
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					//apply a throw force when finger releases:
					_joint.useMotor=true;
					_rigidbody.AddRelativeTorque(new Vector3(dragAxis.x*(rotForce*forceMultiplier),dragAxis.y*(rotForce*forceMultiplier),dragAxis.z*(rotForce*forceMultiplier)));
				break;
					
				default:
				break;
				}
			}
		}			
	}

}