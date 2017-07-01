using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField]
    private GameObject act;
	// Use this for initialization
	void Start () {
        act.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
