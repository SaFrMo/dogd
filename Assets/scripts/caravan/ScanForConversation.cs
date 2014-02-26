using UnityEngine;
using System.Collections;

public class ScanForConversation : MonoBehaviour {

	/*
	 * FIELDS
	 */

	// is the NPC near a player?
	public bool isNearPlayer;
	// range when the above will trigger "true"
	public float range;
	// space to save the player
	GameObject player;


	/*
	 * METHODS
	 */

	// save the player gameobject
	void GetPlayer() {
		player = GameObject.Find ("Player");
	}

	// is the player near this npc?
	void GetNearPlayer () {
		if (Mathf.Abs(player.transform.position.x - transform.position.x ) <= range) {
			if (GetComponent<Conversation>().interruptionOverride) {
				isNearPlayer = false;
			}
			else {
				isNearPlayer = true;
			}
		}
		else {
			if (GetComponent<Conversation>().interruptionOverride) {
				isNearPlayer = true;
			}
			else {
				isNearPlayer = false;
			}
		}
	}

	/*
	 * MAKIN' IT HAPPEN
	 */
	
	void Start () {
		GetPlayer();
	}

	void Update () {
		GetNearPlayer();
	}
}
