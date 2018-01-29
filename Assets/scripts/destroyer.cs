using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour {
	public float time;
	// Use this for initialization
	void Start () {
		StartCoroutine(WaitToDestroy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator WaitToDestroy()
	{

		yield return new WaitForSeconds(time);
		Destroy (this.gameObject);
	}
}
