using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour {
	public GameObject MainPanel;

	public void goHomeNow(){
		SceneManager.LoadScene ("Level0");
		MainPanel.gameObject.SetActive (false);
	}
}
