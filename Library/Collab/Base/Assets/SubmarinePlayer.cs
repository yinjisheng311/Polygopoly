using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;


public class SubmarinePlayer : NetworkBehaviour {
	public float moveForce = 5;
	public Sprite image;
	public Rigidbody2D myBody;

	Transform mainCamera; 		// Reference to the main scene's main camera
	public float cameraDistance = 0.5f; // Distance camera is away from player's submarine
	Vector3 cameraOffset; 		// Vector desecribing level of zoom of camera on player

	void Start () {
		
		if (!isLocalPlayer) {
			return;
		}

		myBody = this.GetComponent<Rigidbody2D> ();

		// Sets up camera offset
		cameraOffset = new Vector3 (0f, 0f, cameraDistance);

		//Find the main camera and move it into the correct position
		mainCamera = Camera.main.transform;
		MoveCamera ();
	}
	
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		
	}

	public override void OnStartLocalPlayer(){
		GetComponent<SpriteRenderer> ().sprite = image;
	}

	void FixedUpdate(){
		if (!isLocalPlayer) {
			return;
		}

		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * moveForce;
		myBody.AddForce (moveVec);

		//Update camera position
		MoveCamera();
	}

	// Moves Camera to the correct spot to center on current player
	void MoveCamera() {

		if (!isLocalPlayer) {
			return;
		}

		mainCamera.position = transform.position;	// Positions camera on the player's submarine
		mainCamera.Translate(cameraOffset);			// Zooms camera away from player
		mainCamera.LookAt(transform);				// Make Camera look at Player's submarine
	}
}
