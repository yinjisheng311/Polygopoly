using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNext : MonoBehaviour {

	public Sprite image1, image2, image3, image4;
	public GameObject birthdayImage;
	public Text pageNumber;
	int counter = 0;

	public void changeWish(){
		int currentHighscore = PlayerPrefs.GetInt ("SinglePlayerHighscore", 0);
		counter++;
		if (counter == 1 && currentHighscore >= 150) {
			birthdayImage.GetComponent<SpriteRenderer> ().sprite = image2;
			pageNumber.text = "2 / 4";

		} else if (counter == 2 && currentHighscore >= 300) {
			birthdayImage.GetComponent<SpriteRenderer> ().sprite = image3;
			pageNumber.text = "3 / 4";
		} else if (counter == 3 && currentHighscore >= 450) {
			birthdayImage.GetComponent<SpriteRenderer> ().sprite = image4;
			pageNumber.text = "4 / 4";
		} else {
			counter = 0;
			birthdayImage.GetComponent<SpriteRenderer> ().sprite = image1;
			pageNumber.text = "1 / 4";

		}
	}

}
