using UnityEngine;
using System.Collections;

public class RubbleBob : MonoBehaviour {

	public float sinModifier = 1f;

	public float rotationRate = 0;

	void Update () {
		float mod = Mathf.Sin (Time.time);
		transform.position = new Vector3 (transform.position.x,
		                                  transform.position.y + sinModifier * 0.001f * (mod),
		                                  transform.position.z);
		transform.Rotate (new Vector3 
	}
}
