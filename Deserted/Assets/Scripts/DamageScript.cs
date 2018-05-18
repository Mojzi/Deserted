using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

	// public Weapon weapon;

        public GameObject attacker;

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Trafiono cos");
		if(collision.gameObject.tag.Equals("Critter")) {
                        int damage = attacker.GetComponent<PlayerMovement>().attack.damage;
                        Vector2 attackerPosition = new Vector2(attacker.GetComponent<Transform>().position.x, attacker.GetComponent<Transform>().position.y);
			// collision.gameObject.GetComponent<Animator>().SetBool("was_hit", true);
                        collision.gameObject.GetComponent<BasicAIController>().WasHit(damage, attackerPosition);
		}
	}
	

}
