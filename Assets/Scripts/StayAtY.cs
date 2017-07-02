using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAtY : MonoBehaviour {

	float why;
	private void Awake()
	{
		why = transform.position.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 1.8302f, transform.position.z);
	}
}
