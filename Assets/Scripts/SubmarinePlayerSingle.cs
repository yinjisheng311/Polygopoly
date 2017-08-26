using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class SubmarinePlayerSingle : MonoBehaviour {
	public float moveForce = 5;
	public Sprite image;
	Rigidbody2D myBody;
	public bool facingright;
	public Image damageImage;
	public Color flashcolor = new Color (1f, 0f, 0f, 0.1f);
	bool damage;
	float shaketimer;
	float shakeintensity;

	Transform mainCamera; 		        // Reference to the main scene's main camera
	public float cameraDistance = 0.5f; // Distance camera is away from player's submarine
	Vector3 cameraOffset; 		        // Vector desecribing level of zoom of camera on player

    void Start () {

		GameObject damageImageObject = GameObject.FindGameObjectsWithTag ("DamageImage")[0];
		Debug.Log (damageImageObject.tag);
		damageImage = damageImageObject.GetComponent<Image> ();


		


        Debug.Log("Submarine Player name is : " + this.transform.name);

        //bulletSpawn = transform.FindChild("FirePos");
        this.facingright = true;

		myBody = this.GetComponent<Rigidbody2D> ();
		GetComponent<SpriteRenderer> ().sprite = image;

		// Sets up camera offset for use later when changing camera position relative to the player's submarine
		cameraOffset = new Vector3 (0f, 0f, 0f);

		//Find the main camera and move it into the correct position
		mainCamera = Camera.main.transform;
		MoveCamera ();
	}
	/*
	public override void OnStartLocalPlayer(){
		GetComponent<SpriteRenderer> ().sprite = image;
	}
	*/
    // Updated every Physics Step at fixed intervals - used for regular updates such as physics objects
    void FixedUpdate(){ 

		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * moveForce;
		myBody.AddForce (moveVec);

		float move = Input.GetAxis ("Horizontal");
		if (moveVec.x > 0 && !facingright) {

			flip ();
		} else if (moveVec.x < 0 && facingright) {
			flip ();
		}

		//Update camera position
		MoveCamera();
	}

    // This flips the direction of the submarine by scaling the submarine by -1 in the x-axis?
	public void flip(){
		this.facingright = !this.facingright;
        //Vector3 thescale = transform.localScale;
        //thescale.x *= -1;
        //transform.localScale = thescale;
        transform.Rotate(new Vector3(0, 180, 0));       // This solution works across the server!
	}

	// Moves Camera to the correct spot to center on current player
	private void MoveCamera() {

        cameraOffset = transform.position;          // Gets Player's current Position
        cameraOffset.z -= cameraDistance + 2;           // then add a fixed offset away from the player for zoom
        mainCamera.position = cameraOffset;         // And set camera location to this new location to enable camera to move as player moves
		mainCamera.LookAt(transform);				// Make Camera look at Player's submarine
	}

	void OnCollisionEnter2D(Collision2D coll){

		if (coll.gameObject.CompareTag ("Blob")) {
			damage = true;
			shaketimer = 1f;
			//change this variable to reduce shaking
			shakeintensity = 0.3f;
			Handheld.Vibrate ();
		}

	}

	void Update(){

		if (damage) {
//			float a = damageImage.color.a;
//			a += 10;
			damageImage.color = flashcolor;

		}else{

			damageImage.color = Color.Lerp (damageImage.color, Color.clear, 0.5f * Time.deltaTime);
//			float a = damageImage.color.a;
//			a -= 10;
		}
		damage = false;

		//shake it shake it
		if(shaketimer>0f){

			Vector2 shakePos = Random.insideUnitCircle * shakeintensity;
			mainCamera.transform.position = new Vector3 (	mainCamera.transform.position.x + shakePos.x,
															mainCamera.transform.position.y + shakePos.y,
															mainCamera.transform.position.z);
			shaketimer -= Time.deltaTime;
		}




	}



}