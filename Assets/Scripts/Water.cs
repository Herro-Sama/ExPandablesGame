using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public Transform FarEnd;

    private Vector3 StartPoint;
    private Vector3 EndPoint;
    private float TimeToComplete = 10f;

    // Use this for initialization
    void Start ()
    {
        StartPoint = transform.position;
        EndPoint = FarEnd.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / TimeToComplete, 1f)));
	}
     void OnTriggerEnter(Collider Player)
    {
        if (Player.name == "Player")
        {
            Player.gameObject.SendMessage("InWater");
        }
    }

}
