using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float moveSpeed;
	public float maxSpeed;
	public float mouseSensitivity = 1f;
	public Transform eye;
	public ParticleSystem targetGlow;
	public float killPlane;
	
	Vector3 moveInput = Vector3.zero;
	Vector3 mouseDiff = Vector3.zero;
	Vector3 prevPosition;
	
	bool lockOn = false;
	
	Vector3 spawnPosition;
	
	void Start() {
		Screen.lockCursor = true;
		spawnPosition = transform.position;
	}
	
	void FixedUpdate() {
		
		Vector3 horSpeed = rigidbody.velocity;
		horSpeed.Scale(new Vector3(1f, 0f, 1f));
		if (horSpeed.magnitude < maxSpeed) {
			
			rigidbody.AddForce(moveInput * moveSpeed,ForceMode.VelocityChange);
			//rigidbody.velocity.Scale(new Vector3(maxSpeed/horSpeed.magnitude, 1f, maxSpeed/horSpeed.magnitude));
			//rigidbody.velocity.Set(maxSpeed/horSpeed.magnitude * rigidbody.velocity.x, rigidbody.velocity.y, 
			//	maxSpeed/horSpeed.magnitude * rigidbody.velocity.z);
		}
		
		//Don't remember if this actually does anything
		Vector3.ClampMagnitude(rigidbody.velocity,maxSpeed);
	}
	
	void Update () {
		mouseDiff = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * mouseSensitivity;
		
		if (Input.GetKeyDown(KeyCode.Space))
				Screen.lockCursor = !Screen.lockCursor;
		
		if (!lockOn) {
			targetGlow.gameObject.SetActive(false);
			
			rigidbody.useGravity = true;
			eye.transform.Rotate(Vector3.up, mouseDiff.x);
			eye.transform.Rotate(Vector3.right, -mouseDiff.y);
			eye.transform.LookAt(eye.transform.position + eye.transform.forward, Vector3.up);
			
			moveInput = Vector3.zero;
			if (Input.GetKey(KeyCode.W))
				moveInput += eye.transform.forward;
			if (Input.GetKey(KeyCode.S))
				moveInput -= eye.transform.forward;
			if (Input.GetKey(KeyCode.A))
				moveInput -= eye.transform.right;
			if (Input.GetKey(KeyCode.D))
				moveInput += eye.transform.right;
			
			moveInput.Scale(new Vector3(1, 0, 1)); //Cancel out any y-movement
			moveInput.Normalize();
			
			
			if (Input.GetMouseButtonDown(0) && CursorControl.grab != null) {
				lockOn = true;
			}
		}		
		else {
			rigidbody.useGravity = false;
			rigidbody.velocity = moveInput = Vector3.zero;
			
			if (CursorControl.grab == null || !Input.GetMouseButton(0))
				lockOn = false;
			else {
				
				RaycastHit grab = CursorControl.grab.data;
				
				targetGlow.gameObject.SetActive(true);
				targetGlow.transform.position = grab.collider.transform.position;
				
				transform.RotateAround(grab.collider.transform.position, Vector3.up, mouseDiff.x);
				transform.RotateAround(grab.collider.transform.position, eye.transform.right, -mouseDiff.y);
				
				Ray collisionLine = new Ray(prevPosition, transform.position - prevPosition); //Prevent glitching thru walls
				if (Physics.Raycast(collisionLine, Vector3.Distance(transform.position, prevPosition)))
					transform.position = prevPosition;
				transform.rotation = Quaternion.identity;
				eye.transform.LookAt(grab.collider.transform.position);
			}
		}
		
		prevPosition = transform.position;
		
		if (transform.position.y < killPlane) 
			Respawn();
	}
	
	void Respawn() {
		prevPosition = transform.position = spawnPosition;
		rigidbody.velocity = Vector3.zero;
	}
}
