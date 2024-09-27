using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    public string enemyTag = "Enemigo"; 
    public string deathAnimationName = "Death"; 
    public float destroyDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KillEnemies();
            Destroy(gameObject);
        }
    }

    private void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            Animator animator = enemy.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Death");
            }
            Destroy(enemy, destroyDelay);
        }
    }
}

