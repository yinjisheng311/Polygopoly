using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {
	
	public GameObject leftMissile, rightMissile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject hit = col.gameObject;
		FiringCommand ammo = hit.GetComponent<FiringCommand>();
		ammo.leftBullet = leftMissile;
		ammo.rightBullet = rightMissile;
	}
}
