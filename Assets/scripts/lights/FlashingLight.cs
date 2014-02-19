using UnityEngine;
using System.Collections;

public class FlashingLight : MonoBehaviour {

	public float flashRate;

	float originalIntensity;

	Timer t;

	void Start () {
		t = new Timer (flashRate, true);
		originalIntensity = light.intensity;
	}

	// Update is called once per frame
	void Update () {
		if (t.RunTimer()) {
			if (light.intensity == originalIntensity) {
				light.intensity = 0;
			}
			else {
				light.intensity = originalIntensity;
			}
		}
	}
}
