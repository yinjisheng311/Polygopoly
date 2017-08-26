using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToGreenButton : MonoBehaviour {

	public Button readyButton;

	public Sprite greenButtonImage;

	public void ChangeGreenNow(){
		readyButton.GetComponent<SpriteRenderer>().sprite = greenButtonImage;
	}
}
