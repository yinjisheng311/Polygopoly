using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class PotionSpawnSingle : MonoBehaviour {
	public GameObject potion;
	public int numberOfPotions;
	public float spawning_interval = 30f;
	
	void Start()
    {	
		Debug.Log ("spawning potions...");
		InvokeRepeating ("spawn_potion", 0f, spawning_interval);
	}

	void spawn_potion(){
		
		for (int i = 0; i < numberOfPotions; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-60.0f, 60.0f), Random.Range(-40.0f, 40.0f), 0.0f);    // Create a random spawn location

			GameObject healthboost = Instantiate(potion, spawnPosition, Quaternion.identity) as GameObject;

			//NetworkServer.Spawn(healthboost);
		}

	}
}