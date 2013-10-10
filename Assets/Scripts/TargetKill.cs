using UnityEngine;
using System.Collections;

public class TargetKill : MonoBehaviour {

	int grabLayer;
	void Start () {
		grabLayer = LayerMask.NameToLayer("Grab");
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == grabLayer)
			Destroy(other.gameObject);
	}
}
