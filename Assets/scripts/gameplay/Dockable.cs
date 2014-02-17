using UnityEngine;
using System.Collections;

public class Dockable : MonoBehaviour {

	// IMPLEMENTATION
	// ================
	// Attach to every Dockable object.
	// (easier implementation - at load time, scan scene for Dockable objects and attach?)

	public bool MouseIsOver { get; private set; }
	public bool Selected { get; private set; }
	public bool Clicked { get; private set; }

	Material originalMaterial;
	public Shader glow;

	// CHECK FOR DOCKABLE SURFACE
	// ============================
	// 1. Is mouse above a surface tagged "DOCKABLE"?
	// 2. If so, make that surface glow.
	// 3. 	If so and if clicked, send player to that surface.

	void OnMouseEnter () {
		MouseIsOver = true;
		renderer.material.shader = glow;

	}

	void OnMouseLeave () {
		MouseIsOver = false;
		renderer.material = originalMaterial;
		print ("gone");
	}


	void Start() {
		// Save original material
		originalMaterial = renderer.material;
	}
}
