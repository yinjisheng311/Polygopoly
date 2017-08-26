using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : NetworkBehaviour {

	public GameObject explosion_animation;
	public LayerMask enemyMask;
	public float attack_speed = 10f;
	public float evade_speed  = 10f;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth;
	Vector3 spawn_position;
	SubmarinePlayer sp;         // Reference to Submarine Player
	bool aggressive = false;    // Flag to decide if enemy becomes aggressive
	GameObject[] submarines;    
	GameObject[] missiles;




	void Start ()
	{
        //Radar.RegisterRadarObject(this.gameObject, image);
        spawn_position = this.transform.position;
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D> ();
		myWidth = this.GetComponent<SpriteRenderer> ().bounds.extents.x;

		submarines = GameObject.FindGameObjectsWithTag ("Submarine");

		if(submarines.Length==1)
		{
			//GameObject texts = GameObject.FindGameObjectWithTag("Text2");
			//DisplayText score = texts.GetComponent<DisplayText>();
			//score.updateText("");
			//score.text = "";
		}else if(submarines==null){
			InvokeRepeating ("findSubmarines", 5f, 120);
		}

	}

	void findSubmarines(){
		submarines = GameObject.FindGameObjectsWithTag ("Submarine");
	}

    [ServerCallback]         // Only runs this update method on the server, the server is going to keep track of the blob to ensure location of blob is consistent throughout clients
    void Update()
	{
		transform.Rotate (0, 0, Random.Range (1.5f, 2.2f));
        if (gameObject != null && submarines.Length>1)
        {
			if (Vector2.Distance (gameObject.transform.position, submarines [0].transform.position) < Vector2.Distance (gameObject.transform.position, submarines [1].transform.position)) {
				myBody.transform.position = Vector2.MoveTowards (myBody.transform.position, submarines [0].transform.position, attack_speed);
			}
//			else if (Vector2.Distance (gameObject.transform.position, submarines [0].transform.position) > Vector2.Distance (gameObject.transform.position, submarines [1].transform.position)) {
//				myBody.transform.position = Vector2.MoveTowards (myBody.transform.position, submarines [1].transform.position, attack_speed);
		//}
			else {
				myBody.transform.position = Vector2.MoveTowards (myBody.transform.position, submarines [1].transform.position, attack_speed);
																
			}
        }
		else if (gameObject != null && submarines.Length==1)
		{
			if (submarines [0] == null) {
				//OnDestroy ();
				Debug.Log("Failed to register submarines");
			} else {
				myBody.transform.position = Vector2.MoveTowards (myBody.transform.position, submarines [0].transform.position, attack_speed);
		
			}
		}
		else
        {
			Debug.Log("Failed to register submarines");
            //NetworkServer.Destroy(gameObject);
        }
	}
	
	[Command]
	void CmdExit()
	{
		RpcShutDown();
	}
	
	[ClientRpc]
	void RpcShutDown()
	{
		Application.Quit();
	}

//	public void idle_mode(){
//		
//		Vector2 myVel = myBody.velocity;	
//		myVel.x = Random.Range (-30.0f, 30.0f);
//		myVel.y = Random.Range (-10.0f, 18.0f);
//		myBody.velocity = myVel*0.1f;
//	}
//

//	void OnTriggerEnter2D(Collider2D other) {
//		if (other.gameObject.CompareTag ("Missile")) {
//			Debug.Log ("Dodging Missile");
//			gameObject.transform.position = Vector2.MoveTowards (myBody.transform.position, -other.gameObject.transform.position, evade_speed);
//
//		} else if (other.gameObject.CompareTag ("Submarine")) {
//			gameObject.transform.position = Vector2.MoveTowards (myBody.transform.position, other.gameObject.transform.position, attack_speed);
//
//		}
//	}


	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject hit = col.gameObject;

		if (hit.name.Equals("Submarine Player Nic 1(Clone)")){
			SubmarineHealth health = hit.GetComponent<SubmarineHealth>();
			if (health!=null){
				health.DecreaseHealth ();
			}


//			if (health != null && health.currentHealth != 10.0f) {
//				health.DecreaseHealth ();
//				//health.currentHealth -= 10f;
//
//				// Blobs should not die upon contact with player, can only be killed by missles...
//				//EnemyBlobHealth enemyHealth = gameObject.GetComponent<EnemyBlobHealth> ();
//				//enemyHealth.DecreaseHealth (hit.tag);
//
//
//			} else if (health != null && health.currentHealth == 10.0f) {
//				
//				Debug.Log ("Changing to End Panel");
//
//				Scene current = this.gameObject.scene;
//				Debug.Log (current);
//				GameObject[] scenes = current.GetRootGameObjects ();
//				foreach (GameObject root in scenes) {
//					Debug.Log (root);
//					if (root.GetComponent<RectTransform> () != null) {
//
//						Debug.Log ("found End");
//						GameObject endScene = GameObject.FindGameObjectWithTag ("End");
//						for (int i = 0; i < endScene.transform.childCount; i++) {
//							if (i == 4) {
//								if (endScene.transform.GetChild (i).gameObject.transform.position.y != -270) {
//									
//									endScene.transform.GetChild (i).gameObject.transform.Translate (0, -320, 0);
//								}
//							}
//
//							endScene.transform.GetChild (i).gameObject.SetActive (true);
//						}
//						//Debug.Log(endScene);
//						Debug.Log ("Finish enabling");
//					} else if (root.GetComponent<NetworkManager> () != null) {
//						root.GetComponent<NetworkManager> ().StopHost ();
//
//					}
//					//NetworkServer.Reset();
//				}
//				//NetworkServer.Stop
//				//NetworkServer.Shutdown();
//				//NetworkServer.DisconnectAll();
//				NetworkServer.Reset ();
//				//NetworkHost host = gameObject.GetComponent<NetworkHost>();
//				//NetworkManager man = host.GetComponent<Network
//				//man.StopHost();
//				//CmdExit();
//
//			} else {
//				Debug.Log ("THIS COULD MEAN BLOB HITTING INTO EACH OTHER");
//			}
		}


	}

	void OnDestroy(){
		
        try
        {
            //NetworkServer.Spawn((GameObject)Instantiate(explosion_animation, gameObject.transform.position, Quaternion.identity));
        }catch(UnassignedReferenceException URE)
        {
            Debug.LogException(URE);
            Debug.Log("UnassignedReferenceException : Probably caused by missing explosion_animation prefab to instanitate.");
        }
		
	}
}
