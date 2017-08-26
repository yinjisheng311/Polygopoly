using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


public class FiringCommand : NetworkBehaviour {

    public GameObject leftBullet, rightBullet , RawBullet;
    Transform bulletSpawn;
	Transform laserSpawn;
    SubmarinePlayer playerControl;
    bool facingright;
	LineRenderer line;
	public bool laser;
	
    private void Start()
    {
        bulletSpawn = transform.FindChild("FirePos");
		laserSpawn = transform.FindChild("LaserPos");
        GameObject g = GameObject.Find("Submarine Player Nic(Clone)");
        Debug.Log("Value of g in Firing Command is : " + g);
        playerControl = GetComponent<SubmarinePlayer>();
        Debug.Log("playerControl is : " + playerControl);
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
		
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) // Only local players can fire
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire") && !laser)
        {
            Debug.Log("FIRE BUTTON PRESSED");
            CmdFire();
            Debug.Log("CmdFire() should have been executed");
        }
		else if(CrossPlatformInputManager.GetButtonDown("Fire") && laser)
		{
			StartCoroutine("FireLaser");
		}
		
    }
	
    // The reason this script is not in submarine player is because this command needs to be accessible to all
    // Players when the server calls this method
    // This method will spawn the bullet seen in the network
    [Command] // This is used to indicate a server call    - Command is run on server when called by local player
    void CmdFire()  // Method will not be run on client! - Note that the bullet must allow server and client authority to work!
    {
        Debug.Log("CmdFire Called by "+this.name);
        //TODO: Implement 360 firing
        // Possible method of implemenation
        // Set spawn point based on pointer of cursor

        GameObject instance = Instantiate(RawBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
        Debug.Log("bulletSpawn Forward Value is : " + bulletSpawn.forward);
        Debug.Log("bulletSpawn Local position is : " + bulletSpawn.localPosition);
        Vector2 forwardForce = new Vector2(bulletSpawn.forward.z * 100, 0);
        Debug.Log("Forward force is : " + forwardForce);
        instance.GetComponent<Rigidbody2D>().AddForce(forwardForce);
        NetworkServer.Spawn(instance); 


        // Bottom can be ignored after implementing 360 firing!
        //facingright = playerControl.facingright;
        //if (facingright)
        //{                  // If facing Right
        //    Debug.Log("Player is facing right ");
        //    GameObject instance = Instantiate(rightBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
        //    NetworkServer.Spawn(instance);  // Spawn right bullet
        //}
        //if (!facingright)
        //{                // If facing left
        //    Debug.Log("Player is facing left ");
        //    GameObject instance = Instantiate(leftBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
        //    NetworkServer.Spawn(instance);   // Spawn left Bullet
        //}
    }
	
	//coroutine
	
	IEnumerator FireLaser () {
		line.enabled = true;
		
		while(CrossPlatformInputManager.GetButton("Fire")){
			RaycastHit2D hit = Physics2D.Raycast(laserSpawn.position, laserSpawn.right, 100);
			Ray ray = new Ray(laserSpawn.position,laserSpawn.right);
			if(hit)
			{
				line.SetPosition(0,ray.origin);
				line.SetPosition(1,hit.point);
				Collider2D collider = hit.collider;
				if(collider.gameObject.tag == "Blob")
				{
					Debug.Log ("Destroying" + collider.gameObject.tag);
					Destroy(collider.gameObject);
				}
			}
			else
			{
				line.SetPosition(0,ray.origin);
				line.SetPosition(1,ray.GetPoint(30));
			}
			yield return null;
		}
		line.enabled = false;
	}
}
