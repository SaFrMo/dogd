using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Computer1 : Conversation {

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
			toContent = "Target ship <i>Grand Duchy</i> within sensor range. Zero life forms aboard ship. Shall I begin playing the boarding message?";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Go ahead, computer.", 1 }
			};
			break;

		case 1:
			toContent = "Transmitting. Patching playback through. Good luck, Captain.";
			AllowContinue();
			break;

		case 2:
			DoneTalking();
			Advance (4);
			//randomSet = false;
			break;

		case 3:
			toContent = idleString;
			AllowContinue(4);
			break;

		case 4:
			DoneTalking();
			idleString = GetIdle ();
			Advance (3);
			break;



		};

		if (!showConversation)
			showPlayerLine = false;

		content = toContent;
	}

	string idleString;

	string GetIdle () {
		//if (!randomSet) {
			string[] idle = new string[] {
				"ILOVEYOU.exe downloading: 47%...",
				"All systems nominal, Captain.",
				"All systems nominal, Captain.",
				"All systems nominal, Captain.",
				"I'm afraid I can't do that, Captain.",
				"Captain...There's a man on the wing.",
				"while (true) { Console.WriteLine (\"help i'm stuck in a computer chip\"); }",
				"while (true) { Console.WriteLine (\"help i'm stuck in a computer chip\"); }"
			};
			idleString = idle[UnityEngine.Random.Range (0, idle.Length)];
		//}
		return (idleString);
	}



	
}
