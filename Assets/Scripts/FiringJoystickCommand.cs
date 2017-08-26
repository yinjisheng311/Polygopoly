using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class FiringJoystickCommand : NetworkBehaviour {

	public GameObject bullet;
	//Vector3 newPosition;
	private bool cooledDown = true;
	Text count1;
	Text count2;
	int counter1;
	int counter2;
	string playerTag;
	Transform laserSpawn;
	LineRenderer line;
	public bool laser;

	void Start(){
        Debug.Log("Firing Joystick Command start() has been called");
		counter1 = 0;
		counter2 = 0;
		// Just simple rudimentary logic for 2 players first
		// Shall assume that server is player 1, and client is player 2
		if (isServer) {
			playerTag = "Player1";
			Debug.Log ("Server here!");
		} else {
			playerTag = "Player2";
			Debug.Log ("Non-server here!");
		}
		if(transform.childCount == 0)
		{
			laserSpawn = transform.parent.FindChild("FirePos");
		}
		else
		{
			laserSpawn = transform.FindChild("FirePos");
		}
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;

	}


	void Update(){

		if (!isLocalPlayer) { // Only local Players can fire
			return;
		}
        if (laser)
        {
            Debug.Log("LASER HAS BEEN PICKED UP");
        }
		//using absolute values to make sure the bullet never spawns too near to itself
		float xControl = CrossPlatformInputManager.GetAxis ("HorizontalFire1");
		float yControl = CrossPlatformInputManager.GetAxis ("VerticalFire1");
		if ((Mathf.Abs(xControl) > 0.5f || Mathf.Abs(yControl) > 0.5f) && cooledDown ) {

            //getting the coordinates of the submarine player in game
            float subx = gameObject.transform.localPosition.x;
            float suby = gameObject.transform.localPosition.y;

            //Debug.LogFormat("Player current location is {0}, {1}", subx, suby);
            //Debug.LogFormat("Player intended fire direction is : ({0},{1})", xControl, yControl);

            //creating a new spawn position for the bullet according to the joystick position and fire command is called
            Vector3 spawnPosition = new Vector3(subx + (xControl * 2.9f), suby + (yControl * 2.9f), 0.0f);
			//laser = true;
            if (!laser)
            {
                CmdFire(spawnPosition, xControl, yControl, playerTag);
                cooledDown = false;
                StartCoroutine(startCoolDown());

            }else
            {
                Debug.Log("Initiating Fire Laser Command");
                Vector2 direction = new Vector2(xControl*100, yControl*100);
				Ray ray = new Ray(gameObject.transform.position, direction);
				Vector3 direction_shoot = ray.GetPoint (100);
				CmdFireLaser(spawnPosition, direction_shoot);
                cooledDown = false;
                laser = false;
                StartCoroutine(startLaserCheck(spawnPosition,direction,ray));   // Running on coroutine so frame does not lag...    
                StartCoroutine(startCoolDown());
            }
        }
	}

	IEnumerator startCoolDown(){
		yield return new WaitForSeconds(0.09f);
		cooledDown = true;
	}

    IEnumerator startLaserCheck(Vector3 spawnPosition, Vector3 direction, Ray ray)
    {
        line.enabled = true;
        RaycastHit2D hit;
        while ((hit = Physics2D.Raycast(spawnPosition, direction, 100)).collider != null)    // If there is something in the path of the laser beam
        {
            //hit = Physics2D.Raycast(laserSpawn.position, laserSpawn.right, 30);
            Debug.Log("raycast hit into something along path, removing them if not background ...");

            Collider2D collider = hit.collider;
			if (collider.gameObject.tag.Equals("Blob"))
            {
				Debug.Log ("Destroying" + collider.gameObject.tag);
					NetworkServer.Destroy (collider.gameObject);
					Debug.Log ("Just destroyed the object laser collided with");
					try {
						gameObject.GetComponent<ScoreUpdater> ().updatePlayerScores (playerTag, collider.gameObject.name);
						Debug.Log ("After method call of updatePlayerScores");
						//gameObject.GetComponent<EnemyBlobHealth>().RpcDie(playerTag);
					} catch (NullReferenceException NRE) {
						Debug.Log ("Did not hit onto blob! But carried out indiscriminate destruction!!!");
					}

            }
            else
            {
                break;     // Break out of while loop as the laser has already hit the boundaries.
            }
        }
        yield return null; // Unity will wait for the next frame to complete the current scope
    }

    IEnumerator laserTime(Vector3 origin, Vector3 end)
    {
        line.enabled = true;
        Debug.Log("Line renderer enabled");
        line.SetPosition(0, origin);
        line.SetPosition(1, end);

        yield return new WaitForSeconds(1f);
        Debug.Log("After showing laser for 1 Seconds");

        line.enabled = false;
        Debug.Log("Disabling line renderer");

    }
    [ClientRpc]
    void RpccreateLine(Vector3 origin, Vector3 end)
    {
        StartCoroutine(laserTime(origin, end));
    }

    [Command]  
    void CmdFireLaser(Vector3 origin, Vector3 end)
    {
        RpccreateLine(origin, end);
    }		

	// This method will spawn the bullet seen in the network
	[Command]// This method will spawn the bullet seen in the network
	void CmdFire(Vector3 spawnPosition, float xControl, float yControl, string playerTagVal){// Method will not be run on client! - Note that the bullet must allow server and client authority to work!
		//Debug.Log ("Cmd Fire Initiated");
		GameObject instance = Instantiate (bullet, spawnPosition, Quaternion.identity);
		//Debug.Log ("Bullet instance is : " + instance);
		//Vector2 forwardForce = new Vector2((newPosition.x-gameObject.transform.localPosition.x) * 100, (newPosition.y-gameObject.transform.localPosition.y) * 100);
		Vector2 forwardForce = new Vector2(xControl*1000,yControl*1000);
		//Debug.Log ("forward force is : " + forwardForce);
		instance.GetComponent<Rigidbody2D>().AddForce(forwardForce);
        //Debug.Log("Player tag is : "+playerTagVal);
		instance.tag = playerTagVal;
		NetworkServer.Spawn (instance);
		//Debug.Log ("Bullet has been Spawned on Server");
	}
				
}
