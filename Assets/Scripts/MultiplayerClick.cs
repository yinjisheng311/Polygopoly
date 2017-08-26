using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MultiplayerClick : MonoBehaviour {

	public void LoadMultiplayer(){

		if (PLobbyManager.lobbySingleton != null) {
			PLobbyManager.lobbySingleton.SetMainPanelActive ();

		} 
		SceneManager.LoadScene ("Level0M");

	}
}
