using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {

	private float xPos = 0;
	private bool dangerCanActivate = true;

	void Start () {
		
	}

	void Update () {
		CheckPosition();
		IsPlayerDead();
	}

	public void PlayerWasHit() {
		xPos = xPos - 1f;
		transform.position = new Vector3 (xPos, transform.position.y, transform.position.z);
	}

	public void PlayerHasSpecial() {
		xPos = xPos + 1f;
		transform.position = new Vector3 (xPos, transform.position.y, transform.position.z);
		if (transform.position.x > -9.8f) {
			GameManager.Instance.DangerIsActive = false;
			GameManager.Instance.DangerActive();
		}
	}

	void CheckPosition () {
		
		if (transform.position.x <= -9.83f) {
			if (dangerCanActivate) {
				GameManager.Instance.DangerIsActive = true;
				GameManager.Instance.DangerActive();
				dangerCanActivate = false;
			}
		}
		else if (transform.position.x >= 2.14f) {
			transform.position =  new Vector3(2.14f, transform.position.y, transform.position.z);
		}
	}

	void IsPlayerDead() {
		if (transform.position.x <= -13.99f) {
			GameManager.Instance.PlayerIsAlive = false;
			// Destroy(gameObject);
		}
	}
}
