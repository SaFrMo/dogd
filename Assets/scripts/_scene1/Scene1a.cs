﻿using UnityEngine;
using System.Collections;

public class Scene1a : MonoBehaviour {

	static Timer t;

	// locator for scripted events
	public static int scriptPlace = -1;

	static GameObject obj = null;

	public static void ScriptedEvents() {

		switch (scriptPlace) {

			// set timer
		case -1:
			t = new Timer(5f);
			scriptPlace++;
			break;

			// wait x seconds
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
			obj.GetComponent<Computer1>().enabled = true;
			scriptPlace++;
			break;

			// reset object
		case 2:
			obj = null;
			break;

			//player tells computer to transmit message - deactivate door,
			// activate "jumping out" trigger
		case 3:
			if (obj == null) {
				obj = GameObject.Find ("Door");
			}
			// Change the door material
			obj.GetComponent<BoxCollider>().isTrigger = true;
			obj = null;
			scriptPlace++;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		t = new Timer(5f);

	}
	
	// Update is called once per frame
	void Update () {
		ScriptedEvents();
	}
}
