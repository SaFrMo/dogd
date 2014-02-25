using UnityEngine;
using System.Collections;

public class Scene1a : MonoBehaviour {

	Timer t;

	// locator for scripted events
	int scriptPlace = 0;

	GameObject obj = null;

	void ScriptedEvents() {

		switch (scriptPlace) {

			// wait 5 seconds
		case 0:
			if (t.RunTimer()) {
				scriptPlace++;
			}
			break;

			// computer announces GD is in range
		case 1:
			if (obj == null) {
				obj = GameObject.Find ("Computer");
			}
			if (computer.GetComponent<ComputerBehavior>().Play01()) {
				scriptPlace++;
			}
			break;

			// player tells computer to transmit message
		case 2:
			print ("At 2!");
			break;
		}
	}

	// Use this for initialization
	void Start () {
		t = new Timer(5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
