using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeMusic : MonoBehaviour {

	[SerializeField] 
	private AudioClip[] songs;
	private AudioSource audioSrc;

	private  bool canPlayGameOverMusic = true;
	
	void Start () {
		audioSrc = GetComponent<AudioSource>();
		audioSrc.clip = songs[0];
		audioSrc.Play();
	}
	
	
	void Update () {
		if (!GameManager.Instance.PlayerIsAlive && canPlayGameOverMusic) {
			audioSrc.Stop();
			audioSrc.clip = songs[1];
			if (canPlayGameOverMusic == true) {
				audioSrc.Play();
				canPlayGameOverMusic = false;
			}
		}
	}
}
