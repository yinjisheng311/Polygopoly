using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PotionSpawn : NetworkBehaviour {
	public GameObject potion;
	public int numberOfPotions;
	public float spawning_interval = 30f;
	
	private void Start()
    {	
        if (!isServer)
        {
            Destroy(this);  // Spawning of the potions can only be done by the server
        }

		InvokeRepeating ("spawn_potion", 0f, spawning_interval);

    }

	void spawn_potion(){
		for (int i = 0; i < numberOfPotions; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-60f, 60.0f), Random.Range(-35.0f, 35.0f), 0.0f);    // Create a random spawn location
			GameObject healthboost = (GameObject)Instantiate(potion, spawnPosition, Quaternion.identity);
			NetworkServer.Spawn(healthboost);
		}	

	}
}