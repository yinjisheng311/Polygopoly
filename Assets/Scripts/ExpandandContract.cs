using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandandContract : MonoBehaviour {

	public enum WaveForm {sin, tri, sqr, saw, inv, noise}; 
	public WaveForm waveform = WaveForm.sin;   

	public float baseStart = 0.3f; // start 
	public float amplitude = 0.0f; // amplitude of the wave
	public float phase = 0.0f; // start point inside on wave cycle
	public float frequency = 0.35f; // cycle frequency per second
	public float rotate_speed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 scale = transform.localScale;
		scale.y = EvalWave();
		scale.x = EvalWave();
		transform.localScale = scale;
	}

	float EvalWave () { 
		float x = (Time.time + phase) * frequency;
		float y ;
		x = x - Mathf.Floor(x); // normalized value (0..1)

		if (waveform == WaveForm.sin) {

			y = Mathf.Sin(x * 2 * Mathf.PI);
		} 
		else if (waveform == WaveForm.tri) {

			if (x < 0.5f)
				y = 4.0f * x - 1.0f;
			else
				y = -4.0f * x + 3.0f;  
		}      
		else if (waveform == WaveForm.sqr) {

			if (x < 0.5f)
				y = 1.0f;
			else
				y = -1.0f;  
		}      
		else if (waveform == WaveForm.saw) {

			y = x;
		}      
		else if (waveform == WaveForm.inv) {

			y = 1.0f - x;
		}      
		else if (waveform == WaveForm.noise) { 

			y = 1f - (Random.value * 2);
		}
		else { 
			y = 1.0f;
		}          
		return (y * amplitude) + baseStart;    
	}
}
