using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nietoper : MonoBehaviour
{
 [Header("Poruszanie w kierunku playera")]
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float speedEnemy;
    [SerializeField] AudioClip DzwiekHita;


    private bool canShoot;
 [Header("Zycie")]
    public int health = 100;
    public int currentHealth;

     [Header("Atak")]
    public Rigidbody2D rb;
    public float TimeBetweenShoot;
    public Animator animator;
    public Transform shootPoint;
    public GameObject EnemyBullet;

    public HealthBar healthBar;


public bool facingRight = false;


    void Start()
    {
        currentHealth = health;
          healthBar.SetMaxHealth(health);
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerPos.position) < distance && player.layer == 8)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);

if (player.transform.position.x < gameObject.transform.position.x && facingRight)
Flip ();
if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
Flip ();

                                if(canShoot)
             StartCoroutine(Shoot());

        }

        else
        {
            if(Vector2.Distance(transform.position, currentPos) <= 0)
            {

            }

        else
            {
            transform.position = Vector2.MoveTowards(transform.position, currentPos,speedEnemy * Time.deltaTime);
            }

        }

    }

    //obracanie nietoperza w strone player'a
         void Flip () 
           {
            facingRight = !facingRight;
            Vector3 tmpScale = gameObject.transform.localScale;
            tmpScale.x *= -1;
            gameObject.transform.localScale = tmpScale;
             }



    public void TakeDamage (int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        SoundManager.instance.PlaySound(DzwiekHita);


        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(TimeBetweenShoot);
        Instantiate(EnemyBullet, shootPoint.position, shootPoint.rotation);
        canShoot = true;
         animator.SetTrigger("attack");

    }

    void Die()
    {
        animator.SetBool("isdead", true);
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
           this.enabled = false;

        
    }

 
}
