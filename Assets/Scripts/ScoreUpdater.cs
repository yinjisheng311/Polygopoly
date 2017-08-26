using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class ScoreUpdater : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updatePlayerScores(string calleeRef, string blobName)
    {
        Debug.Log("Updating player score...");
        Text counter = null;
		Text final = null;

        if (calleeRef.Equals("Player1"))
        {
            //counter = GameObject.Find("Count Text 1").GetComponent<Text>();
			final = GameObject.Find ("Score1").GetComponent<Text> ();
        }
        else if (calleeRef.Equals("Player2"))
        {
            //counter = GameObject.Find("Count Text 2").GetComponent<Text>();
			final = GameObject.Find ("Score2").GetComponent<Text> ();
        }
        else
        {
            Debug.Log("INVALID TAG, GO INVESTIGATE");
            Debug.Log("Tag given is : " + calleeRef);
        }
		int counterString = 0;
		string formattedString = "";
		/*
		foreach (char c in final.text) {
			if (counterString == 16) {
				formattedString += c.ToString ();
			}
			counterString++;
		}
		*/

		Match m = Regex.Match (final.text.Substring(16), "\\d+");
		formattedString += m.Value;
		Debug.Log (formattedString);

		int currentScore = 0;

		if (System.Int32.TryParse(formattedString, out currentScore))
        {
			if (blobName.Equals ("Level 1 Blob(Clone)")) {
				currentScore++;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}

			} else if (blobName.Equals ("Level 2 Blob(Clone)")) {
				currentScore += 2;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}
			} else if (blobName.Equals ("Level 3 Blob(Clone)")) {
				currentScore += 5;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}
			} else if (blobName.Equals ("Child Blob(Clone)")) {
				currentScore += 2;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}
			}
        }
        else
        {
            Debug.Log("Conversion to integer failed!");
        }
		int counter1 = 0;
		int counter2 = 0;
        //counter.text = currentScore.ToString();

		Text winnerText = GameObject.Find ("Winner").GetComponent<Text> ();

		if (calleeRef.Equals("Player1")){
			final.text = "P L A Y E R 1 : " + currentScore.ToString ();
			if (System.Int32.TryParse (currentScore.ToString (), out counter1)) {
				if (counter1 > counter2) {
					winnerText.text = "W I N N E R: P L A Y E R 1";

				} else if (counter1 < counter2) {
					winnerText.text = "W I N N E R: P L A Y E R 2";

				} else {
					winnerText.text = "I T ' S  A  T I E!";

				}
			}

		} else if (calleeRef.Equals("Player2")){
			final.text = "P L A Y E R 2 : " + currentScore.ToString ();
			if (System.Int32.TryParse (currentScore.ToString (), out counter2)) {
				if (counter1 > counter2) {
					winnerText.text = "W I N N E R: P L A Y E R 1";

				} else if (counter1 < counter2) {
					winnerText.text = "W I N N E R: P L A Y E R 2";

				} else {
					winnerText.text = "I T ' S  A  T I E!";

				}
			}

		}

    }
	void SetHighScore(int score){
		PlayerPrefs.SetInt ("Highscore", score);

	}

	
	public string getCurrentScore(string calleeRef)
	{
		Debug.Log("Getting player score...");
        Text score = null;

        if (calleeRef.Equals("Player1"))
        {
            score = GameObject.Find("Count Text 1").GetComponent<Text>();
        }
        else if (calleeRef.Equals("Player2"))
        {
            score = GameObject.Find("Count Text 2").GetComponent<Text>();
        }
        else
        {
            Debug.Log("INVALID TAG, GO INVESTIGATE");
            Debug.Log("Tag given is : " + calleeRef);
        }
		return score.text;
	}
}
