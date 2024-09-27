using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFuego : MonoBehaviour
{
    public float rotationSpeed;
    private float actualRotation;
    public GameObject puerta;

    void Update()
    {
        actualRotation += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, actualRotation, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (puerta != null)
            {
                puerta.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}


