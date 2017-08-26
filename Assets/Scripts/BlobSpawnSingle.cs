using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class BlobSpawnSingle : MonoBehaviour {
	public GameObject blob_1;
	public GameObject blob_2;
	public GameObject blob_3;
	public GameObject blob_4;
	public GameObject blob_5;
	public GameObject blob_6;
	public GameObject blob_7;
	public GameObject blob_7_5;
	public GameObject blob_8;
	public GameObject motherBlob;

	GameObject blob_to_spawn;
	int numberOfEnemies;

    private void Start()
	{
		InvokeRepeating ("spawn_enemy", 0, 5);
	}

	void Update(){
	
	}

	void spawn_enemy(){
		
		if (Time.timeSinceLevelLoad < 5.0f) {
			blob_to_spawn = blob_1;
			numberOfEnemies = 10;
		} else if (Time.timeSinceLevelLoad > 5.0f && Time.timeSinceLevelLoad < 10.0f) {
			blob_to_spawn = blob_2;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 10.0f && Time.timeSinceLevelLoad < 15.0f) {
			blob_to_spawn = blob_3;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 15.0f && Time.timeSinceLevelLoad < 20.0f) {
			blob_to_spawn = blob_4;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 20.0f && Time.timeSinceLevelLoad < 25.0f) {
			blob_to_spawn = blob_5;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 25.0f && Time.timeSinceLevelLoad < 30.0f) {
			blob_to_spawn = blob_6;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 30.0f && Time.timeSinceLevelLoad < 35.0f) {
			blob_to_spawn = blob_7;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 35.0f && Time.timeSinceLevelLoad < 40.0f) {
			blob_to_spawn = blob_7_5;
			numberOfEnemies = 10;
		}
		else if (Time.timeSinceLevelLoad > 40.0f && Time.timeSinceLevelLoad < 45.0f) {
			blob_to_spawn = blob_8;
			numberOfEnemies = 10;
		}
		else {
			blob_to_spawn = motherBlob;
			numberOfEnemies = 5;
		}
		for (int i = 0; i < numberOfEnemies; i++) {
			GameObject blob;
			Vector3 spawnPosition = quadrant_spawn_position ();   // Create a random spawn location
			blob = Instantiate (blob_to_spawn, spawnPosition, Quaternion.identity) as GameObject;
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



