using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour {

	Rigidbody2D rb;
	public float target_angle;
	Vector2 shatter_direction;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		shatter_direction = new Vector2 (Random.Range (0, 360), Random.Range (0, 360));
	}

	void Update(){
		//float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.x, target_angle,0.1f);
		//transform.eulerAngles = new Vector3 ( angle,0,0);

		gameObject.transform.position = Vector2.Lerp (transform.position,shatter_direction,0.001f);

	}
	

}
