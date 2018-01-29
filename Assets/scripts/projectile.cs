using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {
	public GameObject Blood;
	public GameObject Dirt1;
	public GameObject SoundManager;
	// Use this for initialization
	void Start () {
		SoundManager = GameObject.Find ("soundmanager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "plateforme") {
			Instantiate (Dirt1, new Vector3 (transform.position.x, transform.position.y, -2f), Quaternion.identity);
			SoundManager.GetComponent<Sounds> ().PlayImpact ();
		}
		if (col.gameObject.tag == "ennemi") {

				Instantiate (Blood, new Vector3 (transform.position.x, transform.position.y, -2f), Quaternion.identity);
		

		}
	}
}
