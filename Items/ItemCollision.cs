using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollision : MonoBehaviour {

	private BoxCollider2D collide;
	[SerializeField]
	private Sprite hitSprite;
	private SpriteRenderer spriteRenderer;
	private AudioSource audioSrc;
	
	void Start () {
		collide = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (this.name == "Bird") {
				collide.enabled = false;
				spriteRenderer.sprite = hitSprite;
				Invoke("DestroyBird", 0.2f);
				GameManager.Instance.BonusActivatedNum = 0;
				audioSrc.volume = .1f;
				audioSrc.PlayOneShot(audioSrc.clip);
			}
			else {
				collide.enabled = false;
				spriteRenderer.sprite = hitSprite;
				GameManager.Instance.BonusActivatedNum = 0;
				audioSrc.volume = .1f;
				audioSrc.PlayOneShot(audioSrc.clip);
			}	
		}	
		else if (other.tag == "Score") {
			GameManager.Instance.BonusActivatedNum += 1;
		}
		
	}

	void DestroyBird() {
		Destroy(this.gameObject);
	}
	
}
