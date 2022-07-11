using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Dzwiek pocisku")]
    [SerializeField] private AudioClip PociskSFX;
    [SerializeField] private AudioClip enemyhitsound;

    
    [HideInInspector] public bool mustPatrol;
    private bool mustTurn, canShoot;
    [Header("Ustawienia")]
    public int health = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    public float WalkSpeed, range;
    public float shootSpeed;
    public float TimeBetweenShoot;
    private float distToPlayer;
    public Transform EnemyGroundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Animator animator;
    public Transform player;
    public Transform shootPoint;
    public GameObject EnemyBullet;
    private Animator anim;

    public HealthBar healthBar;



    private void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        mustPatrol = true;
        canShoot = true;
    }

    private void Update()
    {

        if(mustPatrol)
        {
           Patrol();
            animator.SetBool("walk", true);
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer <= range && player.gameObject.layer == 8)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            mustPatrol = false;
            animator.SetBool("walk", false);
            rb.velocity = Vector2.zero;
            if(canShoot)
             StartCoroutine(Shoot());
        }
        else
        {
            mustPatrol = true;
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(EnemyGroundCheck.position, 0.1f, groundLayer);
        }
    }

    void Patrol ()
    {
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(WalkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        shootPoint.Rotate(0f, 180f, 0f);
        WalkSpeed *= -1;
        mustPatrol = true;
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        SoundManager.instance.PlaySound(enemyhitsound);


        if (currentHealth <= 0 )
        {
            canShoot = false;
            Die();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(TimeBetweenShoot);
        Instantiate(EnemyBullet, shootPoint.position, shootPoint.rotation);
        canShoot = true;
        SoundManager.instance.PlaySound(PociskSFX);
        animator.SetTrigger("attack");
        
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
