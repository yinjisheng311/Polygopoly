using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChangeVolume : MonoBehaviour {

	public Slider volume;
	public AudioSource myMusic;
	void Start(){

	}

	void Update () {
		myMusic.volume = volume.value;
		PlayerPrefs.SetFloat ("Volume",myMusic.volume);
	}
}
