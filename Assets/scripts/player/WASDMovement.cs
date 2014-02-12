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

	public bool IsAttached { get; protected set; }

	public float slowDownDistance = 1f;

	public float slowDownAmount = 0.05f;




	// END SPECIFIC MATERIAL
	// ===================



	// rate at which to move
	public float movementRate = 0.5f;
	float toMove;

	// where the rigidbody will move
	Vector3 newPosition;

	// MOVEMENT COMPONENT
	// ====================
	// Called for each possible control. Result is added to transform.position
	// to produce accurate movement.

	protected Vector3 SingleControl(KeyCode key, Vector3 direction) {
		if (Input.GetKey (key)) {
			return direction * toMove;
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
			SingleControl (KeyCode.W, Vector3.up) +
			SingleControl (KeyCode.A, Vector3.left) +
			SingleControl (KeyCode.S, Vector3.down) +
			SingleControl (KeyCode.D, Vector3.right);

		// slows down the player as they approach a wall/other object if they are free-floating
		if (!IsAttached) {
			if (Physics.Raycast (new Ray (transform.position, newPosition * 500), slowDownDistance)) {
				toMove = movementRate * slowDownAmount;
			}
			else {
				toMove = movementRate;
			}
		}


		rigidbody.MovePosition (newPosition);
	}


	protected void Update () {
		Controls();
	}
}
