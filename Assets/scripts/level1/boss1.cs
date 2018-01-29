/*

Etats :
0 = etat initial
1 = Sort de l'eau
2 = Static
3 = Plonge
4 = Plonge Vertical
5 = Attack 1
6 = Attack 2

Attaques :
1 = Boules en l'air
2 = Vapeur
3 = boule
4 = plusieurs boules
5 = boules en l'air mouvement

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour {
	public int PV;
	public int InitialPV;
	public int State;
	public AudioClip monster1;
	public AudioClip explosion;
	private AudioSource audiosource;
	public GameObject Splash;
	public GameObject CurrentSplash;
	public Rigidbody2D Ball;
	public bool CanEmit;
	public bool CanAttack;
	public bool CanReverse;
	public int Attaque;
	private Animator anim;
	public Rigidbody2D steam;
	public Rigidbody2D BossBall;
	public GameObject queue;
	public GameObject smokePart;

    public GameObject[] ColliderBossState;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audiosource = GetComponent<AudioSource> ();
		State = 0;
		CanReverse = true;
		CanEmit = true;
		CanAttack = true;
		audiosource.PlayOneShot (monster1, 0.5f);
		InitialPV = PV;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;
		//CONDITIONS
		if(PV <= 0){
			
			queue.GetComponent<queue> ().descend = true;
			Destroy (this.gameObject);
		}



		//ETATS
		/*---------------------------------------------------- Etat Monte Initial ----------------------------------------*/
		if (State == 0) {
			gameObject.transform.Translate (Vector3.up * 1f * Time.deltaTime, Space.World);
			if (gameObject.transform.position.y > -8) {
				gameObject.transform.position = new Vector3 (this.transform.position.x, -8, -1);

				State = 2;
			}
			if (CanEmit) {
				CurrentSplash = Instantiate (Splash, new Vector3 (transform.position.x, -10.1f, -1.5f), Quaternion.identity);
				CanEmit = false;
			}
		}
		/*---------------------------------------------------- Etat Monte ----------------------------------------*/
		else if (State == 1) {
			CanReverse = true;
				Destroy (this.gameObject.GetComponent ("PolygonCollider2D"));	
				gameObject.AddComponent<PolygonCollider2D> ();
				gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;

			anim.SetBool ("Plonge", false);
			anim.SetBool ("Idle", true);
			anim.SetBool ("Attack1", false);
			anim.SetBool ("Attack2", false);
			gameObject.transform.Translate (Vector3.up * 2f * Time.deltaTime, Space.World);
			if (gameObject.transform.position.y > -8) {
				gameObject.transform.position = new Vector3 (this.transform.position.x, -8, -1);

				State = 2;
			}
			if (CanEmit) {
				CurrentSplash = Instantiate (Splash, new Vector3 (transform.position.x, -10.1f, -1.5f), Quaternion.identity);
				CanEmit = false;
			}
		/*---------------------------------------------------- Etat Static ----------------------------------------*/
		} else if (State == 2) {
			if (CanAttack) {
				StartCoroutine(WaitToAttack());
				CanAttack = false;
			}
		}
		/*---------------------------------------------------- Etat Plonge  ----------------------------------------*/
		else if (State == 3) {

			if (Attaque == 2) {

				gameObject.transform.Translate (Vector3.down * 2f * Time.deltaTime, Space.World);
				if (gameObject.transform.position.y < -9.5f) {
					gameObject.transform.position = new Vector3 (this.transform.position.x, -9.5f, -1);
					State = 6;
				}
			}
		}
		/*---------------------------------------------------- Etat Plonge Verticalement ----------------------------------------*/
		else if (State == 4) {

	
			anim.SetBool ("Plonge", true);
			anim.SetBool ("Idle", false);
			anim.SetBool ("Attack1", false);
			anim.SetBool ("Attack2", false);

			Destroy (this.gameObject.GetComponent ("PolygonCollider2D"));	
			gameObject.AddComponent<PolygonCollider2D> ();
			gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;

			gameObject.transform.Translate (Vector3.down * 2.5f * Time.deltaTime, Space.World);
			if (Attaque == 5) {
				if (gameObject.transform.position.y < -13f) {
					gameObject.transform.position = new Vector3 (this.transform.position.x, -13f, -1);

					State = 5;
				}
			} else {
				if (gameObject.transform.position.y < -11.5f) {
					gameObject.transform.position = new Vector3 (this.transform.position.x, -11.5f, -1);

					State = 5;
				}
			}
		}
		/*---------------------------------------------------- Etat Attaque 1 ----------------------------------------*/
		else if (State == 5) {
			if (!CanAttack) {
				anim.SetBool ("Plonge", false);
				anim.SetBool ("Idle", false);
				anim.SetBool ("Attack1", true);
				anim.SetBool ("Attack2", false);
				StartCoroutine (WaitToEndAttack ());
				CanAttack = true;
			}
			if(Attaque == 5){


				if (CanReverse) {
					gameObject.transform.Translate (Vector3.left * 3 * Time.deltaTime, Space.World);
				} else {
					gameObject.transform.Translate (Vector3.right * 3 * Time.deltaTime, Space.World);
					if (gameObject.transform.position.x > 101) {
						gameObject.transform.position = new Vector3 (101, this.transform.position.y, -1);
					

					}
				}
				if (gameObject.transform.position.x < 90) {
					CanReverse = false;
				}

			
			}
		}
		/*---------------------------------------------------- Etat Attaque 2 ----------------------------------------*/
		else if (State == 6) {
			if (!CanAttack) {
				anim.SetBool ("Plonge", false);
				anim.SetBool ("Idle", false);
				anim.SetBool ("Attack2", true);
				anim.SetBool ("Attack1", false);
				StartCoroutine (WaitToEndAttack ());
				CanAttack = true;
			}
		}




	}
	IEnumerator WaitToAttack()
	{
		if(Attaque == 1 || Attaque == 5){
		yield return new WaitForSeconds(Random.Range(3,6));
		}else{
			yield return new WaitForSeconds(Random.Range(1,4));
		}

		//Choix de l'attaque
		if(PV > InitialPV/2){
			Attaque = Random.Range (1, 4);
		}else{
			Attaque = Random.Range (2, 6);
		}

		//Lancement de l'attaque
		if (Attaque == 1) {
			State = 4;
		}else if (Attaque == 2){
			State = 3;
		}
		else if (Attaque == 3){
			StartCoroutine (WaitToEndAttack ());

		}else if (Attaque == 4){
			StartCoroutine (WaitToEndAttack ());

		}else if (Attaque == 5){
			State = 4;

		}
	}

	IEnumerator WaitToEndAttack()
	{
		Rigidbody2D clone;
		if (Attaque == 1) {
			
			for (int i = 1; i <= Random.Range (7, 15); i++) {
				clone = Instantiate (Ball, new Vector3 (transform.position.x - 0.5f, transform.position.y + 0.2f, -0.5f), Quaternion.identity) as Rigidbody2D;
				clone.velocity = transform.TransformDirection (Vector2.up * 6);
				yield return new WaitForSeconds (0.5f);
			}

		} else if (Attaque == 2) {
			clone = Instantiate (steam, new Vector3 (transform.position.x - 3f, transform.position.y + 1.35f, -1.5f), Quaternion.identity) as Rigidbody2D;
			yield return new WaitForSeconds (3f);
			Instantiate (smokePart, new Vector3 (clone.transform.position.x-3f, clone.transform.position.y+1, -1.5f), Quaternion.identity);
			Destroy (clone.transform.gameObject);
		}
		else if (Attaque == 3) {
			anim.Play ("Boss1Attack3");
			clone = Instantiate (BossBall, new Vector3 (transform.position.x - 2.6f, transform.position.y + 1f, -0.5f), Quaternion.identity) as Rigidbody2D;
			clone.velocity =  (new Vector3(GameObject.FindGameObjectWithTag("player").transform.position.x,GameObject.FindGameObjectWithTag("player").transform.position.y-1.5f,1) - transform.position).normalized * 6; 
			CanAttack = true;
		}else if (Attaque == 4) {
			for (int i = 1; i <= Random.Range (5, 8); i++) {
				anim.Play ("Boss1Attack3");
				clone = Instantiate (BossBall, new Vector3 (transform.position.x - 2.6f, transform.position.y + 1f, -0.5f), Quaternion.identity) as Rigidbody2D;
				clone.velocity = (new Vector3 (GameObject.FindGameObjectWithTag ("player").transform.position.x, GameObject.FindGameObjectWithTag ("player").transform.position.y - 1.5f, 1) - transform.position).normalized * 6; 
				yield return new WaitForSeconds (0.5f);
			}
			CanAttack = true;
		}else if (Attaque == 5) {
			for (int i = 1; i <= 16; i++) {
				clone = Instantiate (Ball, new Vector3 (transform.position.x - 1f, transform.position.y + 3f, -0.5f), Quaternion.identity) as Rigidbody2D;
				clone.velocity = transform.TransformDirection (Vector2.up * 6);
				yield return new WaitForSeconds (0.5f);
			}
		}
		State = 1;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "plateforme") {
			audiosource.PlayOneShot (explosion, 0.3f);
			Destroy(col.gameObject);
		}
	}
	public void Blesser(int degats){
		PV-=degats;
	}

    void ResetCollider (int State)
    {
        for (int i = 0; i < ColliderBossState.Length; i++)
        {
            if (i != State)
            {
                ColliderBossState[i].GetComponent<PolygonCollider2D>().enabled = false;
            } else
            {
                ColliderBossState[i].GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

}
