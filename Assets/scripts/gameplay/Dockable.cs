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

	Shader originalShader;
	public Shader glow;

	// these can be changed from instance to instance
	public Color outlineColor = new Color (0, 255, 0);
	public float outlineWidth = 0.01f;

	// CHECK FOR DOCKABLE SURFACE
	// ============================
	// 1. Is mouse above a surface tagged "DOCKABLE"?
	// 2. If so, make that surface glow.
	// 3. 	If so and if clicked, send player to that surface.

	void OnMouseEnter () {
		MouseIsOver = true;
		renderer.material.shader = glow;
		renderer.material.SetColor ("_OutlineColor", outlineColor);
		renderer.material.SetFloat ("_Outline", outlineWidth);
	}

	void OnMouseExit () {
		MouseIsOver = false;
		renderer.material.shader = originalShader;
	}


	void Start() {
		// Save original material
		originalShader = renderer.material.shader;
	}
}
