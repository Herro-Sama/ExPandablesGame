using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float playerChargeJumpForce = 8.0f;
    private float OnGroundTimer = 1f;
    private bool playerOnGround = false;
    private int playerCurrentJumps = 0;
    private int playerMaxJumpCombo = 3;
    private bool PlayerLongJumping = false;
    private float TotalPlayerOnGroundTime = 1f;

    public float JumpComboDecayRate = 0.15f;
    public float PlayerSpeed = 10.0f;
    public float PlayerJumpChargeSpeed = 0.2f;
    public float PlayerJumpMinHeight = 8.0f;
    public float PlayerJumpMaxHeight = 15.0f;

    public Rigidbody rb;

    // Animation
    private bool waved = false;
    private IEnumerator turnRight;
    private IEnumerator turnLeft;
    float idleTime = 0f;
    float timeUntilWave = 4.5f;
    private bool aboutToJump = false;
    [SerializeField]
    private float turnSpeed = 300f;
    [SerializeField]
    private Animator charAnimator;
    private bool facingRight = true;

    private float movement = 0;

    private bool FacingRight
    {
        get { return facingRight; }
        set
        {
            if (value && !facingRight)
            {
                FaceRight();
                facingRight = true;
            }
            else if (!value && facingRight)
            {
                FaceLeft();
                facingRight = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Transform panda = charAnimator.transform;
        turnLeft = TurnToLeft(panda);
        turnRight = TurnToRight(panda);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerHorizontalMovement = Input.GetAxis("Horizontal");

        // Animation
        idleTime += Time.deltaTime;
        if (Input.anyKeyDown || !playerOnGround)
        {
            idleTime = 0f;
        }
        if (idleTime >= timeUntilWave)
        {
            charAnimator.SetBool("isWaving", true);
            if (!waved)
            {
                FaceRight();
                GameManager.instance.PlaySound(3);
                waved = true;
            }
        }
        else
        {
            charAnimator.SetBool("isWaving", false);
            if (waved)
                waved = false;
        }
        if (playerOnGround && !aboutToJump)
        {
            charAnimator.SetBool("isJumping", false);
        }

        if (playerHorizontalMovement > 0.005f)
        {
            if (playerOnGround)
                charAnimator.SetBool("isRunning", true);
            else
                charAnimator.SetBool("isRunning", false);


            FacingRight = true;
        }
        else if (playerHorizontalMovement < -0.005f)
        {
            if (playerOnGround)
                charAnimator.SetBool("isRunning", true);
            else
                charAnimator.SetBool("isRunning", false);

            FacingRight = false;
        }
        else
        {
            charAnimator.SetBool("isRunning", false);
        }

        playerHorizontalMovement *= Time.deltaTime * PlayerSpeed;
        movement = playerHorizontalMovement;

        if (Input.GetButton("Jump"))
        {
            playerChargeJumpForce += PlayerJumpChargeSpeed;
            // Animation
            aboutToJump = true;
            AboutToJumpCooldown();
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (OnGroundTimer == 0)
            {
                playerCurrentJumps = 0;
                OnGroundTimer = TotalPlayerOnGroundTime;
            }

            if (playerChargeJumpForce >= PlayerJumpMinHeight)
            {
                PlayerLongJumping = true;
            }

            Jump(playerHorizontalMovement);
            OnGroundTimer = TotalPlayerOnGroundTime;

            if (playerCurrentJumps == playerMaxJumpCombo)
            {
                print("Jump Reset");
                playerCurrentJumps = 0;
            }

        }
    }


    void FixedUpdate()
    {
        
        if (playerOnGround == true)
        {
            OnGroundTimer -= JumpComboDecayRate;
            if (OnGroundTimer <= 0 && playerCurrentJumps >= 1)
            {
                print("Jump Reset because of TimeOut");
                playerCurrentJumps = 0;
            }
        }

        transform.Translate(movement, 0, 0);
    }

    void Jump(float HorizontalMotion)
    {
        if (playerOnGround == true)
        {
            GameManager.instance.PlaySound(4);

            playerOnGround = false;
            OnGroundTimer = TotalPlayerOnGroundTime;
            if (playerCurrentJumps == 0)
            {
                if (playerChargeJumpForce >= PlayerJumpMaxHeight)
                {
                    Mathf.Clamp(playerChargeJumpForce, 0, PlayerJumpMaxHeight);
                    playerCurrentJumps++;
                    rb.velocity = new Vector3(0, PlayerJumpMaxHeight, 0);
                    playerChargeJumpForce = PlayerJumpMinHeight;
                }

                else
                {
                    playerCurrentJumps++;
                    rb.velocity = new Vector3(0, playerChargeJumpForce, 0);
                    playerChargeJumpForce = PlayerJumpMinHeight;

                }
            }
            else
            {
                if (playerChargeJumpForce >= PlayerJumpMaxHeight)
                {
                    Mathf.Clamp(playerChargeJumpForce, 0, PlayerJumpMaxHeight);
                    playerCurrentJumps++;
                    if (PlayerLongJumping == true)
                    {
                        rb.velocity = new Vector3(0, (PlayerJumpMaxHeight * (playerCurrentJumps / 2.5f)), 0);
                        playerChargeJumpForce = PlayerJumpMinHeight;
                        PlayerLongJumping = false;
                    }
                    else
                    {
                        rb.velocity = new Vector3(0, (PlayerJumpMaxHeight * (playerCurrentJumps / 1.5f)), 0);
                        playerChargeJumpForce = PlayerJumpMinHeight;
                    }
                }

                else
                {
                    playerCurrentJumps++;
                    if (PlayerLongJumping == true)
                    {
                        rb.velocity = new Vector3((HorizontalMotion * 20), (PlayerJumpMaxHeight * (playerCurrentJumps / 3f)), 0);
                        playerChargeJumpForce = PlayerJumpMinHeight;
                        PlayerLongJumping = false;
                    }
                    else
                    {
                        rb.velocity = new Vector3(0, (playerChargeJumpForce * (playerCurrentJumps / 1.5f)), 0);
                        playerChargeJumpForce = PlayerJumpMinHeight;
                    }

                }
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerOnGround = true;
        }

    }

    // Animation.
    private void FaceRight()
    {
        //StopAllCoroutines();     
        StopCoroutine(turnLeft);
        StopCoroutine(turnRight);
        Transform panda = charAnimator.transform;
        turnRight = TurnToRight(panda);
        StartCoroutine(turnRight);
        //StartCoroutine(turnRight);
    }

    private void FaceLeft()
    {
        StopCoroutine(turnLeft);
        StopCoroutine(turnRight);
        Transform panda = charAnimator.transform;
        turnLeft = TurnToLeft(panda);
        StartCoroutine(turnLeft);
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

    private void AboutToJumpCooldown()
    {
        charAnimator.SetBool("isJumping", true);
        Invoke("ResetAboutToJump", 0.7f);
    }

    private void ResetAboutToJump()
    {

        aboutToJump = false;
    }
    //End of Script		
}