using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int health;
    [SerializeField] private Transform groundCheck,attackPoint;
    [SerializeField] private LayerMask groundLayer,playerLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private int speed;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private GameObject Player;


    [Header("References")]
    Animator anim;
    Rigidbody2D rb;
    void Awake()
    {
        Player = GameObject.Find("Player");
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
        bool isPlayer = Physics2D.Raycast(attackPoint.position, (isFacingRight?Vector2.right:Vector2.left), 1, playerLayer);
        bool isRange = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        transform.Translate(Vector2.right * speed * Time.deltaTime * (isFacingRight ? 1 : -1));
        
        if (!isEmpty)
        {
            Flip();
        }
        if (isPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed );

        }

        if (isRange)
        {
            Attack();
        }
        else
        {
            anim.SetBool("isRange", false);

        }
    }
    #region Flip
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    #endregion
    
    #region Attack
    void Attack()

    {
        anim.SetBool("isRange", true);
        speed = 0;


    }
    #endregion

    #region Draw
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position,Vector2.down);
       
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(attackPoint.position, (isFacingRight ? Vector2.right : Vector2.left));

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
