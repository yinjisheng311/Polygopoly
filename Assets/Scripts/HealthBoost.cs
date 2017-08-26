using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject hit = col.gameObject;
		if(hit.GetComponent<SubmarineHealth>() != null)
		{
			SubmarineHealth health = hit.GetComponent<SubmarineHealth>();
			if(health.currentHealth < 100f){
				health.currentHealth = 100f;
			}
			Destroy(this.gameObject);
		}
	}
}
