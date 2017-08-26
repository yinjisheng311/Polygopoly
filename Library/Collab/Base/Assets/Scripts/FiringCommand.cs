using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


public class FiringCommand : NetworkBehaviour {

	bool facingright;
	public GameObject leftBullet, rightBullet;
	Transform bulletSpawn;
	void Start(){
		bulletSpawn = transform.FindChild ("FirePos");
		this.facingright = false;


	}
	void FixedUpdate(){
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		if (moveVec.x < 0 && !facingright) {

			flip ();
		} else if (moveVec.x > 0 && facingright) {
			flip ();
		}
	}
	public void flip(){
		this.facingright = !this.facingright;

	}


	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Fire")) {
			CmdFire ();
		}
	}


	void CmdFire()
	{
		if (facingright) {
			Instantiate (rightBullet, bulletSpawn.position, Quaternion.identity);
			NetworkServer.Spawn (rightBullet);
		}
		if (!facingright) {
			Instantiate (leftBullet, bulletSpawn.position, Quaternion.identity);
			NetworkServer.Spawn (leftBullet);

		}
	}
}
