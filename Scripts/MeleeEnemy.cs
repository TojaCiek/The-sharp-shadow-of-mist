using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

[HideInInspector] public bool mustPatrol;
    private bool mustTurn;
        [SerializeField] private AudioClip EfektDzwiekowy;
    [SerializeField] private AudioClip EfektUderzenie;
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;



public int health = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    public float WalkSpeed;
    private float distToPlayer;
    public Transform EnemyGroundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Animator animator;
    public Transform player;

    public HealthBar healthBar;

    //References
    private Animator anim;
    private PlayerController playerHealth;



      private void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        mustPatrol = true;
        //TEST
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }

        }


         if(mustPatrol)
        {
           Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            mustPatrol = false;
            rb.velocity = Vector2.zero;
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
        WalkSpeed *= -1;
        mustPatrol = true;
    }

    public void TakeDamage (int damage)
    {
     currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        SoundManager.instance.PlaySound(EfektUderzenie);


        if (currentHealth <= 0 )
        {
            Die();
        }
    }


 private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
            SoundManager.instance.PlaySound(EfektDzwiekowy);
    }

    
    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<PlayerController>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

 


    void Die()
    {
        animator.SetBool("isdead", true);
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);


        this.enabled = false;
        
    }
}