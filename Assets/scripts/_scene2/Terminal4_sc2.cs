﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terminal4_sc2 : Conversation {

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
			toContent = "Receiving.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Computer, I have a new directive for you.", 1 }
			};
			break;

		case 1:
			toContent = "Awaiting query or command.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "When I say \"thank you, computer,\" say \"you're welcome, Captain.\"", 2 },
				{ "...Never mind.", 5 }
			};
			break;

		case 2:
			toContent = "Acknowledged.";
			AllowPlayerLines();
			if (!GAME_MANAGER_SCENE_2.sayThankYou) {
				GAME_MANAGER_SCENE_2.sayThankYou = true;
			}
			playerLines = new Dictionary<string, int>() {
				{ "That'll be all for now. Thank you, computer.", 3 }
			};
			break;

		case 3:
			toContent = "You're welcome, Captain.";
			AllowContinue();
			break;

		case 4:
			DoneTalking();
			Advance (2);
			break;

		case 5:
			toContent = "Acknowledged.";
			Advance (0);
			break;


		};

		if (!showConversation)
			showPlayerLine = false;

		content = toContent;
	}
	


	
}
