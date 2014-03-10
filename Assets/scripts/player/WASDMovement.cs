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
	public float movementRate = 0.1f;
	float movementCopy;

	// where the rigidbody will move
	Vector3 newPosition;

	// MOVEMENT COMPONENT
	// ====================
	// Called for each possible control. Result is added to transform.position
	// to produce accurate movement.

	public void Freeze (bool toFreeze) {
		if (toFreeze) {
			movementRate = 0;
		}
		else {
			movementRate = movementCopy;
		}
	}

	protected Vector3 SingleControl(KeyCode key, Vector3 direction) {
		if (Input.GetKey (key)) {
			return direction * movementRate;
		}
		else {
			return Vector3.zero;
		}
	}

	protected void FlyingControls (KeyCode key, Vector3 direction) {
		if (Input.GetKey (key) ) {//&& gameObject.GetComponent<FuelManagement>().FuelCount > 0) {
			rigidbody.AddForce (transform.TransformDirection(direction * fuelMovementRate));
			//gameObject.GetComponent<FuelManagement>().ChangeFuelCount(-1);
		}
	}

	// MAIN CONTROLS
	// ====================
	// Runs all Single Controls and generates a new Vector3 based on the results,
	// then uses the rigidbody (so as to detect collisions) to move the GO to that
	// new position.

	RaycastHit hit;

	public float fuelMovementRate = 100f;

	Timer jumpStart;
	bool canJump = true;
	bool canFly = false;
	bool flying = false;
	bool canLand = false;
	bool grounded = true;

	void OnCollisionStay() {
		canJump = true;
		grounded = true;
	}

	void OnCollisionExit () {
		grounded = false;
	}

	protected void Controls() {
		if (!flying) {
			newPosition = transform.position +
				//SingleControl (KeyCode.Space, transform.TransformDirection(Vector3.up)) +
				SingleControl (KeyCode.A, transform.TransformDirection(Vector3.left)) +
				//SingleControl (KeyCode.S, Vector3.down) +
				SingleControl (KeyCode.D, transform.TransformDirection(Vector3.right));
			rigidbody.MovePosition (newPosition);
		}

		/*
		else {
			if (Input.GetKeyDown (KeyCode.W)) {
				rigidbody.AddForce (transform.TransformDirection(Vector3.up * fuelMovementRate));
			}
		}
		*/


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
				canFly = true;
			}
		}

		else if (canFly) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (rigidbody.constantForce.enabled) {
					rigidbody.constantForce.enabled = false;
					canFly = false;
					flying = true;
				}
			}
		}

		else if (flying) {
			FlyingControls (KeyCode.W, transform.TransformDirection(Vector3.up));
			FlyingControls (KeyCode.A, transform.TransformDirection(Vector3.left));
			FlyingControls (KeyCode.S, transform.TransformDirection(Vector3.down));
			FlyingControls (KeyCode.D, transform.TransformDirection(Vector3.right));
			if (Input.GetKeyDown(KeyCode.Space)) {
				canJump = true;
				canFly = true;
				flying = false;
				rigidbody.constantForce.enabled = true;
			}
		}
	}

	void OnCollisionEnter () {
		if (!rigidbody.constantForce.enabled) {
			rigidbody.constantForce.enabled = true;
		}
		canJump = true;
		canFly = true;
		flying = false;
	}

	void Dock() {
		//GoToTarget();
		RotateToTarget();
		if (correctRotation) {//correctAltitude && correctRotation) {
			dock = null;
		}
	}

	// ANIMATION
	// ============

	Animator animator;

	void PlayerAnimation () {
		//if (grounded) {
			if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
					animator.SetInteger("Direction", 0);
				}
			else {
				animator.SetInteger ("Direction", 1);//(Input.GetKey (KeyCode.A) ? 1 : 2));
			}
		//}
	}


	// START AND UPDATE
	// ===================

	void Start () {
		movementCopy = movementRate;
		animator = GetComponentInChildren<Animator>();
	}

	protected void Update () {
		//print (animator.GetInteger("Direction"));
		PlayerAnimation();
		if (dock == null) {

			Controls();
			/*
			if (!rigidbody.useGravity) {
				rigidbody.useGravity = true;
			}
			*/
		}
		else {
			Dock();
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
				//dock = null;
			}
		}
	}
}
