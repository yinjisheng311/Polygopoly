using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinitebackground : MonoBehaviour {

	public float scrollspeed = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (Time.time * scrollspeed, 0);
		GetComponent<Renderer>().material.mainTextureOffset = offset;﻿
	}
}
