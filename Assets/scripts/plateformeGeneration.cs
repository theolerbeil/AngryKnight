using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeGeneration : MonoBehaviour {
	public float[] yPlateforme;
	public float[] xPlateforme;
	public GameObject plateforme;
	// Use this for initialization
	void Start () {
		yPlateforme [0] = -4;
		xPlateforme[0] = 10;
		Instantiate(plateforme, new Vector2(xPlateforme [0],yPlateforme [0]), Quaternion.identity);
		for (int i = 1; i < yPlateforme.Length; i++) {
			xPlateforme [i] = xPlateforme [i - 1] + 8;
			yPlateforme [i] = Random.Range(yPlateforme [i-1],yPlateforme [i-1]+Random.Range(-6,2));
			Instantiate(plateforme, new Vector2(xPlateforme [i],yPlateforme [i]), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
