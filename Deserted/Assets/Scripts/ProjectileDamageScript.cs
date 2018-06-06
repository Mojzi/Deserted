using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamageScript : MonoBehaviour
{

    // public Weapon weapon;

    public GameObject attacker;
    public Vector2 attackDirection;
    public float projectileSpeed = 200f;
	float horizontalOffset = 0;
	float verticalOffset = 0;
	Vector3 attackPosition;
    Rigidbody2D rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(GameObject attacker, Vector2 directionVector) {
        this.attacker = attacker;
        attackDirection = new Vector2(0,0);
		if(Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y)) {
			if(directionVector.x > 0) {
				//Prawo
				attackDirection.x = 1f;
				this.transform.Rotate(0,0,-90);
				this.transform.localScale = new Vector3(1, 1, -1);
			} else {
				//Lewo
				attackDirection.x = -1f;
				this.transform.Rotate(0,0,90);
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
				this.transform.localScale = new Vector3(1, 1, -1);
			} else {
				//Dół
				horizontalOffset = 4;
				verticalOffset = 4;
				attackDirection.y = -1f;
				// attackAnimation.transform.Rotate(0,0,180);
				this.transform.localScale = new Vector3(-1, -1, 1);
				this.GetComponent<IsometricSpriteRenderer>().offset += 500;
			}
		}
		 attackPosition = new Vector3(attacker.GetComponent<Rigidbody2D>().position.x + attackDirection.x+horizontalOffset,
														attacker.GetComponent<Rigidbody2D>().position.y + attackDirection.y+8+verticalOffset);
		this.transform.position = attackPosition;
		this.GetComponent<Renderer>().sortingOrder = (int)(this.transform.position.y * -10);
        Invoke("SelfDestruct", 3f);
    }

    void FixedUpdate() {
			rigidbody.MovePosition(rigidbody.position + attackDirection*Time.deltaTime*projectileSpeed);
    }

    void SelfDestruct() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacker.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Gracz trafil cos");
            if (collision.gameObject.tag.Equals("Critter"))
            {
                int damage = attacker.GetComponent<MovementController>().attack.damage;
                Vector2 attackerPosition = new Vector2(attacker.GetComponent<Transform>().position.x, attacker.GetComponent<Transform>().position.y);
                // collision.gameObject.GetComponent<Animator>().SetBool("was_hit", true);
                collision.gameObject.GetComponent<MovementController>().WasHit(damage, attackerPosition);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.Log("Cos trafilo gracza");
            if (collision.gameObject.tag.Equals("Player"))
            {
                int damage = attacker.GetComponent<MovementController>().attack.damage;
                Vector2 attackerPosition = new Vector2(attacker.GetComponent<Transform>().position.x, attacker.GetComponent<Transform>().position.y);
                collision.gameObject.GetComponent<MovementController>().WasHit(damage, attackerPosition);
                Destroy(this.gameObject);
            }
        }
    }


}
