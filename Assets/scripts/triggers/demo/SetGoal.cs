using UnityEngine;
using System.Collections;

public class SetGoal : MonoBehaviour {

	// IMPLEMENTATION:
	// =================
	// Attach to a trigger cube and drag a goal object.
	// Player's SPS will display path to get to goal object.

	public GameObject target;

	void OnCollisionEnter() {
		Dockable.player.GetComponent<SPS>().SetTarget(target);
	}
}
