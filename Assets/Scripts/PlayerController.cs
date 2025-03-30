using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wood;
    [SerializeField] private float jumpForce;

    [Header("References")]
    Rigidbody2D rb2D;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
        Movement();
        Jump();

    }
    #region Movement

    void Movement()
    {
        float yatay = Input.GetAxis("Horizontal") * speed;
        rb2D.velocity = new Vector2(yatay, rb2D.velocity.y);

        if(yatay != 0 && isGrounded )
        {
            anim.SetBool("isRunning",true);
        }
        else
        {
            anim.SetBool("isRunning", false);

        }

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, wood);

        if (hit.collider!=null && Input.GetKey(KeyCode.S))
        {
            hit.collider.excludeLayers = LayerMask.GetMask("Player");
        }

        if (yatay > 0 && !isFacingRight)
        {
            Flip();
        }

        else if (yatay < 0 && isFacingRight)
        {
            Flip();
        }
    }
    #endregion

    #region Flip
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
    #endregion

    #region Jump
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }
        else 
        {
            anim.SetBool("isJumping",false);
        }
    }
    #endregion


}



