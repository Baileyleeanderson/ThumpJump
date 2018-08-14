using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

	[SerializeField] public float speed;
	[SerializeField] private float startPosition;
	[SerializeField] private float resetPosition;

	private float maxSpeed = 100f;
	private bool speedCanChange = true;

	void Start () {
		
	}
	

	void Update () {
		MoveRoad();
		RoadSpeedIncrease();
	}

	void MoveRoad() {
		transform.Translate(Vector3.left * (speed * Time.deltaTime));

		if (transform.localPosition.x <= resetPosition) {
			Vector3 newPos = new Vector3 (startPosition, transform.position.y, transform.position.z);
			transform.position = newPos;
		}
	}

	void RoadSpeedIncrease() {
		if (speed <= maxSpeed && speedCanChange) {
			StartCoroutine(IncreaseRoadSpeedTimer());
		}
	}

	IEnumerator IncreaseRoadSpeedTimer() {
		speedCanChange = false;
		yield return new WaitForSeconds(2f);
		speed += 1;
		speedCanChange = true;
	} 
}
