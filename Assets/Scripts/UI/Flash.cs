using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {
	public float flashTime = 0.8f;
	Image img;
	void Start () {
		img = GetComponent<Image> ();
		InvokeRepeating ("FlashImg", flashTime, flashTime);
	}
	
	// Update is called once per frame
	void FlashImg () 
	{
		img.enabled = !img.enabled;
	}

	public void Disable()
	{
		Destroy (gameObject);
	}
}
