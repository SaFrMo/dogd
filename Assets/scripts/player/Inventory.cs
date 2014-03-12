using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GUISkin skin;

	public List<string> inventory = new List<string>();

	public KeyCode GUIKey = KeyCode.Tab;

	float spacer = 10f;

	string InventoryContent () {
		string toReturn = "INVENTORY.\n";
		if (inventory.Count == 0) {
			toReturn += "[inventory empty]";
		}
		else {
			foreach (string s in inventory) {
				toReturn += "." + s + "\n";
			}
		}
		return toReturn;
	}

	void OnGUI () {
		GUI.skin = skin;
		if (Input.GetKey (GUIKey)) {
			GUI.Box (new Rect (spacer,
			                   Screen.height / 4f,
			                   Screen.width / 3f,
			                   Screen.height / 3f), InventoryContent(), skin.customStyles[2]);
		}
	}
}
