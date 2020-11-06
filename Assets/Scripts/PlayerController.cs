using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    


    //Ground / Inputcheck
    private float moveInput;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Jump
    private float jumpTimeCounter;
    public float jumpForce;
    public float jumpTime;
    private bool isJumping;
    private bool facingRight = true;
    private int extraJumps;
    public int extraJumpsValue;


    //Wallslide
    //bool isTouchingFront;
    //public Transform frontCheck;
    //bool wallSliding;
    //public float wallSlidingSpeed;
    //bool wallJumping;
    //public float xWallForce;
    //public float yWallForce;
    //public float wallJumpTime;

    //Dash
    public float dashSpeed = 30f;
    public float startDashTime = 0.1f;
    private float dashTime;
    private int direction;

    //Animation
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();

        //Dash
        dashTime = startDashTime;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private void Update()
    {

       

        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        //if(isTouchingFront == true && isGrounded == false && moveInput != 0)
        //{
        //    wallSliding = true;
        //}else
        //{
        //    wallSliding = false;
        //}

        //if(wallSliding)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        //}

        //if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        //{
        //    wallJumping = true;
        //    Invoke("SetWallJumpingToFalse", wallJumpTime);
        //}

        //if (wallJumping == true)
        //{
        //    rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        //}

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        //void SetWallJumpingToFalse()
        //{
        //    wallJumping = false;
        //}

        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }


        void Flip()
        {
            facingRight = !facingRight;

            transform.Rotate(0f, 180f, 0f);
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (moveInput < 0)
                {
                    direction = 1;
                }
                else if (moveInput > 0)
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }
}