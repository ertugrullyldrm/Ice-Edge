using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (animator == null)
        {
            Debug.LogError("No Animator component found on " + gameObject.name);
        }

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(move));
        }

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (animator != null)
        {
            if (isGrounded)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    animator.SetBool("isJumping", true);
                    animator.SetBool("isFalling", false);
                }
                else if (rb.velocity.y < 0)
                {
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFalling", true);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.collider.tag == "Triangle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.collider.tag == "FinalObject")
        {
            EndGame();
        }

        if (collision.collider.tag == "Enemigo" || collision.collider.tag == "Hazard")
        {
            TakeDamage(20); 
        }

        if (collision.collider.tag == "BalaEnemigo" || collision.collider.tag == "Hazard")
        {
            TakeDamage(20); 
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void EndGame()
    {
        Debug.Log("Game Over!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EndGame();
        }
        else
        {
            if (animator != null)
            {
                animator.SetTrigger("isHurt");
                StartCoroutine(ResetHurt());
            }
        }
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        if (animator != null)
        {
            animator.ResetTrigger("isHurt");
        }
    }
}







