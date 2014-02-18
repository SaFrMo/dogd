using UnityEngine;
using System.Collections;

public class SPS : MonoBehaviour {

	// SPS: Spatial Positioning System

	KeyCode activationKey = KeyCode.E;

	// IMPLEMENTATION:
	// ==================
	// Attach to Player.

	GameObject[] target;
	GameObject singleTarget;

	// SET TARGET (GO target)
	// =========================
	// Target to guide the player to

	public void SetTarget (GameObject[] toSet) {
		target = toSet;
	}

	public void SetTarget (GameObject single) {
		target = new GameObject[] {
			single
		};
	}

	// SHOW THE WAY
	// =========================
	// An Arma-esque "dandelion" will appear to guide the player

	public void ShowTheWay () {
		foreach (GameObject go in target) {
			print (go.transform.name);
		}
	}

	void Update () {
		if (Input.GetKey (activationKey)) {
			ShowTheWay();
		}
	}
}
