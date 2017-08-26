using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostSingle : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject hit = col.gameObject;
		if(hit.GetComponent<SubmarineHealthSingle>() != null)
		{
			SubmarineHealthSingle health = hit.GetComponent<SubmarineHealthSingle>();
			if(health.currentHealth < 100f){
				health.currentHealth = 100f;
				health.SetHealthBar(health.currentHealth);
			}
			Destroy(this.gameObject);
		}
	}
}