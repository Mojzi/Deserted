using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	public GameObject attackObject;
	GameObject attackAnimation;
	GameObject attacker;
	float horizontalOffset = 0;
	float verticalOffset = 0;

	public void beginAttack(GameObject attacker, Vector2 directionVector) {
		this.attacker = attacker;
		attackAnimation= Instantiate(attackObject);
			attackAnimation.GetComponent<DamageScript>().attacker = this.attacker;
			attackAnimation.GetComponent<ReSkinAnimation>().spriteSheetName = attacker.GetComponent<PlayerMovement>().attack.attackAnimation;
		Vector2 attackDirection = new Vector2(0,0);
		if(Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y)) {
			if(directionVector.x > 0) {
				//Prawo
				attackDirection.x = 1f;
				attackAnimation.transform.Rotate(0,0,-90);
				attackAnimation.transform.localScale = new Vector3(1, 1, -1);
			} else {
				//Lewo
				attackDirection.x = -1f;
				attackAnimation.transform.Rotate(0,0,90);
				horizontalOffset = 2;
				verticalOffset = 2;
				// attackAnimation.transform.Rotate(0,0,180);
			}
		} else {
			if(directionVector.y > 0) {
				//Góra
				attackDirection.y = 1f;
				horizontalOffset = -4;
				// verticalOffset = -4;
				attackAnimation.transform.localScale = new Vector3(1, 1, -1);
			} else {
				//Dół
				horizontalOffset = 4;
				verticalOffset = 4;
				attackDirection.y = -1f;
				// attackAnimation.transform.Rotate(0,0,180);
				attackAnimation.transform.localScale = new Vector3(-1, -1, 1);
				attackAnimation.GetComponent<IsometricSpriteRenderer>().offset += 500;
			}
		}
		attackAnimation.transform.position = new Vector3(attacker.GetComponent<Rigidbody2D>().position.x + attackDirection.x+horizontalOffset,
														attacker.GetComponent<Rigidbody2D>().position.y + attackDirection.y+8+verticalOffset);
		attackAnimation.GetComponent<Renderer>().sortingOrder = (int)(attackAnimation.transform.position.y * -10);

		Invoke("RemoveAttackAnimation", .5f);
	}

	void RemoveAttackAnimation() {
		if(attackAnimation != null)
			Destroy(attackAnimation);
			attacker.GetComponent<PlayerMovement>().isAttacking = false;
	}
}
