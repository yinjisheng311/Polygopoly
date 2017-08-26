using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BlobSpawn : NetworkBehaviour {
	public GameObject blob_1;
	public GameObject blob_2;
	public GameObject motherBlob;
	GameObject blob_to_spawn;
	int numberOfEnemies;

    private void Start()
    {	
        if (!isServer)
        {
            Destroy(this);  // Spawning of the blobs can only be done by the server
        }

		InvokeRepeating ("spawn_enemy", 3, 30);

    }

	[ServerCallback]
	void Update(){
	
	}

	void spawn_enemy(){
		
		if (Time.timeSinceLevelLoad < 30.0f) {
			blob_to_spawn = blob_1;
			numberOfEnemies = 10;
		} else if (Time.timeSinceLevelLoad > 30.0f && Time.timeSinceLevelLoad < 60.0f) {
			blob_to_spawn = blob_2;
			numberOfEnemies = 10;
		} else {
			blob_to_spawn = motherBlob;
			numberOfEnemies = 1;
		}
		for (int i = 0; i < numberOfEnemies; i++) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-30.0f, 30.0f), Random.Range (-10.0f, 18.0f), 0.0f);    // Create a random spawn location
			NetworkServer.Spawn ((GameObject)Instantiate (blob_to_spawn, spawnPosition, Quaternion.identity));
		}
	}



}
