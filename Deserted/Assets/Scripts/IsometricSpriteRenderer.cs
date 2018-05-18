using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricSpriteRenderer : MonoBehaviour {

	// Kawałek kodu, który znalazłem na prezentacji. Dynamicznie zmienia sortowanie sprajtów w zależności od ich pozycji na ekranie
	// Czyli generalnie można podejść do kamienia od spodu i być "przed nim" (widocznym), albo podejść od góry i być "za nim".
	// Normalnie Unity by działało tak, że zawsze byłoby się albo pod, albo nad innym sprajtem, tak że te kamyki na mapie
	// Bardziej przypominałyby dywan (nad), albo jakieś wiszące na suficie coś (pod) i musiałbym cały kamyk otoczyć colliderem
	// Offset dodałem ja, żeby można było użyć tego skryptu w "wielosprajtowych obiektach", jak gracz, gdzie jest bazowy sprite
	// a nad nim pancerz

	public int offset = 0;
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10) + offset;
	}
}
