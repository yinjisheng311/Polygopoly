using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;


public class SubmarinePlayer : NetworkBehaviour {
	public bool stealthmode = false;
	public float moveForce = 5;
	public Sprite image;
	public Rigidbody2D myBody;
	public bool facingright;

	Transform mainCamera; 		        // Reference to the main scene's main camera
	public float cameraDistance = 0.5f; // Distance camera is away from player's submarine
	Vector3 cameraOffset; 		        // Vector desecribing level of zoom of camera on player

	void Start () {
		print(GetInstanceID());
		if (!isLocalPlayer) {
            // This will destroy this script if the current player is not a local player, 
            // so this script will never run for non-local players
            //Destroy(this);
			return;
		}
		//bulletSpawn = transform.FindChild ("FirePos");
		this.facingright = true;



		myBody = this.GetComponent<Rigidbody2D> ();

		// Sets up camera offset for use later when changing camera position relative to the player's submarine
		cameraOffset = new Vector3 (0f, 0f, 0f);

		//Find the main camera and move it into the correct position
		mainCamera = Camera.main.transform;
		MoveCamera ();
	}
	void Update(){

	}

	


	public override void OnStartLocalPlayer(){
		GetComponent<SpriteRenderer> ().sprite = image;
	}

	void FixedUpdate(){
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * moveForce;

		float move = Input.GetAxis ("Horizontal");
		if (moveVec.x > 0 && !facingright) {

			flip ();
		} else if (moveVec.x < 0 && facingright) {
			flip ();
		}
		if (!isLocalPlayer) {
			// This will destroy this script if the current player is not a local player, 
			// so this script will never run for non-local players
			//Destroy(this);
			return;
		}

		myBody.AddForce (moveVec);



		//Update camera position
		MoveCamera();
	}
	public void flip(){
		this.facingright = !this.facingright;
		Vector3 thescale = transform.localScale;
		thescale.x *= -1;
		transform.localScale = thescale;
	}

	// Moves Camera to the correct spot to center on current player
	void MoveCamera() {

        cameraOffset = transform.position;          // Gets Player's current Position
        cameraOffset.z -= cameraDistance;           // then add a fixed offset away from the player for zoom
        mainCamera.position = cameraOffset;         // And set camera location to this new location to enable camera to move as player moves
		mainCamera.LookAt(transform);				// Make Camera look at Player's submarine
	}
}
