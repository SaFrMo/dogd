using UnityEngine;
using System.Collections;

public class insideA_term1 : GlowsOnMouseOver {

	bool showScreen = false;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			showScreen = true;
		}
	}
}
