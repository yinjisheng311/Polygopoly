using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SingleplayerClick : MonoBehaviour {

	public void LoadSingleplayer(){
		StartCoroutine(FadingFunction ());
		Debug.Log ("THIS HAPPENED");
		
	}

	public IEnumerator FadingFunction(){
		float fadeTime = GameObject.Find ("Canvas").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

		Debug.Log (fadeTime);

		//not sure why it is not working
		SceneManager.LoadScene ("Level1S");
	}

}

