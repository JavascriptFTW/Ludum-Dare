using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AssemblyCSharp;

public class Monster : MonoBehaviour {

	static Object MonsterBasic = Resources.Load("Prefabs/MonsterBasic");

	public int scarinessLvl = 1;
	float sightRange = 15;
	float maxChaseRange = 20;
	int interactRange = 3;
	float moveSpeed = 5;
	float turnSpeed = 5;
	float jumpHeight = 1;
	PlayerController prey;
	Vector3 wanderVec;
	bool grounded = false;
	List<Cell> path = new List<Cell> ();
	int health = 1;
	float scale = 0;
	public bool doWander = true;


	public static Object create(float x, float y, float z, int scariness) 
	{
		GameObject monsterToCreate = (GameObject)Object.Instantiate (MonsterBasic, new Vector3 (x, y, z), Quaternion.identity);
		monsterToCreate.GetComponent<Monster>().Initialize (scariness);
		return monsterToCreate;
	}

	void Initialize(int scariness) {
		scarinessLvl = scariness;
	}

	// Use this for initialization
	void Start () {

		wanderVec = new Vector3 (transform.position.x, 1, transform.position.y);


		scale = Mathf.Ceil (scarinessLvl / 10);
		if (scale < 1) {
			scale = 1;
		}
		sightRange /= scale;
		maxChaseRange /= scale;
		health = (int)scale * 4;
		jumpHeight *= scale * 5;
		if (health < 1) {
			health = 1;
		}
		transform.localScale = new Vector3 (scale, scale, scale);

		Game.monsters.Add (this);
	}

	void OnCollisionEnter (Collision collisionInfo) {
		if (grounded) {
			tryJump (4f);
		} else if (collisionInfo.transform.position.y < transform.position.y) {
			grounded = true;
		} else if (collisionInfo.transform.position.y > transform.position.y) {
			tryJump (1f);
		}
	}
		
	void OnCollisionExit () {
		grounded = false;
	}
	
	void tryJump (float div) {
		rigidbody.velocity = new Vector3(0, jumpHeight / div, 0);
	}

	void interact(PlayerController player) {
		if (player.getScariness() >= scarinessLvl) {
			player.incScariness(1);
			health -= 1;
		} else if (player.getScariness() > 10) {
			player.incScariness(-1);
		}
	}

	public void kill () {
		Destroy (gameObject);
	}

	public void chase() {
		/*int xInd = Mathf.RoundToInt (transform.position.x + Game.map.getHalfWidth ());
		int yInd = Mathf.RoundToInt (transform.position.z + Game.map.getHalfHeight ());

		int preyXInd = Mathf.RoundToInt (prey.transform.position.x + Game.map.getHalfWidth ());
		int preyYInd = Mathf.RoundToInt (prey.transform.position.z + Game.map.getHalfHeight ());

		path = Game.map.map[xInd][yInd].pathTo(preyXInd, preyYInd);

		float angle = Vector3.Angle (transform.position, Game.player.transform.position);
		float shouldRotate = transform.rotation.y - angle;

		transform.Rotate (0, shouldRotate, 0);
		transform.Translate (moveSpeed * Time.deltaTime, 0, 0);

		if (path.Count > 1) {
			transform.position = new Vector3 (path[1].x, 2, path[1].z);
			path.RemoveAt(0);
		} 

		for (int i = 0; i < monsters.Count; i ++) {
				Monster monsterToUpdate = monsters[i];
				float dist = Vector3.Distance(monsterToUpdate.transform.position, player.transform.position);
				if (dist <= monsterToUpdate.sightRange) {
					monsterToUpdate.chase(player);
				}
			}
		 */

		Quaternion lookRot = Quaternion.LookRotation((prey.transform.position - transform.position).normalized);

		transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, turnSpeed * Time.deltaTime);
		transform.Translate (0, 0, moveSpeed * Time.deltaTime);
	}

	public void flee() {
		Quaternion lookRot = Quaternion.LookRotation((transform.position - prey.transform.position).normalized);
		
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, turnSpeed * Time.deltaTime);
		transform.Translate (0, 0, moveSpeed * Time.deltaTime * 1.5f);
	}

	public void wander() {
		float distToWander = Vector3.Distance (transform.position, wanderVec);
		if (distToWander <= 2) {
			float nX = transform.position.x + Random.Range (-10, 10);
			float nZ = transform.position.y + Random.Range (-10, 10);

			if (nX > -Game.map.getHalfWidth() + 1 && nZ > -Game.map.getHalfHeight() + 1 && nX < Game.map.getHalfWidth() - 1 && nZ < Game.map.getHalfHeight() - 1) {
				wanderVec = new Vector3 (nX, 1, nZ);
			}

		} else {
			Quaternion lookRot = Quaternion.LookRotation((wanderVec - transform.position).normalized);
			
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, turnSpeed );
			transform.Translate (0, 0, moveSpeed * Time.deltaTime / 3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (health >= 1) {
			PlayerController player = Game.player;
			float dist = Vector3.Distance(transform.position, player.transform.position);
			if (prey) {
				if (prey.getScariness() >= this.scarinessLvl) {
					flee ();
				} else {
					chase ();
				}
			} else {
				if (dist <= sightRange + transform.localScale.y + player.transform.localScale.y) {
					prey = Game.player;
				} else if (doWander) {
					this.wander ();
				}
			}
			if (dist <= interactRange + transform.localScale.y + player.transform.localScale.y) {
				interact (player);
			}
			if (dist >= maxChaseRange) {
				prey = null;
			}
		}
	}
}
		