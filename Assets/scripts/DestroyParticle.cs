using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitToDestroy());

	}


	IEnumerator WaitToDestroy()
{

	yield return new WaitForSeconds(2f);
		Destroy (this.gameObject);
}
}
