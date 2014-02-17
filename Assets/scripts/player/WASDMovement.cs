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
	
	Vector3 GoToTarget() {
		if (dock != null && Vector3.Distance (transform.position, dock.transform.position) >= clampDistance) {
			return Vector3.MoveTowards (transform.position, dock.transform.position, dockingRate * Time.deltaTime);
		}
		else {
			dock = null;
			return transform.position;
		}
	}

	void RotateToTarget () {
		if (dock != null) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, dock.transform.rotation, rotationRate * Time.deltaTime);
		}
	}


	// rate at which to move
	const float movementRate = 0.05f;

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

	protected void Controls() {
		newPosition = transform.position +
			//SingleControl (KeyCode.W, Vector3.up) +
			SingleControl (KeyCode.A, transform.TransformDirection(Vector3.left)) +
			//SingleControl (KeyCode.S, Vector3.down) +
			SingleControl (KeyCode.D, transform.TransformDirection(Vector3.right));

		rigidbody.MovePosition (newPosition);
	}

	void Dock() {
		newPosition = GoToTarget();
		rigidbody.MovePosition (newPosition);
		RotateToTarget();
	}
	
	protected void Update () {
		if (dock == null) {
			Controls();
		}
		else {
			Dock();
		}
	}
}
