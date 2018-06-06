using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New", menuName = "New Attack")]
public class Weapon : Item {

	public enum WeaponType{MEELE, RANGED};

	public int damage;
	public int reach;
	public int area;
	public WeaponType type;

	public string attackAnimation;

}
