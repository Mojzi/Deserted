using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour {


	public float movementSpeed = 30f;

	public int healthTotal = 25;
	public int currentHealth = 25;

	Vector2 directionVector;
	Vector2 movementVector;

	Rigidbody2D rigidbody;
	Animator animator;
	public bool knocked = false;
	public bool immune = false;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		directionVector = new Vector2(0,0);
		movementVector = new Vector2(0,0);
		StartCoroutine(ThinkAboutNewdirection());
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
			movementSpeed = movementSpeed * 10;
		}
	}

	void KnockedBack() {
		knocked = false;
		movementSpeed = movementSpeed/10;
	}

	void Damaged() {
		GetComponent<SpriteRenderer>().color = Color.white;
		immune = false;
		if(currentHealth <= 0) {
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame

	IEnumerator ThinkAboutNewdirection() {
		while(true) {
			if(!knocked) {
				int direction = Random.Range(0, 4);
				switch (direction) {
					case 0:
						directionVector.x = 1;
						directionVector.y = 0;
						break;
					case 1:
						directionVector.x = -1;
						directionVector.y = 0;
						break;
					case 2:
						directionVector.x = 0;
						directionVector.y = 1;
						break;
					case 3:
						directionVector.x = 0;
						directionVector.y = -1;
						break;
				}
				movementVector.Set(directionVector.x, directionVector.y);
			}
			yield return new WaitForSeconds(Random.Range(2, 5));
		}
	}
	void FixedUpdate () {
		

		// Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		// Vector2 directionVector = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
		directionVector.Normalize();
		if(movementVector != Vector2.zero) {
			animator.SetBool("is_walking", true);
		} else {
			animator.SetBool("is_walking", false);
		}

		if(!knocked) {
			animator.SetFloat("input_x", directionVector.x);
			animator.SetFloat("input_y", directionVector.y);
		}
		if(!(!knocked && immune)) {
			rigidbody.MovePosition(rigidbody.position + movementVector*Time.deltaTime*movementSpeed);
		}
	}
}
