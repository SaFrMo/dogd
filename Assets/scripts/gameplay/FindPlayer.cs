using UnityEngine;
using System.Collections;

public class FindPlayer : MonoBehaviour {

	// IMPLEMENTATION:
	// ====================
	// Attach to Gameplay Manager.

	// Static reference to the player to prevent a million GameObject.Find calls

	public static GameObject PLAYER = null;

	void Start () {
		if (PLAYER == null) {
			PLAYER = GameObject.Find ("Player");
		}
	}
}
