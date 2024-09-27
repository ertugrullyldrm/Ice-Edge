using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 5f;
    public Transform player;
    public float damageAmount = 10f;
    public int itemsRecolectados = 0;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("No Animator component found on " + gameObject.name);
        }

        if (player == null)
        {
            Debug.LogError("No player Transform assigned in " + gameObject.name);
        }
    }

    void Update()
    {
        if (animator == null || player == null) return;

        if (itemsRecolectados >= 4)
        {
            Die(); 
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else
        {
            StopAttack();
        }
    }

    void Attack()
    {
        animator.SetBool("isAttacking", true);
    }

    void StopAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    void Die()
    {
        Debug.Log("Die method called");
        animator.SetTrigger("Dead"); 
        Destroy(gameObject, 2f); 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}



