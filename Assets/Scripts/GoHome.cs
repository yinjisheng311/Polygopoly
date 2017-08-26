using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour {
	public GameObject MainPanel;

	public void goHomeNow(){
		//Destroy (gameObject);
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			if (o.name.Equals("MainCamera")){
				//do nothing
			}
			else {
				Destroy(o);
			}
		}
		//PLobbyManager.lobbySingleton.StopHost();
		SceneManager.LoadScene ("Level0");
		//MainPanel.gameObject.SetActive (false);

	}
}
