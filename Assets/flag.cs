using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour {
	GameObject GameManager;
	public GameObject StarParts;
	public AudioSource audiosource;
	public AudioClip yeah;
	public AudioClip horn;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		GameManager = GameObject.Find ("GameManager");//recuperer le gqma manager
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.GetComponent<GameManager> ().Lvl1Checkpoint) { // Si le level 1 a ete checkpoint
			gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
		} else {
			gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
		}

	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "player") {
			if (GameManager.GetComponent<GameManager> ().Lvl1Checkpoint == false) {
				Instantiate (StarParts, new Vector3(transform.position.x,transform.position.y,-2), Quaternion.identity);
				audiosource.PlayOneShot (yeah, 0.5f);
				audiosource.PlayOneShot (horn, 0.3f);
				GameManager.GetComponent<GameManager> ().Lvl1Checkpoint = true;
			}
		}
	}
}
