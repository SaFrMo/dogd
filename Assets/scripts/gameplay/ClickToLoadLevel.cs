using UnityEngine;
using System.Collections;

public class ClickToLoadLevel : MonoBehaviour {

	public string levelToLoad;

	public Color outlineColor = new Color (0, 255, 0);
	public float outlineWidth = 0.01f;

	public Shader glow;

	Shader originalShader;

	void Start () {
		originalShader = renderer.material.shader;
	}

	void OnMouseEnter () {
		renderer.material.shader = glow;
		renderer.material.SetColor ("_OutlineColor", outlineColor);
		renderer.material.SetFloat ("_Outline", outlineWidth);
	}

	void OnMouseOver () {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel (levelToLoad);
		}
	}

	void OnMouseExit () {
		renderer.material.shader = originalShader;
	}
}
