using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terminal3_sc2 : Conversation {

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
				{ "Computer, I'm getting uncomfortable that I say \"thank you\" to you so regularly now.", 1 }
			};
			break;

		case 1:
			toContent = "Awaiting query or command.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "You can't really understand what I'm saying, can you?", 2 }
			};
			break;

		case 2:
			toContent = "Your transmission is coming through clearly. Voice-activated command activated and running properly.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Alright. Okay. Thanks.", 3 }
			};
			break;

		case 3:
			toContent = "Received.";
			AllowContinue();
			break;

		case 4:
			DoneTalking();
			Advance(0);
			break;


			/*

		case 1:
			toContent = "Query not understood. Did you mean: <i>Byzantine</i>?";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "\"Byzantine?\" Where the hell did you get \"Byzantine?\"", 2 }
			};
			break;

		case 2:
			toContent = "Query not understood.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Computer, disregard previous query and begin personal recorder.", 3 }
			};
			break;

*/

			/*
		case 0:
			if (GAME_MANAGER_SCENE_2.powerOn) {
				toContent = "Captain. Relevant material discovered.";
				AllowPlayerLines();
				playerLines = new Dictionary<string, int> () {
					{ "Go ahead, computer.", 1 }
				};
				break;
			}
			else {
				toContent = "No power to this terminal yet.";
				AllowContinue(99);
				break;
			}

		case 1:
			toContent = "Grand Duchy is listed as a Courrier-class adaptive liner, destroyed in the war.";
			AllowContinue();
			break;

		case 2:
			toContent = "All <i>right</i>. We got lucky with this one. Begin annotation.";
			AllowContinue();
			break;

		case 3:
			toContent = "This is an unclaimed adaptive liner. A-liners ripe for picking are gold mines; they’ve got plenty of uniques in both hardware and blueprints.";
			AllowContinue();
			break;

		case 4:
			toContent = "Further relevant information discovered.";
			AllowPlayerLines () ;
			playerLines = new Dictionary<string, int>() {
				{ "Stop annotation. Go ahead.", 5 }
			};
			break;

		case 5:
			toContent = "Passenger list contains a Rachel Davis.";
			AllowContinue();
			break;

		case 6:
			toContent = "...";
			AllowContinue();
			break;

		case 7:
			toContent = "...";
			AllowContinue();
			break;

		case 8:
			toContent = "There are lots of Rachels out there. Doesn’t mean it’s Rachel.";
			AllowContinue();
			break;

		case 9:
			toContent = "Descriptions match your subscribed alerts for Rachel Davis, female, twenty-five -";
			AllowPlayerLines ();
			playerLines = new Dictionary<string, int>() {
				{ "[Interrupt]", 10 }
			};
			break;

		case 10:
			toContent = "Fine. Okay. Continue scanning. Save relevant information, but don’t update me yet.";
			AllowContinue();
			break;

		case 11:
			toContent = "Acknowledged.";
			AllowContinue();
			break;

		case 12:
			DoneTalking();
			Advance (9);
			break;

		case 99:
			DoneTalking();
			Advance(0);
			break;
			*/


		};

		if (!showConversation)
			showPlayerLine = false;

		content = toContent;
	}
	


	
}
