using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour {

	Rigidbody2D rb;                 // Reference to the bullet
    float bulletLifeTime = 5f;      // How long can the bullet exist in the game
    bool isDamaging = true;        // Does the bullet damage players?
    float age;                      // How long the shell has been alive for


	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}

    [ServerCallback]        // Only runs this update method on the server, the server is going to keep track of the bullet
	void FixedUpdate () 
	{
        age += Time.deltaTime;
        if (age > bulletLifeTime)
        {
            NetworkServer.Destroy(gameObject); // Destroys bullet every where if bullet has been around for too long
        }
	}

    // When the shell hits something
	void OnCollisionEnter2D(Collision2D col)
	{
        NetworkServer.Destroy(gameObject); // Destroys bullet every where as bullet has already collided
        if (!isDamaging)    // Leave if bullet is not damaging
        {
            return;
        }
        //Debug.Log("Bullet has hit something, and action is managed by server");
        GameObject hit = col.gameObject;        // Reference to object that bullet collides with
        SubmarineHealth health;                 // Player health
		if (hit.tag.Equals("Submarine"))
        {
			// do nothing
			// do not decrease health, submarines cannot kill each other
			Debug.Log("IT HIT A SUBMARINE " + hit.tag); 
			/*
            health = hit.GetComponent<SubmarineHealth>();
            if (health != null)
            {
                health.DecreaseHealth();
            }else
            {
                Debug.Log("HEALTH IS NULL, WHY");
            }
            */
		}else if(hit.tag.Equals("Blob"))//TODO: Unimplemented logic for monster
		{
			EnemyBlobHealth enemyHealth = hit.GetComponent<EnemyBlobHealth> ();
			enemyHealth.DecreaseHealth ();

        }
		else if(hit.tag.Equals("Missile"))//TODO: Unimplemented logic for monster
		{
			Debug.Log("HIT INTO ITSELF");
			//do nothing

		}


        else
        {
            // Probably do nothing...
            // For now just carry out indiscriminate damage and assume target is a submarine
			Debug.Log("YOU SHOULD NOT BE HERE! You are hitting " + hit.tag);
        }
	}
}
