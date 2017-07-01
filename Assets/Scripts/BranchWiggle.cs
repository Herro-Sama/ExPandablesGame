using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchWiggle : MonoBehaviour {
    public Rigidbody selfRigid;
    public Collider coll;
	
    private Vector3 StartLocation;
    private float ResetTimer = 1.5f;
    
    // Use this for initialization
	void Start ()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        selfRigid = GetComponent<Rigidbody>();
        StartLocation = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (selfRigid.useGravity == true)
        {
            StartCoroutine(WaitForReset(ResetTimer));
        }
        if (selfRigid.useGravity == false)
        {
            ResetTimer = 1.5f;
            transform.position = StartLocation;
        }

	}

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    void OnTriggerEnter(Collider PlayerCollision)
    {
        if (PlayerCollision.name == "Player")
        {
            selfRigid.useGravity = true;
        }
    }
    private IEnumerable WaitForReset(float ResetTimer)
    {
        while (true)
        {
            yield return new WaitForSeconds(ResetTimer);

        }
    }
}
