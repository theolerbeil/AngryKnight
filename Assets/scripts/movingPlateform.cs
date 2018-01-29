using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movingPlateform : MonoBehaviour {
	public float pos1X;
	public float pos2X;

	public float pos1Y;
	public float pos2Y;

	public bool Horizontalmoving;
	public bool MoveLeft;

	public float speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (Horizontalmoving) {
			if(transform.position.x <= pos1X){
				
				MoveLeft = false;
			
			}
			else if(transform.position.x >= pos2X){
				
				MoveLeft = true;
			}

			if (MoveLeft) {
				transform.position = Vector3.MoveTowards(transform.position, new Vector2(pos1X,transform.position.y), step);
			} else {
				transform.position = Vector3.MoveTowards(transform.position, new Vector2(pos2X,transform.position.y), step);
			}




		}
	}
}
