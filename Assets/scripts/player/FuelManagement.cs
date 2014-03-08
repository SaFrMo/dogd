using UnityEngine;
using System.Collections;

public class FuelManagement : MonoBehaviour {

	public int startingFuel = 5;

	// FUEL MANAGEMENT
	// ==================
	// Attach to the player

	public int FuelCount { get; private set; }

	public void ChangeFuelCount (int delta) {
		FuelCount += delta;
	}

	void Start () {
		FuelCount = startingFuel;
	}

}
