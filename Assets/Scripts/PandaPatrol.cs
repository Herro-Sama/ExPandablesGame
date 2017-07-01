using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaPatrol : MonoBehaviour
{
    private float maxLeft;
    private float maxRight;
    private bool movingRight = true;
    private bool still = false;

    [SerializeField]
    private float minWaitTime = 2f;
    [SerializeField]
    private float maxWaitTime = 5f;

    [SerializeField]
    private float walkSpeed = 1f;

    private float counter;
    private float nextWaitTime;

    private void Awake()
    {
        maxLeft = transform.parent.GetChild(1).position.x;
        maxRight = transform.parent.GetChild(2).position.x;

        Debug.Log(maxRight);

        movingRight = true;
        still = false;
        nextWaitTime = (Random.Range(minWaitTime*100, maxWaitTime*100))/100;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter > nextWaitTime)
        {
            Flip();
            counter = 0;
            nextWaitTime = (Random.Range(minWaitTime * 100, maxWaitTime * 100)) / 100;
        }

        if (!still)
            transform.Translate((movingRight ? Vector3.right : Vector3.left) * Time.deltaTime * walkSpeed);


        Debug.Log(transform.position);
        
        if(movingRight && transform.position.x > maxRight)
        {
            movingRight = false;
        }
        
        else if (transform.position.x < maxLeft)
        {
            movingRight = true;
        }
        
    }

    private void Flip()
    {
        still = !still;
    }
}