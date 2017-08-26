using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DisplayText : NetworkBehaviour {

    public Text textHolder;
    [SyncVar (hook ="updateText")]string textVal;

	// Use this for initialization
	void Start () {
		textVal = textHolder.text;
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
