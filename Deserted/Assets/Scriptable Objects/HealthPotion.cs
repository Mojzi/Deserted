using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion")]
public class HealthPotion : Item {

	public int healingPower = 50;

	public override void Use(GameObject user) {
		user.GetComponent<MovementController>().ChangeHealth(healingPower);
		Inventory.instance.Remove(this);
	}
}
