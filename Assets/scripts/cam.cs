using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour {
	public GameObject player;
	public float minX;
	public float maxX;
	public float minY;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		if (player.transform.position.x >= minX && player.transform.position.x <= maxX) {
			if (player.transform.position.y >= minY) {
				this.transform.SetParent (player.transform);
				this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			} else {
				this.transform.SetParent (null);
			}
		
		} else {
			this.transform.SetParent (null);
		}



	}
}
