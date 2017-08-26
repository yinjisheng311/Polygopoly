using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfMessages : MonoBehaviour {

	public Text countText;
	public Text congratulations;

	void Update(){
		int currentScore = 0;
		System.Int32.TryParse (countText.text, out currentScore);
		if (currentScore > 0 && currentScore < 150) {
			congratulations.text = "U N L O C K E D: 1 / 4";
		}
		else if (currentScore >= 150 && currentScore < 300) {
			congratulations.text = "U N L O C K E D: 2 / 4";

		} else if (currentScore >= 300 && currentScore < 450) {
			congratulations.text = "U N L O C K E D: 3 / 4";


		} else if (currentScore >= 450) {
			congratulations.text = "U N L O C K E D: 4 / 4 ";

		}
	}
}
