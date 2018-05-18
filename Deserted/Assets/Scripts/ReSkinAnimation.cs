using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ReSkinAnimation : MonoBehaviour {

	// Kod z tej samej prezentacji, to IsometricSpriteRenderer.
	// Pozwala w locie dla jednej animacji podmieniać w locie spriteSheet z czym Unity ma normalnie problem
	// Dobre generalnie do tego, że nie trzeba robić osobnej animacji dla każdego sprajta postaci i pancerza
	// i można je łatwo w locie podmieniać przesyłając tylko nazwę obrazka

	public string spriteSheetName;

	void LateUpdate() {
		var subSprites = Resources.LoadAll<Sprite>("Graphics/" + spriteSheetName);

		foreach(var renderer in GetComponents<SpriteRenderer>()) {
			string spriteName = renderer.sprite.name;
			var newSprite = Array.Find(subSprites, item => item.name == spriteName);
			if(newSprite) {
				renderer.sprite = newSprite;
			}
		}
	}

}
