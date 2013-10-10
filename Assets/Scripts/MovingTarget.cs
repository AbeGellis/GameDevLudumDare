using UnityEngine;
using System.Collections;

public class MovingTarget : MonoBehaviour {

	public Vector3 movement;
	
	void Start () {
	
	}
	
	void FixedUpdate() {
		transform.Translate(movement);
	}
	
	void Update () {
		
	}
}
