using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	// HUD
	// ======
	// Attach a single copy to Gameplay Manager.

	static GameObject player;
	public GUISkin skin;

	void Start () {
		player = GameObject.Find ("Player");
		remainingFuelWidth = Screen.width / 5;
		remainingFuelHeight = Screen.height / 5;
	}

	// GUI FUNCTIONALITY
	// ==================
	// Display fuel boosts left.

	float spacer = 40f;

	float remainingFuelWidth;
	float remainingFuelHeight;


	void RemainingFuel() {
		// upper right hand corner of screen
		GUI.Box (new Rect (Screen.width - remainingFuelWidth - spacer,
		                   spacer,
		                   remainingFuelWidth,
		                   remainingFuelHeight), player.GetComponent<FuelManagement>().FuelCount.ToString(), skin.customStyles[1]);
	}





	void OnGUI () {
		GUI.skin = skin;
		RemainingFuel();

	}
}
