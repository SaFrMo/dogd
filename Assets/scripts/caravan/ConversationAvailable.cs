using UnityEngine;
using System.Collections;

public class ConversationAvailable : MonoBehaviour {

	// icon to display
	public Texture2D conversationIcon;

	// so we can hide the icon when the conversation's displayed
	public bool showIcon = true;

	// bouncing variables
	public float distanceAboveNPC = 0f;
	public float smooth;
	public float height;

	// dummy style to clear the default Unity style (ie, no visible background, in this case)
	public GUIStyle style;

	// shows the "use key" icon above the NPC if a conversation is available
	void ShowConversationAvailable () {
		if (gameObject.GetComponent<ScanForConversation>().isNearPlayer && showIcon) {
			GUI.Box (new Rect (Camera.main.WorldToScreenPoint(transform.position).x - transform.renderer.bounds.extents.x,
			                   Camera.main.WorldToScreenPoint(transform.renderer.bounds.extents).y + distanceAboveNPC,
			                   conversationIcon.width,
			                   conversationIcon.height), conversationIcon, style);
		}
	}

	// animates the bouncing for the icon
	bool rising = true;

	void CalculateDistanceAboveNPC () {
		if (rising) {
			distanceAboveNPC = Mathf.SmoothStep (distanceAboveNPC, height, Time.deltaTime * smooth);
			// the 0.2f here and below help the bouncing motion avoid the Unity long, slow end-of-lerp problem
			if (distanceAboveNPC > height - 0.2f) {
				rising = false;
			}
		}
		else {
			distanceAboveNPC = Mathf.SmoothStep (distanceAboveNPC, -height, Time.deltaTime * smooth);
			// see the "if" clause above
			if (Mathf.Abs (distanceAboveNPC) > height - 0.2f) {
				rising = true;
			}
		}
	}



	void OnGUI () {
		ShowConversationAvailable();
		CalculateDistanceAboveNPC();
	}
}
