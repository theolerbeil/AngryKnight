using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class poisson : MonoBehaviour {
	public GameObject Player;
	public bool CanJump;
	public GameObject WaterParts;
	public bool CanEmit;

	public AudioSource audiosource;
	public AudioClip plouf;
	// Use this for initialization
	void Start () {
		CanEmit = true;
		CanJump = true;
		Player = GameObject.FindWithTag ("player");
		audiosource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.transform.Translate (Vector3.left * 4f * Time.deltaTime, Space.World);
		if((gameObject.transform.position.x - Player.transform.position.x < 4)&&Player.transform.position.y < -6){
			if (CanJump) {
				gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
				gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.up * 700);
				CanJump = false;
			}
		}
		if (gameObject.transform.position.y < -11) {
			if (CanEmit == true) {
				audiosource.PlayOneShot (plouf, 0.5f);
				Instantiate (WaterParts, new Vector3 (transform.position.x+0.5f, transform.position.y - 0.25f, -0.5f), Quaternion.identity);
				CanEmit = false;
			}
			gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		if(gameObject.transform.position.y<-30||gameObject.transform.position.x<-12){
			Destroy (this.gameObject);
		}

	}


}
