using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour {

	/*
	 * FIELDS
	 */

	public GUISkin skin;


	/*
	public GameObject GUIStyleHolder;
	public GUIStyle style;
	public GUIStyle text;
	public GUIStyle playerChoice;
	*/

	// will display conversation text
	public bool showConversation = false;
	public Texture2D continueIcon;

	// these need to be overridden in children classes
	public int conversationIndex = 0;


	// Content information
	/*public Dictionary<int, string> conversation = new Dictionary<int, string>();
	
	public void AddCell (int key, string val) {
		conversation.Add (key, val);
	}*/
	
	protected virtual void GetContent (int key, out string content, out Dictionary<string, int> playerLines) {//out string[] playerLines) {
		content = "It's more than a name, it's an attitude.";
		playerLines = null;
	}

	// continue text
	protected bool showContinueButton = false;


	// box resizing information
	float currentWidth = 0;
	float currentHeight = 0;
	float maxWidth = 200f;
	float maxHeight = 200f;

	public float spacer = 5f;
	public float smooth = 2f;

	// key to activate/deactivate conversations
	KeyCode useKey = KeyCode.E;

	// player's dialogue
	public bool showPlayerLine = false;
	float currentPlayerLineWidth = 0;
	float currentPlayerLineHeight = 0;
	float maxPlayerLineWidth;
	float maxPlayerLineHeight;

	protected int whereTo = -1;

	// override when a far-away character interrupts the speaker
	public bool interruptionOverride = false;
	public bool beingInterrupted = false;

	/*
	 * METHODS
	 */



	// displays either the "hey! listen! conversation available!" icon or the conversation itself
	void IsShowWindow () {
		if (gameObject.GetComponent<ScanForConversation>().isNearPlayer) {
			if (Input.GetKeyDown(useKey)) {
				showConversation = !showConversation;
			}
			if (showConversation) 
				gameObject.GetComponent<ConversationAvailable>().showIcon = false;
			else {
				if (interruptionOverride)
					gameObject.GetComponent<ConversationAvailable>().showIcon = false;
				else
					gameObject.GetComponent<ConversationAvailable>().showIcon = true;
			}
		}
		else {
			showConversation = false;
		}
	}

	// the conversation window itself, where all the text is displayed
	void ConversationWindow () {




		Rect textBox = new Rect (Camera.main.WorldToScreenPoint (transform.position).x - currentWidth * .75f,
		                         Camera.main.WorldToScreenPoint (transform.position).y + transform.renderer.bounds.extents.y + spacer,
		                         currentWidth,
		                         currentHeight);


		//string content = GetContent (conversationIndex);
		string content;
		Dictionary<string, int> playerLinesArray;
		GetContent (conversationIndex, out content, out playerLinesArray);

		//string playerLines = string.Empty;
		/*if (playerLinesArray != null) {
			foreach (string x in playerLinesArray) {
				playerLines += "\n" + x;
			}
		}*/

		

		// resize dialogue window
		currentWidth = Mathf.Lerp (currentWidth, (showConversation ? maxWidth : 0), smooth * Time.deltaTime);
		currentHeight = Mathf.Lerp (currentHeight, (showConversation ? maxHeight : 0), smooth * Time.deltaTime);
		// prevent text-shuffling
		if (currentWidth <= 0.8f * maxWidth) {
			content = "";
			//text.wordWrap = false;
		} //else { text.wordWrap = true; }


		// PLAYER LINES

		// side offshoot containing dialogue choices
		maxPlayerLineWidth = maxWidth;
		maxPlayerLineHeight = maxHeight / 2;

		currentPlayerLineWidth = Mathf.Lerp (currentPlayerLineWidth, (showPlayerLine ? maxPlayerLineWidth : 0), smooth * Time.deltaTime);
		currentPlayerLineHeight = maxPlayerLineHeight;//Mathf.Lerp (currentPlayerLineHeight, (showPlayerLine ? maxPlayerLineHeight : 0), smooth * Time.deltaTime);

		if (showPlayerLine) {
			float boxX = textBox.x + textBox.width;
			float boxY = textBox.y + .25F * textBox.height;
			// background box
			GUI.Box (new Rect (boxX,
			                   boxY,
			                   currentPlayerLineWidth,
			                   currentPlayerLineHeight), "");
			GUILayout.BeginArea ( new Rect (boxX,
			                     boxY,
			                     currentPlayerLineWidth,
			                     currentPlayerLineHeight));
			if (playerLinesArray != null) {
				foreach (KeyValuePair<string, int> x in playerLinesArray) {
					if (GUILayout.Button (x.Key)) {
						Advance (x.Value);
					}
				}
			}
			GUILayout.EndArea();
		}



		// disappear it if it gets too small
		if (currentWidth >= 10f && currentHeight >= 10f) {
			GUI.depth = 1;
			// THIS IS THE CONTENT OF THE CONVERSATION
			GUI.Box (textBox, content);
			// THERE IT IS ^
			GUI.depth = 0;
			if (showContinueButton) {
				if (whereTo == -1) {
					if (GUI.Button (textBox, ( (int) Time.realtimeSinceStartup % 2 == 0 ? continueIcon : null))) {
						Advance ();
					}
				}
				else {
					if (GUI.Button (textBox, ( (int) Time.realtimeSinceStartup % 2 == 0 ? continueIcon : null))) {
						Advance (whereTo);
					}
				}
			}
		}
	}

	// rough "advance one"
	void Advance () {
		conversationIndex++;
	}

	protected void Advance (int where) {
		conversationIndex = where;
	}

	protected void DoneTalking() {
		showConversation = false;
	}

	public int conversationIndexCopy;

	// have another character say something
	protected void Interrupt (GameObject character, int theirLine) {
		//character.GetComponent<Conversation>().conversationIndexCopy = character.GetComponent<Conversation>().conversationIndex;
		showConversation = false;
		interruptionOverride = true;
		character.GetComponent<Conversation>().interruptionOverride = false;
		character.GetComponent<Conversation>().conversationIndex = theirLine;
		character.GetComponent<Conversation>().showConversation = true;
	}


	//protected void DoneBeingInterrupted () {


	/*
	 * MAKIN' IT HAPPEN
	 */

	void Start () {
		/*
		text.wordWrap = true;
		style = GUIStyleHolder.GetComponent<MasterGUIStyle>().style;
		text = GUIStyleHolder.GetComponent<MasterGUIStyle>().text;
		playerChoice = GUIStyleHolder.GetComponent<MasterGUIStyle>().playerChoice;
		*/
	}

	void Update () {
		IsShowWindow();
	}

	void OnGUI () {
		GUI.skin = skin;
		ConversationWindow();
	}
}
