using UnityEngine;
using System.Collections;

public class Grab {
	public RaycastHit data;
	public Grab(RaycastHit data) {
		this.data = data;
	}
}

public class CursorControl : MonoBehaviour {
	public GUIElement inertCursor;
	public GUIElement activeCursor;
	
	public float grabDistance;
	public Transform playerEye;
	public static Grab grab;
	
	int grabLayer;
	
	void Start() {
		grabLayer = LayerMask.NameToLayer("Grab");
	}
	
	void Update () {
		Ray grabRay = new Ray(playerEye.position, playerEye.forward);
		RaycastHit rayHit = new RaycastHit();
		if (grab == null) {
			if (Physics.Raycast(grabRay, out rayHit, grabDistance) && rayHit.collider.gameObject.layer == grabLayer) {
				grab = new Grab(rayHit);
				inertCursor.gameObject.SetActive(false);
				activeCursor.gameObject.SetActive(true);
			}
		}
		else {
			if (Physics.Raycast(grabRay, out rayHit, grabDistance) && rayHit.collider.gameObject.layer == grabLayer)
				grab = new Grab(rayHit);
			else {
				grab = null;
				inertCursor.gameObject.SetActive(true);
				activeCursor.gameObject.SetActive(false);
			}
		}
	}
}