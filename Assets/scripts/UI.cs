using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
public class UI : MonoBehaviour {
	public GameObject[] heart;
	public int nbrHeart;
	public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindGameObjectWithTag ("player");
	}
	public void Actualiser(){
		nbrHeart = Player.GetComponent<PlayerPlatformerController> ().HP;
		for (int i = 1; i <= heart.Length; i++) {
			if (i <= nbrHeart) {
				heart [i-1].SetActive(true);
			} else {
				heart [i-1].SetActive(false);
			}
		}
	}
}
