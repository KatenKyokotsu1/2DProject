using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public float attackCooldown = 0.5f; // Saldýrý süresi
    private bool canAttack = true;

    private Animator animator;

    [Header("Saldýrý Ayarlarý")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            PerformAttack();
        }
    }

    public void PerformAttack()
    {
        if (!canAttack) return;
        canAttack = false;

        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Düþmana vuruldu: " + enemy.name);
            //enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
