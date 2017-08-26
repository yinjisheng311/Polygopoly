using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeSingle : MonoBehaviour {

	public void goHomeNow(){
		
		SceneManager.LoadScene ("Level0");
	}
}
