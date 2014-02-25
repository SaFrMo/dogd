using UnityEngine;
using System.Collections;

public class Terminal : GlowsOnMouseOver {

	bool showScreen = false;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			showScreen = true;
		}
	}
}
