using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriuseShip : MonoBehaviour {

	[SerializeField] private Vector3 startPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CruiseShipMove();
	}

	void CruiseShipMove () {
		transform.Translate(Vector3.left * (2f * Time.deltaTime));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Respawn") {
			transform.position = startPos;
		}
	}
}
