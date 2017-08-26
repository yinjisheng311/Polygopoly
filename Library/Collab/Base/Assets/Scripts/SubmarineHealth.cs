using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineHealth : MonoBehaviour {
	public bool stealthmode = false;
	public float maxHealth = 100f; 
	public float currentHealth = 0f;
	public GameObject healthBar;
	Rigidbody2D myBody;


	// Use this for initialization
	void Start () {
		Debug.Log ("STARTED");
		currentHealth = maxHealth;
		//InvokeRepeating ("DecreaseHealth", 1f, 1f);

	}

	// Update is called once per frame
	void Update () {
		if (currentHealth == 0f) {
			Die ();
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		GameObject hit = col.gameObject;
		SubmarineHealth health = hit.GetComponent<SubmarineHealth>();
		if(health != null){
			health.DecreaseHealth();
		}
	}


	public void DecreaseHealth(){
		currentHealth -= 10f;
		float calculatedHealth = currentHealth / maxHealth;
		SetHealthBar (calculatedHealth);

	}

	public void SetHealthBar(float myHealth){

		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

	}
	void Die(){
		Application.LoadLevel (Application.loadedLevel);
	}
}
