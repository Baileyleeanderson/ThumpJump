using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private GameObject dangerImage;
	[SerializeField]
	private GameObject bonus;
	[SerializeField]
	private GameObject friesImage;
	[SerializeField] 
	private GameObject gameOverPanel;
	[SerializeField] 
	private GameObject playButton;
	[SerializeField] 
	private GameObject scoreCollider;

	private int bonusActivatedNum = 0;

	public float score = 0;
	private bool playerIsAlive;
	private bool dangerIsActive = false;
	private bool bonusActivated = false;
	private bool burgerSpecial;
	private int burgerSpecialNum = 0;

	public bool PlayerIsAlive {
		get {
			return playerIsAlive;
		}

		set {
			playerIsAlive = value;
		}
	}

	public bool DangerIsActive {
		get {
			return dangerIsActive;
		}
		set {
			dangerIsActive = value;
		}
	}

	public int BonusActivatedNum {
		get {
			return bonusActivatedNum;
		}
		set {
			bonusActivatedNum = value;
		}
	}

	public bool BonusActivated {
		get {
			return bonusActivated;
		}
		set {
			bonusActivated = value;
		}
	}

	public bool BurgerSpecial {
		get {
			return burgerSpecial;
		}
		set {
			burgerSpecial = value;
		}
	}

	public int BurgerSpecialNum {
		get {
			return burgerSpecialNum;
		}
		set {
			burgerSpecialNum = value;
		}
	}

	public static GameManager instance;

	public static GameManager Instance{
		get {
			if(instance == null){

			}
			return instance;
		}
	}

	void Awake () {
		instance = this;
	}

	void Start () {
	
	}

	void Update () {
		if (playerIsAlive){
			IncreaseScore();
		}
		GameOver ();
		AddBonusPoints();
	}

	public void IncreaseScore () {
		score += Time.deltaTime * 7;
		scoreText.text = "" + score.ToString("F0");
	}

	public void DangerActive() {
		if (dangerIsActive) {
			StartCoroutine(DangerBlinkOnScreen());
		}
	}

	IEnumerator DangerBlinkOnScreen () {
		while (playerIsAlive && dangerIsActive) {
			yield return new WaitForSeconds(0.3f);
			dangerImage.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			dangerImage.SetActive(false);
			
		}
	}

	void GameOver () {
		if (!playerIsAlive) {
			Time.timeScale = .1f;
			gameOverPanel.SetActive(true);
			playButton.SetActive(true);
			scoreCollider.SetActive(false);
			if (SaveLoadScript.instance.HighScore < score) {
				SaveLoadScript.instance.HighScore = Mathf.FloorToInt(score);
				SaveLoadScript.instance.SaveGameData();
			}
		}
		else if (playerIsAlive) {
			Time.timeScale = 1f;
			gameOverPanel.SetActive(false);
			playButton.SetActive(false);
			scoreCollider.SetActive(true);
		}
	}

	void AddBonusPoints () {
		if (bonusActivatedNum >= 6) {
			score += 500;
			bonusActivatedNum = 0;
			bonusActivated = true;
			ShowBonusImage();
			burgerSpecialNum += 1;
			if (burgerSpecialNum >= 3) {
				burgerSpecial = true;
			}
		}
	}

	void ShowBonusImage () {
		StartCoroutine(ShowBonusImageBlink());
	}

	IEnumerator ShowBonusImageBlink () {
		while (bonusActivated) {
			yield return new WaitForSeconds(0f);
			bonus.SetActive(true);
			yield return new WaitForSeconds(0.4f);
			bonus.SetActive(false);
			yield return new WaitForSeconds(0.4f);
			bonus.SetActive(true);
			yield return new WaitForSeconds(0.4f);
			bonus.SetActive(false);
			bonusActivated = false;
		}
	}

	public void ShowFriesBonus () {
		StartCoroutine(FriesBonusShowTime());
	}

	IEnumerator FriesBonusShowTime () {
		yield return new WaitForSeconds(0f);
		friesImage.SetActive(true);
		yield return new WaitForSeconds(.12f);
		friesImage.SetActive(false);
		yield return new WaitForSeconds(.12f);
		friesImage.SetActive(true);
		yield return new WaitForSeconds(.12f);
		friesImage.SetActive(false);
	}
}
