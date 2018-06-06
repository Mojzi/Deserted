using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

	// Use this for initialization
	public GameObject destination;
	public Camera camera;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			other.GetComponent<Transform>().position = destination.GetComponent<Transform>().position;
			camera.GetComponent<Transform>().position = destination.GetComponent<Transform>().position;
		}
	}
}
