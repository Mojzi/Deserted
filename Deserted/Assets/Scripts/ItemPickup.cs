using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;


	void onCreate() {
		GetComponent<ReSkinAnimation>().name = item.name;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			Debug.Log("Podniesiono ");
			if(Inventory.instance.Add(item)) {
				// other.gameObject.GetComponent<MovementController>().attack = attack;
				Destroy(this.gameObject);
			}
		}
	}
}
