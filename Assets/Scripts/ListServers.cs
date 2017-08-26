using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListServers : MonoBehaviour {


	public GameObject ListPanel;

	void Start(){
	}

	public void ChangeToList(){
		
		ListPanel.gameObject.SetActive (true);
	}
}
