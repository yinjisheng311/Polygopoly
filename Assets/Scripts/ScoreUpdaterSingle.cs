using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class ScoreUpdaterSingle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateScores(string calleeRef,string blobName)
    {
        Debug.Log("Updating player score...");
        Text counter = null;

        if (calleeRef.Equals("Player1"))
        {
            counter = GameObject.Find("Count Text 1").GetComponent<Text>();
        }
        
        else
        {
            Debug.Log("INVALID TAG, GO INVESTIGATE");
            Debug.Log("Tag given is : " + calleeRef);
        }
        int currentScore = 0;
        if (System.Int32.TryParse(counter.text, out currentScore))
        {
			if (blobName.Equals ("Blob_level1_Ryan(Clone)")) {
				currentScore++;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}

			} else if (blobName.Equals ("Blob_level1 1_Ryan(Clone)")) {
				currentScore += 2;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}
			} else if (blobName.Equals ("Blob_level1 1 1_Ryan(Clone)")) {
				currentScore += 5;
				if (currentScore > PlayerPrefs.GetInt ("Highscore", 0)) {
					SetHighScore (currentScore);
				}
			} else if (blobName.Equals ("Child Blob_RyanClone)")) {
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

        counter.text = currentScore.ToString();
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
        
        else
        {
            Debug.Log("INVALID TAG, GO INVESTIGATE");
            Debug.Log("Tag given is : " + calleeRef);
        }
		return score.text;
	}
}