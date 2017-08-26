using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LaserController : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject hit = col.gameObject;
		if(hit.GetComponent<FiringJoystickCommand>() != null)
		{
			FiringJoystickCommand fire = hit.GetComponent<FiringJoystickCommand>();
			
			fire.laser = true;
			Destroy(this.gameObject);   // Destroys the potion.
		}
	}
	
	
	
}
