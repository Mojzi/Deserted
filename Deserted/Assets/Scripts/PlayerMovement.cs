using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	
	// public GameObject armour;
	// public bool isAttacking;

	// Rigidbody2D rigidbody;
	// Animator animator;
	MovementController movement;

	// Use this for initialization
	void Start () {
		movement = GetComponent<MovementController>();
		// isAttacking = false;
		// rigidbody = GetComponent<Rigidbody2D> ();
		// animator = GetComponent<Animator> ();
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		if(EventSystem.current.IsPointerOverGameObject()) {
			movement.movementVector.Set(0,0);
			return;
		}
		movement.movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		movement.directionVector = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		movement.directionVector.Normalize();
		// if(movementVector != Vector2.zero) {
		// 	animator.SetBool("is_walking", true);
		// 	armour.GetComponent<Animator>().SetBool("is_walking", true);
		// } else {
		// 	animator.SetBool("is_walking", false);
		// 	armour.GetComponent<Animator>().SetBool("is_walking", false);
		// }
		if(Input.GetMouseButtonDown(0) && !movement.isAttacking) {
			// attackObject.GetComponent<ReSkinAnimation>().spriteSheetName = attack.name;
			GetComponent<AttackScript>().beginAttack(this.gameObject, GetComponent<MovementController>().directionVector);
			movement.isAttacking = true;
		}
		// animator.SetBool("is_attacking", isAttacking);
		// armour.GetComponent<Animator>().SetBool("is_attacking", isAttacking);

		// if(!isAttacking) {
		// 	animator.SetFloat("input_x", directionVector.x);
		// 	animator.SetFloat("input_y", directionVector.y);
		// 	armour.GetComponent<Animator>().SetFloat("input_x", directionVector.x);
		// 	armour.GetComponent<Animator>().SetFloat("input_y", directionVector.y);
		// 	rigidbody.MovePosition(rigidbody.position + movementVector*Time.deltaTime*movementSpeed);
		// } else {
		// 	rigidbody.MovePosition(rigidbody.position);
		// }
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
}
