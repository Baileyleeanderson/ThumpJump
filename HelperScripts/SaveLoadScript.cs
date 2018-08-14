using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveLoadScript : MonoBehaviour {

	public static SaveLoadScript instance;
	private GameData gameData;
	private int highScore;

	[SerializeField]
	private Text highScoreText;

	public int HighScore {
		get {
			return highScore;
		}
		set {
			highScore = value;
		}
	}

	[HideInInspector]
	public int starScore, score_Count, selected_Index;

	[HideInInspector]
	public bool[] heroes;

	[HideInInspector]
	public bool playSound = true;

	private string data_Path = "GameData.dat";

	void Awake () {
		MakeSingleton ();

		InitializeGameData ();
	}

	void Start() {
//		print (Application.persistentDataPath + data_Path);
	}
	
	void MakeSingleton() {
		if (instance != null) {
			Destroy (gameObject);
		} else if(instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void InitializeGameData() {
		LoadGameData ();

		if (gameData == null) {
			// we are running our game for the first time
			// set up initial values
			highScore = 0;

			gameData = new GameData ();

			gameData.HighScore = highScore;

			highScoreText.text = "HighScore: " + highScore;

			SaveGameData ();

		}

	}

	public void SaveGameData() {
		FileStream file = null;

		try {

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Create(Application.persistentDataPath + data_Path);

			if(gameData != null) {

				gameData.HighScore = highScore;

				bf.Serialize(file, gameData);
			}

		} catch(Exception e) {
			
		} finally {
			if (file != null) {
				file.Close ();
			}
		}

	}

	void LoadGameData() {

		FileStream file = null;

		try {

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Open(Application.persistentDataPath + data_Path, FileMode.Open);

			gameData = (GameData)bf.Deserialize(file);

			if(gameData != null) {
				highScore = gameData.HighScore;
				highScoreText.text = "HighScore: " + highScore;
			}

		} catch(Exception e) {
			
		} finally {
			if (file != null) {
				file.Close ();
			}
		}
	}

} // class





































