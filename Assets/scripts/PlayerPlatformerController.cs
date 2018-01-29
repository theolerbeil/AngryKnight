
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 15;
	public float amoSpeed;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	public Rigidbody2D projectile;
	public bool CanFire;
	public float cooldown;
	public int HP;
	public bool CanBeHurt;
	public AudioSource audiosource;
	public AudioClip hurt;
	public AudioClip jump;
	public AudioClip fire;
	public AudioClip plouf;
	public GameObject UIManager;
	public GameObject SmokePart;
	public GameObject WaterParts;
	public bool CanSmoke;
	public bool IsCkeckPoint;
	public bool CanRespawn;

	// Use this for initialization
	void Awake () 
	{
		CanRespawn = true;
		IsCkeckPoint = false;
		audiosource = GetComponent<AudioSource> ();
		CanFire = true;
		spriteRenderer = GetComponent<SpriteRenderer> (); 
		anim = GetComponent<Animator> ();
		CanBeHurt = true;
		CanSmoke = true;
		UIManager = GameObject.Find ("UIManager");
	}

	protected override void ComputeVelocity ()
	{

		if(grounded){
			if (CanSmoke) {
				Instantiate (SmokePart, new Vector3(transform.position.x,transform.position.y-0.25f,-0.5f), Quaternion.identity);
				CanSmoke = false;
			}
		}

		//STATES
		if (gameObject.transform.position.y < -20) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		if (!CanBeHurt) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.5f);
		} else {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
		}
		if (HP <= 0) {
			Mort ();
		} 

		//MOVING
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if ((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("Jump2")) && grounded) {
			audiosource.PlayOneShot (jump, 0.3f);
			CanSmoke = true;
			velocity.y = jumpTakeOffSpeed;
		} else if (Input.GetButtonUp ("Jump") || Input.GetButtonUp ("Jump2")) {
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}



		//SHOOTING SECTION
		if (CanFire) {
			if (Input.GetButtonDown ("Fire1")||Input.GetButtonDown ("Fire2")) {
				Rigidbody2D clone;
				clone = Instantiate (projectile, transform.position, Quaternion.identity) as Rigidbody2D;
				audiosource.PlayOneShot (fire, 0.3f);


				if (spriteRenderer.flipX) {
					clone.velocity = transform.TransformDirection (Vector2.left * amoSpeed);
				} else {
					clone.velocity = transform.TransformDirection (Vector2.right * amoSpeed);
				}
				CanFire = false;
				StartCoroutine(WaitToShoot());
			}
		}
		//FLIP SPRITE
		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.0f) : (move.x < 0.0f));
		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

	

		targetVelocity = move * maxSpeed;



		//ANNIMATION
		if (move.x != 0f) {
			anim.SetBool ("IsRuning", true);
			anim.SetBool ("IsIdle", false);
		} else {
			anim.SetBool ("IsRuning", false);
			anim.SetBool ("IsIdle", true);
		}
		if (grounded == false) {
			anim.SetBool ("IsJumping", true);
			anim.SetBool ("IsRuning", false);
			anim.SetBool ("IsIdle", false);
			if (velocity.y < 2) {
				anim.SetBool ("IsJumping2", true);
			}
		} else {
			anim.SetBool ("IsJumping", false);
			anim.SetBool ("IsJumping2", false);
		}
	}



	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "kill")
		{
			Mort ();
		}
		if(col.gameObject.tag == "ennemi")
		{
			Degats ();
		}
		if(col.gameObject.tag == "projectile")
		{
			Degats ();
		}
		if(col.gameObject.tag == "water")
		{
			audiosource.PlayOneShot (plouf, 0.5f);
			Instantiate (WaterParts, new Vector3(transform.position.x,transform.position.y-0.25f,-0.5f), Quaternion.identity);
		}
		if(col.gameObject.tag == "pic")
		{
			if (CanBeHurt) {
				audiosource.PlayOneShot (hurt, 0.5f);
				HP--;

				UIManager.GetComponent<UI> ().Actualiser ();


				if (velocity.y <0) {
					gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1000);
				}
				else if (velocity.y >0) {
					gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.down * 200);
				}
				else if (velocity.y ==0) {
					if (spriteRenderer.flipX) {
						gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 300);

					} else {
						gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 300);


					}
				}

				CanBeHurt = false;
				StartCoroutine(WaitToBeHurtable());
			}



		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		
		if(col.gameObject.tag == "MovingPlateform")
		{
			this.transform.SetParent (col.transform);
		}
		if(col.gameObject.tag == "ennemi")
		{
			Degats ();
		}
	
	}

	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag == "MovingPlateform"){
			this.transform.SetParent (null);

		}
	}

	IEnumerator WaitToShoot()
	{

		yield return new WaitForSeconds(cooldown);
		CanFire = true;
	}
	IEnumerator WaitToBeHurtable()
	{

		yield return new WaitForSeconds(1);
		CanBeHurt = true;
	}
	public void Degats(){
		if (CanBeHurt) {
			audiosource.PlayOneShot (hurt, 0.5f);
			HP--;					
			UIManager.GetComponent<UI> ().Actualiser ();

			if (spriteRenderer.flipX) {					
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 300);
				if (velocity.y < 0) {
					gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1100);
				}
			} else {
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 300);
				if (velocity.y < 0) {
					gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1100);
				}

			}
			CanBeHurt = false;
			StartCoroutine(WaitToBeHurtable());

		}
	}
	public void Mort(){
		if (!IsCkeckPoint) {
			if (CanRespawn == true) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

	}


}