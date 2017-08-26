using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalHighScore : MonoBehaviour {
	public Text highscore;

	void Start(){
		int currentHighscore = PlayerPrefs.GetInt ("SinglePlayerHighscore", 0);
		//format the score so that each digit is separated by a space, purely for looks
		string highscoreInString = currentHighscore.ToString ();
		string result = "";
		foreach (char c in highscoreInString) {
			result += c.ToString () + " ";
		}
		Debug.Log (result);

		highscore.text = "H I G H S C O R E :  " + result;
	}
}
