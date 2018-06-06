using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCleanerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Clean", 3f);
	}
	
	void Clean() {
			Destroy(this.gameObject);
	}

}
