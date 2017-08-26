using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettingsScript : MonoBehaviour {

	public GameObject SettingsPanel;

	public void CloseSettingsNow(){
		SettingsPanel.gameObject.SetActive (false);
	}
}
