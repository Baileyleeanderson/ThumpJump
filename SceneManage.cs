using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class SceneManage : MonoBehaviour {

	private AudioSource audioSrc;
	[SerializeField]
	private AudioClip playButtonFx;

	void Start () {
		audioSrc = GetComponent<AudioSource>();
	}
	

	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("PlayMainMenu")) {
			SceneManager.LoadScene("Game");
			audioSrc.PlayOneShot(playButtonFx);
		}
	}
}
