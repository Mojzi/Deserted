using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAIController : MonoBehaviour {




	Rigidbody2D rigidbody;
	Animator animator;
	MovementController movement;
	bool inChaseMode;

	public GameObject player;
	public int sightRadius = 48;
	public int attackReach = 32;
	Transform pTransform;
	Vector2 distanceVector;

	// Use this for initialization
	void Start () {
		pTransform = player.GetComponent<Transform>();
		inChaseMode = false;
		movement = GetComponent<MovementController>();
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		StartCoroutine(ThinkAboutNewdirection());
	}


	// void OnTriggerEnter2D(Collider2D other) {
	// 	if(other.gameObject.tag.Equals("Player")) {
	// 		GetComponent<AttackScript>().beginAttack(this.gameObject, movement.directionVector);
	// 		movement.isAttacking = true;
	// 	}
	// }
	IEnumerator ThinkAboutNewdirection() {
		while(true) {
			if(!movement.knocked) {
				int direction = Random.Range(0, 4);
				switch (direction) {
					case 0:
						movement.directionVector.x = 1;
						movement.directionVector.y = 0;
						break;
					case 1:
						movement.directionVector.x = -1;
						movement.directionVector.y = 0;
						break;
					case 2:
						movement.directionVector.x = 0;
						movement.directionVector.y = 1;
						break;
					case 3:
						movement.directionVector.x = 0;
						movement.directionVector.y = -1;
						break;
				}
				movement.movementVector.Set(movement.directionVector.x, movement.directionVector.y);
			}
			yield return new WaitForSeconds(Random.Range(2, 5));
		}
	}
	IEnumerator ChasePlayer() {
		while(true) {
			if(pTransform == null) {
				break;
			}
			Vector2 playerPos = new Vector2(pTransform.position.x, pTransform.position.y);

			movement.directionVector.Set(playerPos.x - this.GetComponent<Transform>().position.x,
											 playerPos.y - this.GetComponent<Transform>().position.y);
			movement.directionVector.Normalize();
			movement.movementVector.Set(movement.directionVector.x, movement.directionVector.y);

			Vector2 distanceVector = new Vector2(Mathf.Abs(playerPos.x - this.GetComponent<Transform>().position.x),
											 Mathf.Abs(playerPos.y - this.GetComponent<Transform>().position.y));

			yield return new WaitForSeconds(Random.Range(1, 2));
		}
	}

	IEnumerator AttackPlayer() {
		while(true) {
			if(distanceVector.magnitude < attackReach && !movement.isAttacking) {
				GetComponent<AttackScript>().beginAttack(this.gameObject, movement.directionVector);
				movement.isAttacking = true;
			}
			yield return new WaitForSeconds(Random.Range(1, 3));
		}
	}

	void FixedUpdate () {
		if(pTransform ==null) {
			return;
		}
		Vector2 playerPos = new Vector2(pTransform.position.x, pTransform.position.y);
		distanceVector = new Vector2(Mathf.Abs(playerPos.x - this.GetComponent<Transform>().position.x),
											 Mathf.Abs(playerPos.y - this.GetComponent<Transform>().position.y));

		if(distanceVector.magnitude < sightRadius && !inChaseMode) {
			Debug.Log("Goni!");
			StopAllCoroutines();
			StartCoroutine(ChasePlayer());
			StartCoroutine(AttackPlayer());
			movement.movementSpeed = movement.movementSpeed * 1.5f;
			inChaseMode = true;
		} else if (distanceVector.magnitude > sightRadius && inChaseMode) {
			StopAllCoroutines();
			StartCoroutine(ThinkAboutNewdirection());
			StartCoroutine(AttackPlayer());
			inChaseMode = false;
			movement.movementSpeed = movement.movementSpeed / 1.5f;
			Debug.Log("Przestal!");
		}
	}
}
