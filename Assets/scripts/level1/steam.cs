using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steam : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<CapsuleCollider2D> ().enabled = false;
		StartCoroutine(WaitToCollide());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator WaitToCollide()
	{

		yield return new WaitForSeconds(0.3f);
		gameObject.GetComponent<CapsuleCollider2D> ().enabled = true;

	}
}
