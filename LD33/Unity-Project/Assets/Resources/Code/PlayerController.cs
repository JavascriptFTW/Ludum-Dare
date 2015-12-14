using UnityEngine;
using System.Collections;

using AssemblyCSharp;

public class PlayerController : MonoBehaviour {

	float moveSpeed = 10;
	float turnSpeed = 150;
	float jumpHeight = 1;
	int minScariness = 20;
	int scarinessLvl;
	bool grounded = false;
	bool player = true;
	float scale = 0;

	// Use this for initialization
	void Start () {
		scarinessLvl = minScariness;
		Game.init (this);
	}

	void OnCollisionEnter () {
		grounded = true;
	}

	void OnCollisionExit () {
		grounded = false;
	}

	void tryJump () {
		if (grounded) {
			rigidbody.velocity = new Vector3(0, jumpHeight, 0);
		}
	}
	
	public int getScariness() {
		return scarinessLvl;
	}

	public void incScariness(int amount) {
		scarinessLvl += amount;
		if (scarinessLvl < minScariness) {
			scarinessLvl = minScariness;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Horizontal")) {
			transform.Rotate (0, turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0);
		}
		if (Input.GetButton ("Vertical")) {
			transform.Translate	(0, 0, moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
		}
		if (Input.GetButton ("Jump")) {
			tryJump ();
		}
		scale = Mathf.Ceil (scarinessLvl / 10);
		if (scale < 1) {
			scale = 1;
		}
		jumpHeight = scale * 5;
		transform.localScale = new Vector3 (scale, scale, scale);
	}
}
