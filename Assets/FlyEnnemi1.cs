using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnnemi1 : MonoBehaviour {
	public int PV;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PV <= 0) {
			Destroy (this.gameObject);
		}

	}
	public void Blesser(int degats){
		//audiosource.PlayOneShot (cris, 0.8f);
		PV-=degats;
	}
}
