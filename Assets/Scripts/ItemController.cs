using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public BossController bossController; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();

            Destroy(gameObject);
        }
    }

    void CollectItem()
    {
        bossController.itemsRecolectados++;
        Debug.Log("Items recolectados: " + bossController.itemsRecolectados);
    }
}



