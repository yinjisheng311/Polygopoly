using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlobHealth : MonoBehaviour {
	public float maxHealth = 100f; 
	public float currentHealth = 0f;
	public GameObject healthBar;
	Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0f) {
			Die ();
		}
	}
	public void Die(){
		
		Destroy (gameObject);
	}

	public void DecreaseHealth(){

		currentHealth -= 10f;
		float calculatedHealth = currentHealth / maxHealth;
		SetHealthBar (calculatedHealth);

	}
	public void SetHealthBar(float myHealth){

		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

	}
}
