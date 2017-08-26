using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SingleplayerClick : MonoBehaviour {

	public IEnumerator LoadSingleplayer(){
		float fadeTime = GameObject.Find ("Canvas").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

		Debug.Log (fadeTime);

		//not sure why it is not working
		SceneManager.LoadScene ("Lobby");
	}

}

