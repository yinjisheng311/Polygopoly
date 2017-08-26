using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class Single_Radar : MonoBehaviour {

	Transform playerPos;
	Vector3 radarPos;
	GameObject[] radars;
	float mapScale = 1.0f;

	public static List<RadarObject> radObjects = new List<RadarObject>();

	public static void RegisterRadarObject(GameObject o, Image i){
		Debug.Log ("Image is coming from"+o.name);
		Image image = Instantiate (i);
		radObjects.Add (new RadarObject (){ owner = o, icon = image });
		//Debug.Log (o.transform.position);

	}

	public static void RemoveRadarObject(GameObject o){
		List<RadarObject> newList = new List<RadarObject> ();
		for (int i = 0; i < radObjects.Count; i++) {
			if (radObjects [i].owner == o) {
				Destroy (radObjects [i].icon);
				continue;
			} else 
				newList.Add (radObjects [i]);

		}
		radObjects.RemoveRange (0, radObjects.Count);
		radObjects.AddRange (newList);
	}

	void DrawRadarDots(){
		foreach (RadarObject ro in radObjects) {
			Vector2 radPos = (ro.owner.transform.position-playerPos.transform.position);
			radPos.y += 0f;

			ro.icon.transform.SetParent (radars[0].transform);

			//ro.icon.transform.position = new Vector3(radarPos.x+radPos.x,radarPos.y+radPos.y,radarPos.z);
			ro.icon.transform.position = new Vector3(radPos.x, radPos.y-127f, 0) + new Vector3(radarPos.x,radarPos.y,radarPos.z);

		}
	}
	// Use this for initialization
	void Start () {

		radars = GameObject.FindGameObjectsWithTag ("Radar");
		radarPos = radars [0].transform.position;
		//Debug.LogFormat("radars is : {0} , radarPos is : {1}",radars,radarPos);
	}

	// Update is called once per frame
	//
	void Update () {

		playerPos = gameObject.transform;
		DrawRadarDots ();
		//Debug.Log (radarPos.x);

	}
}
