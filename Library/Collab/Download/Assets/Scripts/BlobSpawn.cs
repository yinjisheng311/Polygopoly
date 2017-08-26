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

		InvokeRepeating ("spawn_enemy", 0, 30);

    }

	[ServerCallback]
	void Update(){
	
	}

	void spawn_enemy(){
		
		if (Time.timeSinceLevelLoad < 30.0f) {
			blob_to_spawn = blob_1;
			numberOfEnemies = 12;
		} else if (Time.timeSinceLevelLoad > 30.0f && Time.timeSinceLevelLoad < 60.0f) {
			blob_to_spawn = blob_2;
			numberOfEnemies = 8;
		} else {
			blob_to_spawn = motherBlob;
			numberOfEnemies = 1;
		}
		for (int i = 0; i < numberOfEnemies; i++) {
			Vector3 spawnPosition = quadrant_spawn_position();    // Create a random spawn location
			NetworkServer.Spawn ((GameObject)Instantiate (blob_to_spawn, spawnPosition, Quaternion.identity));
		}
	}

	Vector2 quadrant_spawn_position(){
		int randomint = Random.Range (1, 6);
		if (randomint == 1) {
			return new Vector2 (Random.Range (-50f, -37.5f), Random.Range (0f, 30f));
		} else if (randomint == 2) {
			return new Vector2 (Random.Range (-37.5f, 37.5f), Random.Range (20f, 30f));
		} else if (randomint == 3) {
			return new Vector2 (Random.Range (37.5f, 50f), Random.Range (0f, 30f));
		} else if (randomint == 4) {
			return new Vector2 (Random.Range (-50f, -37.5f), Random.Range (-30f, 0f));
		} else if (randomint == 5) {
			return new Vector2 (Random.Range (-37.5f, 37.5f), Random.Range (-30f, -10f));
		} else if (randomint == 6) {
			return new Vector2 (Random.Range (10f, 37.5f), Random.Range (0f, -30f));
		} else {
			return new Vector2 (Random.Range (-50f, 50f), Random.Range (-30f, 30f));
		}
	}



}
