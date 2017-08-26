using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBirthdayWishes : MonoBehaviour {

	public Text birthdayWishes;

	void Update () {
		if (Time.timeSinceLevelLoad < 5.0f) {
			birthdayWishes.text = "Hi Emily! How's studying in Australia? Time passed so fast!\n It's been 2 years since you left Singapore for your studies. Hopefully by the end of this year you will graduate with your desired results :)\n Do update your life every now and then so that we know what you have been up to! While you're studying hard,  make sure to take a break once in a while.\n Lastly, I wish you a HAPPY 21st BIRTHDAY! PARTY PARTY PARTY! ";
		} else if (Time.timeSinceLevelLoad > 5.0f && Time.timeSinceLevelLoad < 10.0f) {
			birthdayWishes.text = "b";
		}
		else if (Time.timeSinceLevelLoad > 10.0f && Time.timeSinceLevelLoad < 15.0f) {
			birthdayWishes.text = "cd";
		}
		else if (Time.timeSinceLevelLoad > 15.0f && Time.timeSinceLevelLoad < 20.0f) {
			birthdayWishes.text = "ef";
		}
		else if (Time.timeSinceLevelLoad > 20.0f && Time.timeSinceLevelLoad < 25.0f) {
			birthdayWishes.text = "";
		}
		else if (Time.timeSinceLevelLoad > 25.0f && Time.timeSinceLevelLoad < 30.0f) {
			birthdayWishes.text = "";
		}
		else if (Time.timeSinceLevelLoad > 30.0f && Time.timeSinceLevelLoad < 35.0f) {
			birthdayWishes.text = "";
		}
		else if (Time.timeSinceLevelLoad > 30.0f && Time.timeSinceLevelLoad < 35.0f) {
			birthdayWishes.text = "";
		}
		else {
			birthdayWishes.text = "";
		}
	}
}
