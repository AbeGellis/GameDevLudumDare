using UnityEngine;
using System.Collections;

public class NPCControl : MonoBehaviour {
	
	public Transform sprite;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rotateDir = Camera.main.transform.forward;
		rotateDir.Scale(new Vector3(1f, 0f, 1f));
		sprite.LookAt(sprite.position + rotateDir);
	}
}
