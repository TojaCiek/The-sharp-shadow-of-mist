using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_with_sword : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    private bool mustTurn, canShoot;

    public int health = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    public float WalkSpeed;
    
   
    
    public Transform EnemyGroundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Animator animator;
    public Transform player;
    
  

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        mustPatrol = true;
       
    }

    private void Update()
    {

        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(EnemyGroundCheck.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(WalkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        WalkSpeed *= -1;
        mustPatrol = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");



        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isdead", true);
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
        this.enabled = false;

    }
}
