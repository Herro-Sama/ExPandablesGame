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

    [SerializeField]
    private float turnSpeed = 300f;

    [SerializeField]
    private float attackRange = 5f;

    private float counter;
    private float nextWaitTime;

    private IEnumerator turnRight;
    private IEnumerator turnLeft;

    private Animator animator;
	public float damage = 3000;
    private Transform target;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

        maxLeft = transform.parent.GetChild(1).position.x;
		maxRight = transform.parent.GetChild (2).position.x;

        movingRight = true;
        still = false;
        nextWaitTime = (Random.Range(minWaitTime*100, maxWaitTime*100))/100;

        turnLeft = TurnToLeft(transform.GetChild(0));
        turnRight = TurnToRight(transform.GetChild(0));
    }

    private void Start()
    {
        GameObject ply = GameObject.FindGameObjectWithTag("Player");
        if(ply)
            target = ply.transform;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter > nextWaitTime)
        {
            Flip();
            counter = 0;
            nextWaitTime = (Random.Range(minWaitTime * 100, (still ? maxWaitTime/2 : maxWaitTime) * 100)) / 100;
        }

        if (!still)
            transform.Translate((movingRight ? Vector3.right : Vector3.left) * Time.deltaTime * walkSpeed);
        
        if(movingRight && transform.position.x > maxRight)
        {
            movingRight = false;
            StopCoroutine(turnLeft);
            StopCoroutine(turnRight);

            turnLeft = TurnToLeft(transform.GetChild(0));
            StartCoroutine(turnLeft);
        }
        
        else if (transform.position.x < maxLeft)
        {
            movingRight = true;
            StopCoroutine(turnLeft);
            StopCoroutine(turnRight);

            turnRight = TurnToRight(transform.GetChild(0));
            StartCoroutine(turnRight);
        }

        rektCooldown -= Time.deltaTime;

        // Attack
        if (target == null)
        {
            return;
        }
        if((movingRight && target.position.x > transform.position.x) || (!movingRight && target.position.x < transform.position.x))
        {
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                still = false;
                Flip();
                animator.SetBool("isHitting", true);
                Invoke("GetRekt", 0.35f);
            }
            else
            {
                animator.SetBool("isHitting", false);
            }
        }      
        else
        {
            animator.SetBool("isHitting", false);
        }
    }

    private void Flip()
    {
        still = !still;
        if(still)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    private IEnumerator TurnToRight(Transform tran)
    {
        while (tran.eulerAngles.y > 90)
        {
            tran.Rotate(0, -(Time.deltaTime * turnSpeed), 0);
            yield return null;
        }
    }

    private IEnumerator TurnToLeft(Transform tran)
    {
        while (tran.eulerAngles.y < 270)
        {
            tran.Rotate(0, (Time.deltaTime * turnSpeed), 0);
            yield return null;
        }
    }

    float rektCooldown = 0f;
    private void GetRekt()
    {
        if(rektCooldown > 0)
        {
            return;
        }
		target.GetComponent<Rigidbody>().AddExplosionForce(damage, transform.position, 200f);
        CancelInvoke("GetRekt");
        rektCooldown = 1;
    }
}