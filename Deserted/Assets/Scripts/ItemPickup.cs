using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Weapon attack;


	void onCreate() {
		GetComponent<ReSkinAnimation>().name = attack.name;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			Debug.Log("Podniesiono ");
			other.gameObject.GetComponent<PlayerMovement>().attack = attack;
			Destroy(this.gameObject);
		}
	}
}
