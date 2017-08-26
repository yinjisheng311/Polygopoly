using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class RadarObject{
	
	public Image icon{ get; set;}
	public GameObject owner { get; set;}
}

public class Radar : MonoBehaviour {
	

	Transform playerPos;
	Vector3 radarPos;
	GameObject[] radars;
	float mapScale = 1.0f;

	public static List<RadarObject> radObjects = new List<RadarObject>();

	public static void RegisterRadarObject(GameObject o, Image i){
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
			//float distToObject = Vector2.Distance (playerPos.position, ro.owner.transform.position);
			//float deltay = Mathf.Atan2 (radPos.x, radPos.y) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;
			//radPos.x = distToObject * Mathf.Cos (deltay * Mathf.Deg2Rad) * 1;
			//radPos.y = distToObject * Mathf.Sin (deltay * Mathf.Deg2Rad);

			ro.icon.transform.SetParent (radars[0].transform);

			//ro.icon.transform.position = new Vector3(radarPos.x+radPos.x,radarPos.y+radPos.y,radarPos.z);
			ro.icon.transform.position = new Vector3(radPos.x, radPos.y, 0) + new Vector3(radarPos.x,radarPos.y,radarPos.z);

		}
	}
	// Use this for initialization
	void Start () {
        //Debug.Log("Starting Radar for gameObject : " + gameObject);
        //Debug.Log("Is gameObject equals to Radar? " + gameObject.Equals(GameObject.FindGameObjectWithTag("Radar")));
        //Debug.Log("Result of get component for Submarine Player is : "+ gameObject.GetComponent<SubmarinePlayer>());
        //Debug.Log("Is Local Player : " + gameObject.GetComponent<SubmarinePlayer>().isLocalPlayer);
        try
        {
            if (!gameObject.GetComponent<SubmarinePlayer>().isLocalPlayer)
            {
                Destroy(this);
                return;
            }
        }
        catch(NullReferenceException NRE)
        {
            Debug.Log("This should be the Radar on the canvas, so not going to destroy...");
        }

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
