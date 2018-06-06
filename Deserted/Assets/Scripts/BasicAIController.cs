using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour {




	Rigidbody2D rigidbody;
	Animator animator;
	MovementController movement;

	// Use this for initialization
	void Start () {
		movement = GetComponent<MovementController>();
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		// directionVector = new Vector2(0,0);
		// movementVector = new Vector2(0,0);
		StartCoroutine(ThinkAboutNewdirection());
	}

	// public void WasHit(int damage, Vector2 hitPosition) {
	// 	if(!immune) {
	// 		knocked = true;
	// 		immune = true;
	// 		currentHealth -= damage;
	// 		Debug.Log("Damaged! damageDone:" + damage + " healthLeft: " +currentHealth);
	// 		GetComponent<SpriteRenderer>().color = Color.red;
	// 		Invoke("Damaged", 1f);
	// 		Invoke("KnockedBack", 0.1f);
	// 		movementVector = new Vector2(transform.position.x, transform.position.y) - hitPosition;
	// 		movementVector = movementVector.normalized;
	// 		movementSpeed = movementSpeed * 10;
	// 	}
	// }

	// void KnockedBack() {
	// 	knocked = false;
	// 	movementSpeed = movementSpeed/10;
	// }

	// void Damaged() {
	// 	GetComponent<SpriteRenderer>().color = Color.white;
	// 	immune = false;
	// 	if(currentHealth <= 0) {
	// 		Destroy(this.gameObject);
	// 	}
	// }

	// Update is called once per frame

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			float x = other.GetComponent<Transform>().position.x;
			float y = other.GetComponent<Transform>().position.y;
			Vector2 direction = new Vector2(x - movement.movementVector.x, y -  movement.movementVector.y);
			if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
				if(direction.x > 0) {
					direction.x = 1.0f;
					direction.y = 0.0f;
				} else {
					direction.x = -1.0f;
					direction.y = 0.0f;
				}
			} else {
				if(direction.y > 0) {
					direction.x = 0.0f;
					direction.y = 1.0f;
				} else {
					direction.x = 0.0f;
					direction.y = -1.0f;
				}
			}
			GetComponent<AttackScript>().beginAttack(this.gameObject, direction);
			movement.isAttacking = true;
		}
	}
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
	void FixedUpdate () {
		

		// Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		// Vector2 directionVector = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		// movement.directionVector.Normalize();
		// if(movement.movementVector != Vector2.zero) {
		// 	animator.SetBool("is_walking", true);
		// } else {
		// 	animator.SetBool("is_walking", false);
		// }

		// if(!movement.knocked) {
		// 	animator.SetFloat("input_x", directionVector.x);
		// 	animator.SetFloat("input_y", directionVector.y);
		// }
		// if(!(!knocked && immune)) {
		// 	rigidbody.MovePosition(rigidbody.position + movementVector*Time.deltaTime*movementSpeed);
		// }
	}
}
