using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;


public class SubmarinePlayer : NetworkBehaviour {
	public float moveForce = 5;
	public Sprite image;
	public Rigidbody2D myBody;

	//Transform mainCamera; 		// Reference to the main scene's main camera
	//public float cameraDistance = 0.5f; // Distance camera is away from player's submarine
	//Vector3 cameraOffset; 		// Vector desecribing level of zoom of camera on player

	void Start () {
		
		if (!isLocalPlayer) {
			return;
		}

		myBody = this.GetComponent<Rigidbody2D> ();


	}
	
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		
	}

	public override void OnStartLocalPlayer(){
		//Camera.main.GetComponent<CameraFollow> ();

		GetComponent<SpriteRenderer> ().sprite = image;
	}

	void FixedUpdate(){
		if (!isLocalPlayer) {
			return;
		}

		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * moveForce;
		myBody.AddForce (moveVec);


	}


}
