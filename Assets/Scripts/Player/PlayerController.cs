using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    private int _lives = 1;
    public int maxLives = 3;


    public int lives
    {
        get { return _lives; }
        set
        {

            //if (_lives > value)
            //respawn code here 

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            //if (_lives > 0)
            //gameover

            Debug.Log("Lives Set To: " + lives.ToString());
        }
    }


    public float speed;
    public int jumpForce;

    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    bool coroutineRunning = false;

    public PhysicsMaterial2D pogoJump;
    bool canPrePutt = false;



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
        float verticalInput = Input.GetAxisRaw("Vertical");
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);

        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) //find the jump button
        {
            rb.velocity = Vector2.zero;                  //makes the rigidbody velocity 0
            rb.AddForce(Vector2.up * jumpForce);       //add force in .up direction(positive button) * jumpforce
        }

        if (curPlayingClip.Length > 0)
        {
            if (canPrePutt)
            {
                if (Mathf.Abs(horizontalInput) > 0)
                {
                    anim.SetBool("putt", true);
                }
            }
            if (curPlayingClip[0].clip.name == "Lookup")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetBool("combo", true);
                }
            }
            else if (curPlayingClip[0].clip.name != "Fire")
            {
                Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        if (Input.GetButtonUp("Fire1") || verticalInput < 0.1)
        {
            anim.SetBool("combo", false);
        }


        //Pogo Jump Key Press
        if (!isGrounded && Input.GetButtonDown("Vertical"))
        {
            anim.SetBool("pogoJump", true);
            rb.sharedMaterial = pogoJump;
        }

        if (Input.GetButtonUp("Vertical"))
        {
            anim.SetBool("pogoJump", false);
            rb.sharedMaterial = null;
        }

        if (anim.GetBool("pogoJump") && isGrounded)
        {
            float previousGroundHeight = -3.86f;
            float currentGroundHeight = transform.position.y;
            if (currentGroundHeight > previousGroundHeight + 0.2 )
            {
                previousGroundHeight = currentGroundHeight;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce *2);

            }
        }

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetFloat("vert", verticalInput);
        anim.SetBool("isGrounded", isGrounded);


        if (horizontalInput != 0)
        {
            sr.flipX = horizontalInput < 0;
        }

    }

    //2 button combo for prePutt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            canPrePutt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            canPrePutt = false;
        }
    }

    //PowerUps Below
    public void StartSpeedChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("SpeedChange");
        }
        else
        {
            StopCoroutine("SpeedChange");
            speed /= 2;
            StopCoroutine("SpeedChange");
        }
    }

    IEnumerator SpeedChange()
    {
        coroutineRunning = true;
        speed *= 2;

        yield return new WaitForSeconds(5.0f);

        speed /= 2;
        coroutineRunning = false;
    }



    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("JumpForceChange");
        }
        else
        {
            StopCoroutine("JumpForceChange");
            jumpForce /= 2;
            StopCoroutine("JumpForceChange");
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        coroutineRunning = false;
    }
    //PowerUps Above
}