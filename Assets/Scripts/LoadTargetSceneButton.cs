using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTargetSceneButton : MonoBehaviour {

	public void LoadSceneNum(int num){
		if (num < 0 || num >= SceneManager.sceneCountInBuildSettings) {
			Debug.LogWarning ("CANT LOAD SCENE " + num);
			return;
		}
		Debug.Log ("Running THIS NOW");
		LoadingScreenManager.LoadScene (num);
	}
}
