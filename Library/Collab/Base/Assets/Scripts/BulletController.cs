using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Vector2 speed;
	Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = speed;
	}

	void Update () 
	{
		rb.velocity = speed;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject hit = col.gameObject;
		SubmarineHealth health = hit.GetComponent<SubmarineHealth>();
		if(health != null){
			health.DecreaseHealth();
		}
		Destroy (gameObject);
	}
}
