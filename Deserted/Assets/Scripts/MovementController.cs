using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public Vector2 directionVector;
	public Vector2 movementVector;

	public float movementSpeed = 30f;

	public int healthTotal = 25;
	public int currentHealth = 25;

	public Weapon attack;

	public bool knocked = false;
	public bool immune = false;
	Animator animator;

	public bool isAttacking;

	Rigidbody2D rigidbody;
	public GameObject armour;

	public int weight = 1;

	void Start () {
		isAttacking = false;
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	public void WasHit(int damage, Vector2 hitPosition) {
		if(!immune) {
			knocked = true;
			immune = true;
			currentHealth -= damage;
			Debug.Log("Damaged! damageDone:" + damage + " healthLeft: " +currentHealth);
			GetComponent<SpriteRenderer>().color = Color.red;
			Invoke("Damaged", 1f);
			Invoke("KnockedBack", 0.1f);
			movementVector = new Vector2(transform.position.x, transform.position.y) - hitPosition;
			movementVector = movementVector.normalized;
			movementSpeed = (movementSpeed * 10)/weight;
		}
	}

	void KnockedBack() {
		knocked = false;
		movementSpeed = (movementSpeed/10)*weight;
	}

	void Damaged() {
		GetComponent<SpriteRenderer>().color = Color.white;
		immune = false;
		if(currentHealth <= 0) {
			Destroy(this.gameObject);
		}
	}
	void FixedUpdate () {
		

		// Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		// Vector2 directionVector = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		// directionVector.Normalize();
		if(movementVector != Vector2.zero) {
			animator.SetBool("is_walking", true);
			if(armour != null) {
				armour.GetComponent<Animator>().SetBool("is_walking", true);
			}
		} else {
			animator.SetBool("is_walking", false);
			if(armour != null) {
				armour.GetComponent<Animator>().SetBool("is_walking", false);
			}
		}

		// if(!isAttacking && !knocked) {
		// 	GetComponent<AttackScript>().beginAttack(this.gameObject, directionVector);
		// 	isAttacking = true;
		// }

		animator.SetBool("is_attacking", isAttacking);
		if(armour != null) {
			armour.GetComponent<Animator>().SetBool("is_attacking", isAttacking);
		}

		if(!knocked) {
			if(!isAttacking) {
				animator.SetFloat("input_x", directionVector.x);
				animator.SetFloat("input_y", directionVector.y);
				if(armour != null) {
					armour.GetComponent<Animator>().SetFloat("input_x", directionVector.x);
					armour.GetComponent<Animator>().SetFloat("input_y", directionVector.y);
				}
			} else {
				rigidbody.MovePosition(rigidbody.position);
			}
		}

		if(!(!knocked && immune) && !isAttacking) {
			rigidbody.MovePosition(rigidbody.position + movementVector*Time.deltaTime*movementSpeed);
		}
	}
}
