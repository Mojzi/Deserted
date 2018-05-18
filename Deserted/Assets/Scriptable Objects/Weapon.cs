using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New", menuName = "New Attack")]
public class Weapon : Item {

	public int damage;
	public int reach;
	public int area;

	public string attackAnimation;

}
