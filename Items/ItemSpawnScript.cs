using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnScript : MonoBehaviour {

	[SerializeField]
	private GameObject[] items;
	private GameObject spawnedItem;

	

	void Start () {
		SpawnItem();
	}
	
	void Update () {
		
	}

	void SpawnItem() {
		var randm = Random.Range(0, 11);
		
		if (randm == 1 || randm == 5) {
			spawnedItem = Instantiate(items[randm], new Vector3(transform.position.x, 5f, 0), Quaternion.identity, transform.parent);	
		}
		else if (randm == 3) {
			spawnedItem = Instantiate(items[randm], new Vector3(transform.position.x, -1.75f, 0), Quaternion.identity, transform.parent);	
		}
		else if (randm == 4 || randm == 8) {
			spawnedItem = Instantiate(items[randm], new Vector3(transform.position.x, -2f, 0), Quaternion.identity, transform.parent);	
		}
		else if (randm == 9 || randm == 10) {
			spawnedItem = Instantiate(items[randm], new Vector3(transform.position.x, 5f, 0), Quaternion.identity, transform.parent);	
		}
		else {
			spawnedItem = Instantiate(items[randm], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform.parent);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Respawn") {
			Destroy(this.spawnedItem);
			SpawnItem();
		}
	}
}
