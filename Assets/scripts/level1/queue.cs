using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queue : MonoBehaviour {
	public GameObject Splash;
	public AudioClip explosion;
	private AudioSource audiosource;
	public bool descend;
	GameObject ActualSplash;
	// Use this for initialization
	void Start () {
		descend = false;
		audiosource = GetComponent<AudioSource> ();
		 ActualSplash=Instantiate (Splash, new Vector3 (transform.position.x, -10.1f, -1.5f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (descend) {
			GetComponent<Animator> ().enabled = false;
			gameObject.transform.Translate (Vector3.down * 3f * Time.deltaTime, Space.World);
			if (gameObject.transform.position.y < -15) {
				Destroy (ActualSplash);
				Destroy (this.gameObject);

			}
		} else {
			gameObject.transform.Translate (Vector3.up * 4f * Time.deltaTime, Space.World);
			if (gameObject.transform.position.y > -8) {
				gameObject.transform.position = new Vector3 (this.transform.position.x, -8, -1);


			}
		}
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "plateforme") {
			audiosource.PlayOneShot (explosion, 0.3f);
			Destroy(col.gameObject);
		}
	}
}
