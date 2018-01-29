using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {
	public AudioSource audiosource;
	public AudioClip impact;
	void Start(){

		audiosource = GetComponent<AudioSource> ();

	}
	void Update(){
		
	}
	public void PlayImpact(){
		audiosource.PlayOneShot (impact, 0.5f);
	}
}
