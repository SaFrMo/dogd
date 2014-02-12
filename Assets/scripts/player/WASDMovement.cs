using UnityEngine;
using System.Collections;

public class WASDMovement : MonoBehaviour {

	// rate at which to move
	public float movementRate = 0.5f;

	// where the rigidbody will move
	Vector3 newPosition;

	// MOVEMENT COMPONENT
	// ====================
	Vector3 SingleControl(KeyCode key, Vector3 direction) {
		if (Input.GetKey (key)) {
			return direction * movementRate;
		}
		else {
			return Vector3.zero;
		}
	}

	void Controls() {
		newPosition = transform.position +
			SingleControl (KeyCode.W, Vector3.up) +
			SingleControl (KeyCode.A, Vector3.left) +
			SingleControl (KeyCode.S, Vector3.down) +
			SingleControl (KeyCode.D, Vector3.right);
		rigidbody.MovePosition (newPosition);
	}


	void Update () {
		Controls();
	}
}
