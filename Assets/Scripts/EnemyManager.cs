using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int health;


    [Header("References")]
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(health <= 0)
        {
            anim.SetBool("isDead",true);
            Destroy(this.gameObject, 1);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("TakeDamage");
    }
}
