using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData {

	private int highScore;

	public int HighScore {
		get {
			return highScore;
		}
		set {
			highScore = value;
		}
	}

} // class


































