using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed;
    public int jumpForce;

    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    public PhysicsMaterial2D pogoJump;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        //Could be used as a double check to ensure ground check is set if we are null
        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("Ground Check").transform;
        }

        if (speed <= 0)          
        {
            speed = 5.0f;          
        }

        if (jumpForce <= 0)      
        {
            jumpForce = 300;         
        }

        if (groundCheckRadius <= 0)     
        {
            groundCheckRadius = 0.01f;    
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        

            //float verticalInput = Input.GetAxisRaw("Vertical");
            float previousGroundHeight = -3.868245f;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);  

        if (Input.GetButtonDown("Jump") && isGrounded ) //find the jump button
        {
            rb.velocity = Vector2.zero;                  //makes the rigidbody velocity 0
            rb.AddForce(Vector2.up * jumpForce);       //add force in .up direction(positive button) * jumpforce
        }

        //if (Input.GetButtonDown("Vertical") && !isGrounded)
        // {
        //     rb.velocity = Vector3.down;
        //     rb.AddForce(Vector3.down * speed);
        // }

        //Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        //rb.velocity = moveDirection;

        if (curPlayingClip.Length > 0)
        {
            if (curPlayingClip[0].clip.name != "Fire")
            {
                Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        
        if (!isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("pogoJump", true);
            rb.sharedMaterial = pogoJump;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("pogoJump", false);
            rb.sharedMaterial = null;
        }

        if (anim.GetBool("pogoJump") && isGrounded)
        {
            float currentGroundHeight = transform.position.y;
            if (currentGroundHeight > previousGroundHeight + 0.2)
            {
                previousGroundHeight = currentGroundHeight;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);

            }
        }

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        //anim.SetFloat("pogoJump", Mathf.Abs(verticalInput));
        anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("isAttack", isAttack);

        if (horizontalInput != 0)                                 
        {
            sr.flipX = horizontalInput < 0;
        }

    }

    
}
