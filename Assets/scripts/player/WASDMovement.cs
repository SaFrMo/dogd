using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class WASDMovement : MonoBehaviour {

	// IMPLEMENTATION
	// ===================
	// Attach to an object that needs WASD to move.
	// Edit controls in Controls() below as fit.



	// SPECIFIC TO DOGD
	// ===================

	//public bool Docked { get; private set; }
	
	GameObject dock = null;
	
	public float dockingRate = 0.1f;
	public float clampDistance = 1f;
	public float rotationRate = 1f;
	Vector3 jumpForce = Vector3.up;

	// SET TARGET (GO)
	// ===================
	// Creates a new target to dock on to
	
	public void SetTarget (GameObject target) {
		dock = target;
	}

	// GO TO TARGET()
	// ==================
	// Move towards the target while rotating toward it.
	// TODO: Snap-to key increases rotation speed
	
	//Vector3 dockingOffset;

	bool correctAltitude;
	bool correctRotation;
	
	void GoToTarget() {
		if (dock != null && (Mathf.Abs (transform.position.y) - Mathf.Abs (dock.transform.position.y) >= clampDistance ||
		                     Mathf.Abs (transform.position.z) - Mathf.Abs (dock.transform.position.z) >= clampDistance)) {
		    				//Vector3.Distance(transform.position, dock.transform.position + Vector3.up) >= clampDistance) {
			correctAltitude = false;
			rigidbody.useGravity = false;
			rigidbody.MovePosition (Vector3.MoveTowards (transform.position, dock.transform.position + Vector3.up, dockingRate * Time.deltaTime));
		}
		else {
			correctAltitude = true;
			rigidbody.useGravity = true;
		}
	}

	void RotateToTarget () {
		if (dock != null && transform.rotation != dock.transform.rotation) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, dock.transform.rotation, rotationRate * Time.deltaTime);
			rigidbody.useGravity = false;
			correctRotation = false;
		}
		else {
			correctRotation = true;
		}
	}


	// rate at which to move
	const float movementRate = 0.1f;

	// where the rigidbody will move
	Vector3 newPosition;

	// MOVEMENT COMPONENT
	// ====================
	// Called for each possible control. Result is added to transform.position
	// to produce accurate movement.

	protected Vector3 SingleControl(KeyCode key, Vector3 direction, float rate = movementRate) {
		if (Input.GetKey (key)) {
			return direction * rate;
		}
		else {
			return Vector3.zero;
		}
	}

	// MAIN CONTROLS
	// ====================
	// Runs all Single Controls and generates a new Vector3 based on the results,
	// then uses the rigidbody (so as to detect collisions) to move the GO to that
	// new position.

	RaycastHit hit;

	Timer jumpStart;
	bool canJump = true;

	void OnCollisionStay() {
		canJump = true;
	}

	protected void Controls() {
		newPosition = transform.position +
			//SingleControl (KeyCode.Space, transform.TransformDirection(Vector3.up)) +
			SingleControl (KeyCode.A, transform.TransformDirection(Vector3.left)) +
			//SingleControl (KeyCode.S, Vector3.down) +
			SingleControl (KeyCode.D, transform.TransformDirection(Vector3.right));

		rigidbody.MovePosition (newPosition);

		// allows differentiation between tap-jumps and held-jumps
		if (canJump) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				jumpStart = new Timer (.04f, 5);
				rigidbody.AddRelativeForce (jumpForce, ForceMode.Force);
			}
			if (Input.GetKey (KeyCode.Space)) {
				if (jumpStart.RunTimer()) {
					rigidbody.AddRelativeForce (jumpForce, ForceMode.Impulse);
				}
			}
			if (Input.GetKeyUp (KeyCode.Space)) {
				canJump = false;
			}
		}
	}

	void Dock() {
		GoToTarget();
		RotateToTarget();
		if (correctAltitude && correctRotation) {
			dock = null;
		}
	}
	
	protected void Update () {
		if (dock == null) {
			Controls();
			if (!rigidbody.useGravity) {
				rigidbody.useGravity = true;
			}
		}
		else {
			Dock();
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
				//dock = null;
			}
		}
	}
}
