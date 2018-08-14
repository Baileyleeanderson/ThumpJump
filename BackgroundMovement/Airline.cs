using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airline : MonoBehaviour {

	[SerializeField] private Vector3 startPos;

	void Start () {
		
	}
	
	void Update () {
		AirPlaneMove();
	}

	void AirPlaneMove () {
		transform.Translate(Vector3.left * (4f * Time.deltaTime));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Respawn") {
		Invoke("ResetPlane", 5);
		}
	}

	void ResetPlane() {
		transform.position = startPos;
	}
}
