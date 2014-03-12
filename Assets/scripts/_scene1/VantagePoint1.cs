using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VantagePoint1 : Conversation {

	// Variation of the standard Vantage Point script to allow for specific dialogue
	
	// IMPLEMENTATION:
	/* GetContent is the main function here. First, it resets the following values:
	 * 		playerLines: the dictionary<string, int> that contains what a player says and what index that leads to
	 * 		showContinueButton: whether or not the NPC has more to say before the player can chime in
	 * 		showPlayerLine: what the player can say to the NPC
	 * 		whereTo: -1 is a flag value that means clicking on the "next" button will simply advance the conversation by 1.
	 * 			If AllowContinue(int x) is called, -1 will be replaced by x, which will take the conversation to a different
	 * 			place than the next index.
	 * 
	 */
	
	// this is the string the NPC will say
	string toContent;
	
	
	
	// allow progression to next conversation index
	void AllowContinue () {
		showContinueButton = true;
	}
	
	// jump to a special index
	void AllowContinue (int where) {
		showContinueButton = true;
		whereTo = where;
	}
	
	void AllowPlayerLines () {
		showPlayerLine = true;
	}
	
	public float targetCameraSize;
	public float rate = 5f;
	float originalCameraSize;
	float originalMovement;
	
	// HOW TO USE
	// 1. toContent = what the NPC will say.
	// 2a. If the player is allowed to progress to the next line without any choice, call AllowContinue() or AllowContinue(int where).
	// 2b. If the player has something to say, call AllowPlayerLines() and then create:
	// 		Dictionary<string, int> playerLines = new Dictionary<string, int>() { ... };
	// 		where each string is the player dialogue choice and each int is the corresponding index of the NPC's response
	// 3. If the NPC is to be interrupted, call Interrupt (gameObject interrupter, int lineInterrupterSays); on the relevant case.
	//		Make sure to set conversationIndex on the other character to the one you want them to start out with when you speak with them next.
	protected override void GetContent (int key, out string content, out Dictionary<string, int> playerLines) {
		
		interruptionOverride = false;
		playerLines = null;
		showContinueButton = false;
		showPlayerLine = false;
		whereTo = -1;
		
		switch (key) {
			
		case 0:
			toContent = "Receiving, Captain.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Begin annotation.", 1 }
			};
			break;
			
		case 1:
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, targetCameraSize, rate * Time.deltaTime);
			toContent = "You may proceed.";
			player.GetComponent<WASDMovement>().Freeze(true);
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Target ship <i>Grand Duchy</i> is a luxury passenger liner. Listed as destroyed in the war.", 2 }
			};
			break;

		case 2:
			toContent = "Recorded: \"Target ship <i>Grand Duchy</i> is a luxury passenger liner. Listed as destroyed in the war.\"";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "The ship has a large unique on its starboard side, but otherwise looks relatively standard.", 3 }
			};
			break;

		case 3:
			toContent = "Recorded: \"The ship has a large unique on its starboard side, but otherwise looks relatively standard.\"";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "I'll board and search for software blueprints before worrying about hardware uniques. Annotation complete.", 4 }
			};
			break;

		case 4:
			toContent = "Recorded: \"I'll board and search for software blueprints before worrying about hardware uniques.\" Annotation stopped.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Thank you, computer.", 5 }
			};
			break;
			
		case 5:
			// snaps-to at 0.2 difference
			if (Camera.main.orthographicSize - originalCameraSize >= 1f) {
				player.GetComponent<WASDMovement>().Freeze (false);
				Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, originalCameraSize, rate * Time.deltaTime);
				DoneTalking();
			}
			else {
				Advance(0);
			}
			break;
			
			
		};
		
		if (!showConversation)
			showPlayerLine = false;
		
		content = toContent;
		
	}
	
	protected void Start () {
		base.Start();
		originalCameraSize = Camera.main.orthographicSize;
	}
	
	
	
	
}
