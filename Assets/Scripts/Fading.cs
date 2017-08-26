using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1; //if -1 then fade in, if 1 then fade out

	void OnGUI(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		//clamp the value between 0 and 1, GUI color uses value between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		//setting color of GUI(texture)
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth; //making sure black texture renders on top
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), fadeOutTexture);

	}

	public float BeginFade(int direction){
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded(){
		BeginFade (-1);
	}
}
