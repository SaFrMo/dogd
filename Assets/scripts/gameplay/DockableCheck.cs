using UnityEngine;
using System.Collections;

public class DockableCheck : MonoBehaviour {

	// IMPLEMENTATION
	// =================
	// Attach a single copy to the Gameplay Manager GO.

	public string DOCKABLE_TAG = "Dockable";

	public Shader glow;



	// CHECK FOR DOCKABLE SURFACE
	// ============================
	// 1. Grab mouse position.
	// 2. Is it above a surface tagged "DOCKABLE"?
	// 3. If so, make that surface glow.
	// 4. 	If so and if clicked, send player to that surface.

	Ray myRay;
	RaycastHit myHit;
	Material originalMat = null;

	void CheckForDockable () {
		// mouse is above a Dockable GO
		if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out myHit, Mathf.Infinity)) {
			if (myHit.collider.gameObject.CompareTag (DOCKABLE_TAG)) {
				myHit.collider.renderer.material.shader = glow;


				// ON CLICK
				if (Input.GetMouseButtonDown (0)) {
					// I WANT TO GO TO THERE
				}
			}
		}


	}

	// UPDATE()
	// ==========
	void FixedUpdate () {
		//CheckForDockable();
	}
}
