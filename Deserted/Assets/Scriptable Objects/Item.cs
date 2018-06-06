using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	public new string name = "New Item";
	public int value = 0;
	public Sprite icon = null;
	public bool isDefaultItem = false;


	public virtual void Use(GameObject user) {
		Debug.Log("Using" + name);
	}

}
