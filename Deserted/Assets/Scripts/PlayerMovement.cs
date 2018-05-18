using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 30f;
	public float attackDistance = 16f;
	public Weapon attack;
	
	public GameObject armour;
	public bool isAttacking;

	Rigidbody2D rigidbody;
	Animator animator;

	// Use this for initialization
	void Start () {
		isAttacking = false;
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 directionVector = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		directionVector.Normalize();
		if(movementVector != Vector2.zero) {
			animator.SetBool("is_walking", true);
			armour.GetComponent<Animator>().SetBool("is_walking", true);
		} else {
			animator.SetBool("is_walking", false);
			armour.GetComponent<Animator>().SetBool("is_walking", false);
		}
		if(Input.GetMouseButtonDown(0) && !isAttacking) {
			// attackObject.GetComponent<ReSkinAnimation>().spriteSheetName = attack.name;
			GetComponent<AttackScript>().beginAttack(this.gameObject, directionVector);
			isAttacking = true;
		}
		if(Input.GetKeyDown("escape")) {
			Application.Quit();
		}
		animator.SetBool("is_attacking", isAttacking);
		armour.GetComponent<Animator>().SetBool("is_attacking", isAttacking);

		if(!isAttacking) {
			animator.SetFloat("input_x", directionVector.x);
			animator.SetFloat("input_y", directionVector.y);
			armour.GetComponent<Animator>().SetFloat("input_x", directionVector.x);
			armour.GetComponent<Animator>().SetFloat("input_y", directionVector.y);
			rigidbody.MovePosition(rigidbody.position + movementVector*Time.deltaTime*playerSpeed);
		} else {
			rigidbody.MovePosition(rigidbody.position);
		}
	}
}
