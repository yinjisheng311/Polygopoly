using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GiveBirth : NetworkBehaviour {


	public float number_of_children = 10f;
	public GameObject blob_to_spawn;
	// Use this for initialization
	private void Start()
	{	
		if (!isServer)
		{
			Destroy(this);  // Spawning of the blobs can only be done by the server
		}			

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		float random_coordinate_offset = Random.Range (-10, 10);
		for (int i = 0; i < number_of_children; i++) {
			Vector3 spawnPosition = new Vector3 (gameObject.transform.position.x + random_coordinate_offset, gameObject.transform.position.y + random_coordinate_offset, 0f);    // Create a random spawn location
			NetworkServer.Spawn ((GameObject)Instantiate (blob_to_spawn, spawnPosition, Quaternion.identity));
		}

	}
}
