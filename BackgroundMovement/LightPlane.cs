using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlane : MonoBehaviour {

	[SerializeField] private Vector3 startPos;

	void Start () {
		
	}
	
	void Update () {
		PlaneMove();
	}

	void PlaneMove () {
		transform.Translate(Vector3.left * (4f * Time.deltaTime));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Respawn") {
			transform.position = startPos;
		}
	}
}
