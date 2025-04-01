using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int health;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int speed;
    [SerializeField] private bool isFacingRight = true;


    [Header("References")]
    Animator anim;
    Rigidbody2D rb;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(health <= 0)
        {
            anim.SetBool("isDead",true);
            Destroy(this.gameObject, 1);
        }
        Patrol();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("TakeDamage");
    }
    void Patrol()
    {
        bool isEmpty = Physics2D.Raycast(groundCheck.position,Vector2.down,0.2f,groundLayer);

        transform.Translate(Vector2.right * speed * Time.deltaTime * (isFacingRight ? 1 : -1));
        
        if (!isEmpty)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position,Vector2.down);
    }
}
