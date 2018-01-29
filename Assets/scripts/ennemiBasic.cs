using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemiBasic : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	public int PV;
	public float pos1X;
	public float pos2X;

	public float pos1Y;
	public float pos2Y;

	public bool Horizontalmoving;
	public bool MoveLeft;

	public float speed;
	public Rigidbody2D spell;
	public Rigidbody2D rb2D;
	public bool CanAttack;
	public Animator anim;

	public AudioSource audiosource;
	public AudioClip spell1;
	public AudioClip cris;


	// Use this for initialization
	void Start () {
		CanAttack = true;
		spriteRenderer = GetComponent<SpriteRenderer> (); 
		anim = GetComponent<Animator> (); 
		audiosource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {



		float step = speed * Time.deltaTime;
		if (Horizontalmoving) {
			if(transform.position.x <= pos1X){

				MoveLeft = false;
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}
			else if(transform.position.x >= pos2X){

				MoveLeft = true;
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}

			if (MoveLeft) {
				Vector3 fwd = transform.TransformDirection (Vector3.left)*4;
				Debug.DrawRay(transform.position, fwd, Color.green);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd,5);
				if (hit.collider != null) {
					if (CanAttack) {
						Attaquer (0);
						CanAttack = false;
					}
				}
					if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("ennemi1Attack")) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector2 (pos1X, transform.position.y), step);
					}
			} else {
				Vector3 fwd = transform.TransformDirection (Vector3.right)*4;
				Debug.DrawRay(transform.position, fwd, Color.green);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd,5);
				if (hit.collider != null) {
					
					if (CanAttack) {
						Attaquer (1);
						CanAttack = false;
					}
				}
					if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("ennemi1Attack")) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector2 (pos2X, transform.position.y), step);
					}
			}
		
		}
	


		if (PV <= 0) {
			Destroy (this.gameObject);
		}

	
	}

	public void Blesser(int degats){
		audiosource.PlayOneShot (cris, 0.8f);
		PV-=degats;
	}
	public void Attaquer(int dir){
		audiosource.PlayOneShot (spell1,1f);
		anim.Play("ennemi1Attack");
		Rigidbody2D clone;
		clone = Instantiate (spell, transform.position, Quaternion.identity) as Rigidbody2D;
		if (dir == 0) {
			clone.velocity = transform.TransformDirection (Vector2.left * 5);
		} else if (dir==1) {
			clone.velocity = transform.TransformDirection (Vector2.right * 5);
		}

		CanAttack = false;
		StartCoroutine(WaitToAttack());
	}
	IEnumerator WaitToAttack()
	{

		yield return new WaitForSeconds(2);
		CanAttack = true;
	}
}
