using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public bool Lvl1Checkpoint;
	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	void Start () {
		Lvl1Checkpoint = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
