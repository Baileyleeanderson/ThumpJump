using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	private float speed = 4f;

	void Start () {
	}
	
	void Update () {
		BirdFly();
	}

	void BirdFly() {
		transform.Translate(Vector3.left * (speed * Time.deltaTime));
	}

}
