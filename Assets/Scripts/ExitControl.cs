using UnityEngine;
using System.Collections;

public class ExitControl : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (Application.loadedLevel < Application.levelCount - 1)
			Application.LoadLevel(Application.loadedLevel + 1);
		else
			Application.LoadLevel(0);
	}
}
