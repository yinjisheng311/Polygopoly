using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSettings : MonoBehaviour {
	public GameObject SettingsPanel;


	public void GoSettingsNow(){
		SettingsPanel.gameObject.SetActive (true);

	}
}
