using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
	private Rigidbody2D rb2d;
	public float scrollspeed = 0.05f;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (scrollspeed, 0);
	}
	
	// Update is called once per frame
	void Update () {

		rb2d.velocity = Vector2.zero;
	}
}
