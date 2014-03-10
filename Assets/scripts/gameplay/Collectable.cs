using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Collectable : MonoBehaviour {

	public string collectableDescription;
	public GameObject particleSplash;

	static GameObject player = null;

	public float rotationRate = 40f;
	public float resizeRate = 2f;

	void Start () {
		if (player == null) {
			player = GameObject.Find ("Player");
		}
	}

	void Update () {
		SaFrMo.RotateAndResize (transform, rotationRate, resizeRate);
	}


	void OnCollisionEnter (Collision c) {
		if (c.collider.gameObject == player) {
			player.GetComponent<Inventory>().inventory.Add (collectableDescription);
			GameObject p = Instantiate (particleSplash) as GameObject;
			p.transform.position = transform.position;
			Destroy (gameObject);
		}
	}
}
