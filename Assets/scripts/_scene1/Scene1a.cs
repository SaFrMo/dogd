using UnityEngine;
using System.Collections;

public class Scene1a : MonoBehaviour {

	static Timer t;

	public AudioClip ding;
	public AudioClip message;
	public AudioClip doorOff;

	// locator for scripted events
	public static int scriptPlace = -1;

	static GameObject obj = null;
	

	public void ScriptedEvents() {

		switch (scriptPlace) {

			// set timer
		case -1:
			t = new Timer(5f);
			scriptPlace++;
			break;

			// wait x seconds
		case 0:
			if (t.RunTimer()) {
				t = null;
				scriptPlace++;
			}
			break;

			// computer announces GD is in range
		case 1:
			if (obj == null) {
				obj = GameObject.Find ("Computer");
			}
			obj.GetComponent<Computer1>().enabled = true;
			if (!audio.isPlaying) {
				audio.clip = ding;
				audio.Play ();
			}
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
			obj.audio.Play ();

			if (!audio.isPlaying) {
				audio.clip = message;
				audio.Play ();
			}
			// Overdub music on message
			// Script player movement?
			obj.GetComponent<BoxCollider>().isTrigger = true;
			obj = null;
			scriptPlace++;
			break;

		case 4:
			if (t == null) {
				t = new Timer (5f);
			}
			if (t.RunTimer()) {
				t = null;
				currentTitle++;
			}
			break;


		}
	}

	// GUI PORTION
	// ==============
	// for the opening title

	int currentTitle = 0;

	string ShowTitles() {
		switch (currentTitle) {

		case 0:
			return string.Empty;
			break;

		case 1:
			if (t == null) {
				t = new Timer (3f);
			}
			if (t.RunTimer()) {
				currentTitle++;
				t = null;
				return string.Empty;
			}
			else {
				return "JASON & SANDER GAMES\npresent";
			}
			break;

		case 2:
			if (t == null) {
				t = new Timer (3f);
			}
			if (t.RunTimer()) {
				currentTitle++;
				t = null;
			}
			return string.Empty;
			break;

		case 3:
			if (t == null) {
				t = new Timer (3f);
			}
			if (t.RunTimer()) {
				currentTitle++;
				t = null;
				return string.Empty;
			}
			else {
				return "THE DEATH\nOF THE GRAND DUCHY";
			}
			break;

		case 4:
			if (t == null) {
				t = new Timer (3f);
			}
			if (t.RunTimer()) {
				currentTitle++;
				t = null;
			}
			return string.Empty;
			break;

		case 5:
			if (t == null) {
				t = new Timer (3f);
			}
			if (t.RunTimer()) {
				currentTitle++;
				t = null;
				return string.Empty;
			}
			else {
				return "Other credits here.";
			}
			break;

		default:
			return string.Empty;
			break;

		
		};
	}

	public GUISkin skin;

	void OnGUI () {
		GUI.skin = skin;
		GUI.Box (new Rect (Screen.width * .6f, Screen.height * .75f, Screen.width * .4f, Screen.width * .25f), ShowTitles(), skin.customStyles[3]);
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
