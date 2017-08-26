using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour {

	public GameObject ListPanel;
	public RectTransform mainPanel;

	public void RemoveListPanel(){
		PLobbyManager.lobbySingleton.ChangeTo (mainPanel);
		//ListPanel.gameObject.SetActive (false);

	}
}
