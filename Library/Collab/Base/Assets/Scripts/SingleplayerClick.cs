using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SingleplayerClick : MonoBehaviour {

	public void LoadSingleplayer(){
		SceneManager.LoadScene ("Lobby");
	}
}
