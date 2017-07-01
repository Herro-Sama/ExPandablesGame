using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour {
    public Rigidbody selfRigid;
    public Collider coll;


    private bool StartTimer = false;
    private Vector3 StartLocation;
    private float ResetTimer = 150f;

    // Use this for initialization
    void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        selfRigid = GetComponent<Rigidbody>();
        StartLocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (StartTimer == true)
        {
                ResetTimer -= 1f;
        }
        if (ResetTimer <= 0)
        {
            ResetTimer = 150f;
            transform.position = StartLocation;
            selfRigid.useGravity = false;
            StartTimer = false;
 
        }


    }

    void OnTriggerEnter(Collider PlayerCollision)
    {
        if (PlayerCollision.tag == "Player")
        {
            StartTimer = true;
            selfRigid.useGravity = true;

        }
    }

}
