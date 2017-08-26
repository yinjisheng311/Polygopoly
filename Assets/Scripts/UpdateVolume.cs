using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVolume : MonoBehaviour {

	public AudioSource myMusic;

	void Start(){
		myMusic.volume = PlayerPrefs.GetFloat (("Volume"),1.0f);
	}
}
