using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelScript : MonoBehaviour {

	private GameObject MainPanel;
	private GameObject ListPanel;

	void Start()
	{
		MainPanel = GameObject.Find ("MainPanel");
		MainPanel.gameObject.SetActive (false);

	}

	public void ChangeToList(){
		MainPanel.gameObject.SetActive (true);
		ListPanel = GameObject.Find ("ServerListPanel");
		ListPanel.gameObject.SetActive (true);
	}
}
