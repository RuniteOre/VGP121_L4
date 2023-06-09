using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	//component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //movement variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    public bool attack = false;
    public bool jumpAttack = false;

    //groundcheck variables
    public bool isGrounded;
    public bool floating;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;
	
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		
		if (speed <= 0) speed = 5.0f;
		if (jumpForce <= 0) jumpForce = 300.0f;
		if (groundCheckRadius <= 0) groundCheckRadius = 0.02f;

		if (!groundCheck) groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
		
		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector2.up * jumpForce);
		}
		
		if (Input.GetButtonDown("Fire1"))
		{
			attack = true;
		}
		
		if (Input.GetButtonUp("Fire1"))
		{
			attack = false;
		}
		
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        Vector2 MoveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = MoveDirection;
		
		anim.SetFloat("hInput", Mathf.Abs(hInput));
		anim.SetBool("GroundCheck", isGrounded);
		anim.SetBool("Attack", attack);
		
		//flip player sprite based on direction
        if (hInput < 0)
        {
            sr.flipX = true;
        }
        else if (hInput > 0)
        {
            sr.flipX = false;
        }
    }
}
