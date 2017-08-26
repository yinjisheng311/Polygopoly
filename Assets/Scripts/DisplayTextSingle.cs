using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class DisplayTextSingle : MonoBehaviour {

    public Text textHolder;
    string textVal;

	// Use this for initialization
	void Start () {
        textVal = "0";
        textHolder.text = textVal;
	}

    // Update is called once per frame
    void Update()
    {
        textVal = textHolder.text;
    }
	//EnemyController - set textHolder to "" if only 1 player
    public void updateText(string newTextVal)
    {
        textHolder.text = newTextVal;
    }

}