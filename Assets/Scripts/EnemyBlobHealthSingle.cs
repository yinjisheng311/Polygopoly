using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyBlobHealthSingle : MonoBehaviour {
	public float maxHealth; // Synced to prevent cheating! Maybe? 
    public float currentHealth = 0f;  // The hook means that the method declared in the hook will be called everytime this variable is changed
    //public GameObject healthBar;
	Rigidbody2D myBody;
    public RectTransform healthBar;
	public RectTransform healthBarBackground;
	public RectTransform healthBarBorder;
	float transparency = 1f;
	public GameObject shattered_pieces;

    // Use this for initialization
    void Start () {
		currentHealth = maxHealth;
		//do not show the health bar unless hit
		healthBar.gameObject.SetActive (false);
		healthBarBackground.gameObject.SetActive (false);
		healthBarBorder.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	//void Update () {              // This is not very computationally efficient
	//	if (currentHealth <= 0f) {
	//		Die ();
	//	}
	//}

    
	public void blobDie(string calleeRef){
		GameObject shattering = (GameObject)Instantiate (shattered_pieces, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject); // Destroys all instances of the blob everywhere
        Debug.Log("Blob died by missle, calling update player score method now...");
        Debug.Log(gameObject.GetComponent<ScoreUpdaterSingle>());
        Debug.Log("gameObject for EnemyBlobHealth is : " + gameObject);
        Debug.Log("Tag to pass along is : " + calleeRef);
		string blobName = gameObject.name;
		gameObject.GetComponent<ScoreUpdaterSingle>().updateScores(calleeRef,blobName);
        //Text counter = null;

        //if (calleeRef.Equals("Player1"))
        //{
        //    counter = GameObject.Find("Count Text 1").GetComponent<Text>();
        //}
        //else if (calleeRef.Equals("Player2"))
        //{
        //    counter = GameObject.Find("Count Text 2").GetComponent<Text>();
        //}
        //else
        //{
        //    Debug.Log("INVALID TAG, GO INVESTIGATE");
        //}
        //int currentScore = 0;
        //if (System.Int32.TryParse(counter.text, out currentScore))
        //{
        //    currentScore++;
        //}
        //else
        //{
        //    Debug.Log("Conversion to integer failed!");
        //}

        //counter.text = currentScore.ToString();
    }

	public void DecreaseHealth(string calleeRef){
        // Damage can only be calculated by the server, preventing hacked client from cheating
        // Also, if player is dead, no need to run this code anymore
		healthBar.gameObject.SetActive(true);
		healthBarBackground.gameObject.SetActive (true);
		healthBarBorder.gameObject.SetActive (true);
		Debug.Log("Can't change transparency, Default-Particle doesn't have color attr");
		//transparency = GetComponent<SpriteRenderer> ().material.color.a;
		//transparency -= 10f;

        Debug.Log("Decrease Health Method on Blob is called! current health is : " + currentHealth);
        if (currentHealth <= 0)
        {
            return;
        }
        currentHealth -= 10f; // No need to make explicit call to set player health as the current health variable is already hooked to setHealthBar method
        //float calculatedHealth = currentHealth / maxHealth;
        SetHealthBar (currentHealth);

        if (currentHealth <= 0f)
        {
            // Call a method on all instances of this object on all clients (RPC)
            blobDie(calleeRef);
            return;
        }

    }
	public void SetHealthBar(float myHealth){

        Debug.Log("Blob is hit! health of blob is : "+myHealth);
		healthBar.sizeDelta = new Vector2 (myHealth/maxHealth*5, healthBar.sizeDelta.y);

	}


}