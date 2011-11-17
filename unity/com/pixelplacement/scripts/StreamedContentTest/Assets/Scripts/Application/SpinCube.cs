using UnityEngine;
using System.Collections;

public class SpinCube : MonoBehaviour
{
	void Start ()
	{
		iTween.RotateBy(gameObject, iTween.Hash("amount", new Vector3(0,.5f,0), "time", 2, "loopType", "pingPong","easeType",iTween.EaseType.easeInOutExpo));
	}
}

