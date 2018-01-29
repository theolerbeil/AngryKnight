using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {
	public int StageState;
	public GameObject Player;
	public GameObject CurrentPlayer;
	public GameObject Cam;
	public GameObject boss;
	public GameObject queue;
	public bool locker;
	public GameObject fish;
	public bool IsCkeckPoint;
	GameObject GameManager;

	// Use this for initialization
	void Start () {
		

		/*------------ Spawn du joueur-----------------*/
		GameManager = GameObject.Find ("GameManager");//recuperer le gqma manager
		if (GameManager.GetComponent<GameManager> ().Lvl1Checkpoint) { // Si le level 1 a ete checkpoint
			Instantiate (Player, new Vector3 (72.5f, -7, 0), Quaternion.identity);
		} else {
			Instantiate (Player, new Vector3 (-5, -4, 0), Quaternion.identity); // sinon
		}


		IsCkeckPoint = false;
		StageState = 1;
		locker = true;
		StartCoroutine(FishSpasn());

	}
	
	// Update is called once per frame
	void Update () {
		CurrentPlayer = GameObject.FindGameObjectWithTag ("player");
		Cam = GameObject.FindGameObjectWithTag ("MainCamera");
		if(CurrentPlayer.transform.position.x > 98){
			StageState = 2;

		}



		if(StageState == 2){
			
			if (locker) {
				GameObject ActualBoss = Instantiate (boss, new Vector3 (101, -15, -1), Quaternion.identity);
				GameObject ActualQueue = Instantiate (queue, new Vector3 (87, -15, -1), Quaternion.identity);
				ActualBoss.GetComponent<boss1> ().queue = ActualQueue;
				Cam.GetComponent<cam> ().minX = 98;
				Cam.transform.position = new Vector3 (Cam.transform.position.x,-7.5f,-10);

				locker = false;
			
			}
		}
	}

	IEnumerator FishSpasn()
	{
		if(StageState == 2){
		yield return new WaitForSeconds(10);
		}else{
			yield return new WaitForSeconds(5);
		}
		Instantiate (fish, new Vector3(Random.Range(110,130),-10f,-3f), Quaternion.identity);
		StartCoroutine(FishSpasn());

	}
}
