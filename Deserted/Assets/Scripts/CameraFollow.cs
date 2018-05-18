using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		camera.orthographicSize = (Screen.height / 3f) / 4f;

		if(target) {
			transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0,0, -10);
		}
	}
}
