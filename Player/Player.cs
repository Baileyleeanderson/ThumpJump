using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private Animator anim;
	private float xPos = 0;
	private PlayerPosition playerPos;
	private bool burgerSpecialActivated;
	public int burgerSpecialNum = 0;

	[SerializeField] 
	private GameObject thumpInvert;
	[SerializeField] 
	private GameObject jumpInvert;
	[SerializeField] 
	private GameObject burgerSpecialButton;
	[SerializeField]
	private GameObject burgerSpecialPanel;
	[SerializeField] 
	private GameObject damagePanel;

	[SerializeField]
	private AudioClip jumpFx;
	[SerializeField]
	private AudioClip thumpFx;
	[SerializeField]
	private AudioClip hiHurtFx;
	[SerializeField]
	private AudioClip lowHurtFx;
	[SerializeField] 
	private AudioClip burgerSpecFx;
	[SerializeField] 
	private AudioClip playButtonFx;
	[SerializeField] 
	private AudioClip burgSpecCollideFx;
	[SerializeField] 
	private AudioClip burgSpecActivateFx;
	[SerializeField] 
	private AudioClip burgSpecActivate2Fx;
	[SerializeField] 
	private AudioClip burgSpecActivate3Fx;

	private AudioSource audioSrc;
	private BoxCollider2D box2D;

	private bool specialPanelIsActive;

	void Start () {
		anim = GetComponent<Animator>();
		playerPos = GameObject.Find("PlayerParent").GetComponent<PlayerPosition>();
		GameManager.Instance.PlayerIsAlive = true;
		audioSrc = GetComponent<AudioSource>();
		box2D = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
		Movement();
		BurgerSpecialActivate();
		CheckIfSpecialPanelIsActive ();
		if (!GameManager.Instance.PlayerIsAlive) {
			box2D.enabled = false;
		}
	}

	void Movement() {
		if (!burgerSpecialActivated) {
			if (Input.GetKeyDown(KeyCode.W) || CrossPlatformInputManager.GetButtonDown("Jump")) {
				anim.SetTrigger("Jump");
				jumpInvert.SetActive(true);
				audioSrc.volume = .2f;
				audioSrc.PlayOneShot(jumpFx);
			}
			if (Input.GetKeyUp(KeyCode.W) || CrossPlatformInputManager.GetButtonUp("Jump")) {
				jumpInvert.SetActive(false);
			}
			if (Input.GetKeyDown(KeyCode.S) || CrossPlatformInputManager.GetButtonDown("Thump")) {
				anim.SetTrigger("Thump");
				thumpInvert.SetActive(true);
				audioSrc.volume = .2f;
				audioSrc.PlayOneShot(thumpFx);
			}
			if (Input.GetKeyUp(KeyCode.S) || CrossPlatformInputManager.GetButtonUp("Thump")) {
				thumpInvert.SetActive(false);
			}
			if (CrossPlatformInputManager.GetButtonDown("Play")) {
				SceneManager.LoadScene("Game");
				audioSrc.volume = .2f;
				audioSrc.PlayOneShot(playButtonFx);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (burgerSpecialActivated) {
			if (other.tag == "ItemHigh" || other.tag == "ItemLow"){
				GameManager.Instance.score += 200;
				GameManager.Instance.ShowFriesBonus();
				GameManager.Instance.IncreaseScore();
				playerPos.PlayerHasSpecial();
				audioSrc.PlayOneShot(burgSpecCollideFx);
			}
		}
		else if (!burgerSpecialActivated){
			if (other.tag == "ItemHigh") {
				playerPos.PlayerWasHit();
				anim.SetTrigger("ThumpHurt");
				damagePanel.SetActive(true);
				Invoke("DamagePanel", 0.15f);
				audioSrc.volume = .2f;
				audioSrc.PlayOneShot(hiHurtFx);
				
			}
			else if (other.tag == "ItemLow") {
				playerPos.PlayerWasHit();
				anim.SetTrigger("JumpHurt");
				damagePanel.SetActive(true);
				Invoke("DamagePanel", 0.15f);
				audioSrc.volume = .2f;
				audioSrc.PlayOneShot(lowHurtFx);
			}
			
		}
	}

	void BurgerSpecialActivate () {
		if (GameManager.Instance.BurgerSpecial) {
			burgerSpecialButton.SetActive(true);
			PlayerBurgerSpecialIncreasePos();

			if (CrossPlatformInputManager.GetButtonDown("BurgerSpecial")) {
				burgerSpecialActivated = true;
				anim.SetBool("BurgerSpecial", true);
				StartCoroutine(BurgerSpecialActiveTime());
				specialPanelIsActive = true;
				StartCoroutine(BurgerSpecialPanelBlink());
				GameManager.Instance.BurgerSpecial = false;
				burgerSpecialButton.SetActive(false);
				audioSrc.volume = .06f;
				audioSrc.PlayOneShot(burgerSpecFx);
				audioSrc.volume = .06f;
				audioSrc.PlayOneShot(burgSpecActivateFx);
				audioSrc.volume = .06f;
				audioSrc.PlayOneShot(burgSpecActivate2Fx);
				Invoke("PlaySpecialTune", .75f);
			}

		}
	}

	void PlaySpecialTune() {
		audioSrc.volume = .05f;
		audioSrc.PlayOneShot(burgSpecActivate3Fx);
		
	}

	void CheckIfSpecialPanelIsActive () {
		if (!specialPanelIsActive) {
			burgerSpecialPanel.SetActive(false);
		}
	}

	IEnumerator BurgerSpecialActiveTime () {
		yield return new WaitForSeconds(6f);
		burgerSpecialActivated = false;
		anim.SetBool("BurgerSpecial", false);
		burgerSpecialNum = 0;
		GameManager.Instance.BurgerSpecialNum = 0;
		specialPanelIsActive = false;
	}

	IEnumerator PlayerBurgerSpecialIncreasePos() {
		yield return new WaitForSeconds(2f);
		playerPos.PlayerHasSpecial();
		yield return new WaitForSeconds(2f);
		playerPos.PlayerHasSpecial();
		yield return new WaitForSeconds(2f);
		playerPos.PlayerHasSpecial();
	}

	IEnumerator BurgerSpecialPanelBlink() {
		while (specialPanelIsActive) {
			yield return new WaitForSeconds(.1f);
			burgerSpecialPanel.SetActive(true);
			yield return new WaitForSeconds(.1f);
			burgerSpecialPanel.SetActive(false);
		}
	}

	void DamagePanel() {
		damagePanel.SetActive(false);
	}

}
