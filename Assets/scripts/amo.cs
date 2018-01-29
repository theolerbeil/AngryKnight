using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amo : MonoBehaviour {
	public bool ShoulRotate;
	public float timeToDestroy;
	public bool Ennemi;

	public GameObject SoundManager;


	public bool CanCollide; // using just for waterball

	public bool WaterBall;

	// Use this for initialization
	void Start () {
		CanCollide = false;
		StartCoroutine(WaitToDestroy());
		SoundManager = GameObject.Find ("soundmanager");

	}
	void Update () {
		if (ShoulRotate) {
			transform.Rotate (Vector3.back * 12);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "plateforme")
		{
			
			if (!WaterBall) {
				
				Destroy (this.gameObject);
			}
			if (CanCollide) {
				Destroy (this.gameObject);
			}
		}
		if (!Ennemi) {
			if (col.gameObject.tag == "ennemi") {
				if (col.gameObject.GetComponent<ennemiBasic> () != null) {
					col.gameObject.GetComponent<ennemiBasic> ().Blesser (1);

				}else if(col.gameObject.GetComponent<boss1> () != null){
					col.gameObject.GetComponent<boss1> ().Blesser (1);

				}else if(col.gameObject.GetComponent<FlyEnnemi1> () != null){
					col.gameObject.GetComponent<FlyEnnemi1> ().Blesser (1);

				}
				Destroy (this.gameObject);

			}
		}
	}
	IEnumerator WaitToDestroy()
	{

		yield return new WaitForSeconds(timeToDestroy);
		if (WaterBall) {
			Rigidbody2D clone;
			clone = Instantiate (this.gameObject.GetComponent<Rigidbody2D>(), new Vector3(Random.Range(88f,97.5f),transform.position.y+0.2f,-0.5f), Quaternion.identity) as Rigidbody2D;
			clone.GetComponent<SpriteRenderer>().flipY = !clone.GetComponent<SpriteRenderer>().flipY;
			clone.GetComponent<amo> ().WaterBall = false;
			clone.GetComponent<amo> ().CanCollide = true;
			clone.velocity = transform.TransformDirection (Vector2.down * 6);

			Destroy (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}
}
