using UnityEngine;
using System.Collections;

public class GlowsOnMouseOver : MonoBehaviour {

	// IMPLEMENTATION:
	// =================
	// Base class for anything that glows on mouseover.
	// Requires Silhouetted Diffuse shader to work.

	public Color outlineColor = new Color (0, 255, 0);
	public float outlineWidth = 0.01f;
	
	public Shader glow;
	
	Shader originalShader;
	
	protected void Start () {
		originalShader = renderer.material.shader;
	}
	
	protected void OnMouseEnter () {
		renderer.material.shader = glow;
		renderer.material.SetColor ("_OutlineColor", outlineColor);
		renderer.material.SetFloat ("_Outline", outlineWidth);
	}

	protected void OnMouseExit () {
		renderer.material.shader = originalShader;
	}
}
