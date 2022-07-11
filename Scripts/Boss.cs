using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Poruszanie w kierunku playera")]
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float speedEnemy;
    [SerializeField] private AudioClip EfektDzwiekowy;
    [SerializeField] private AudioClip EfektSmierci;
    // obrazenia/strzelanie
    private bool canShoot;
    [Header("Zycie")]
    public int health = 100;
    public int currentHealth;

    [Header("Atak")]
    public Rigidbody2D rb;
    public float TimeBetweenShoot;
    public Collider2D bodyCollider;
    public Animator animator;
    public Transform shootPoint;
    public GameObject EnemyBullet;
    public HealthBar healthBar;
    public bool isSpawning = false;


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
        if (Vector2.Distance(transform.position, playerPos.position) < distance && player.layer == 8)
        {
            isSpawning = true;
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);

            if (player.transform.position.x < gameObject.transform.position.x && facingRight)
                Flip();
            if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
                Flip();

            if (canShoot)
                StartCoroutine(Shoot());

        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speedEnemy * Time.deltaTime);
            isSpawning = false;
        }

    }

    //obracanie w strone player'a
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        SoundManager.instance.PlaySound(EfektDzwiekowy);


        if (currentHealth <= 0)
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
        SoundManager.instance.PlaySound(EfektSmierci);
        animator.SetBool("isdead", true);
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
        this.enabled = false;



    }


}
