using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour {
	public bool CanEffect;
	public bool CanBounce;
	// Use this for initialization
	void Start () {
		CanEffect = false;
		CanBounce = true;
		StartCoroutine(WaitToEffect());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "plateforme") {
			Destroy (this.gameObject);

		}
		if (col.gameObject.tag == "ennemi") {
			if(CanEffect){
				if (CanBounce) {
					this.gameObject.GetComponent<Rigidbody2D>().velocity =  (new Vector3(GameObject.FindGameObjectWithTag("player").transform.position.x,GameObject.FindGameObjectWithTag("player").transform.position.y,1) - transform.position).normalized * 6;
					CanBounce = false;
				} else {
					if(col.gameObject.GetComponent<boss1> () != null){
						col.gameObject.GetComponent<boss1> ().Blesser (10);
					}
					Destroy (this.gameObject);
				}
			}

		}
	}
	IEnumerator WaitToEffect()
	{

		yield return new WaitForSeconds(1);
		CanEffect = true;
	}
}
