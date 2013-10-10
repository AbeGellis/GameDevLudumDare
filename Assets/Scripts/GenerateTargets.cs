using UnityEngine;
using System.Collections;

public class GenerateTargets : MonoBehaviour {
	
	public float interval;
	public GameObject movingTarget;
	
	float timer = 0;
	
	void FixedUpdate() {
		timer -= Time.fixedDeltaTime;
		if (timer < 0) {
			timer += interval;
			Instantiate(movingTarget, transform.position, Quaternion.identity);
		}
	}
}
