using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public Vector2 Margin;
	public Vector2 Smoothing;

	public BoxCollider2D Bounds;
	private Vector3 _min, _max;
	private Camera mainCamera;

	public bool IsFollowing { get; set; }

	public void Start(){
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		mainCamera = GetComponent<Camera>();

	}

	public void Update(){
		float x = transform.position.x;
		float y = transform.position.y;


		float CameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);
		x = Mathf.Clamp (x, _min.x + CameraHalfWidth, _max.x - CameraHalfWidth);
		y = Mathf.Clamp (y, _min.y + mainCamera.orthographicSize, _max.y - mainCamera.orthographicSize);

		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
