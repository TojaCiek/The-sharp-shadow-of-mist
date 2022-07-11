using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int currentHealth;
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public HealthBar healthBar;
    public Text myLife;
    bool canDoubleJump;
    public Transform sterownikKamery;
    public Transform kamera;
    Vector3 pos;
    public Transform end;

    [SerializeField] private AudioClip hitsound;
    [SerializeField] private AudioClip potionsound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundLayer;
    AudioSource audioSource;
    public ShopScript Shop;
    public LicznikPotions potki;

    public Animator animator;

    public GameObject FallPoint;


    public static Vector2 lastCheckpointPosition = new Vector2(-24, -15);




    private void Start()
    {
        Shop = FindObjectOfType<ShopScript>();
        currentHealth = health;
        healthBar.SetMaxHealth(health);

        audioSource = GetComponent<AudioSource>();
    }



    void Update()
    {
        myLife.text = currentHealth.ToString();

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0 && IsGrounded())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }

        else if (!IsGrounded() || horizontal == 0)
        {
            audioSource.Stop();
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump"))
        {

            if (IsGrounded())
            {
                SoundManager.instance.PlaySound(jumpSound);
                rb.velocity = Vector2.up * jumpingPower;
                canDoubleJump = true;
            }


            else if (canDoubleJump && Shop.buyDoubleJump == true)
            {
                SoundManager.instance.PlaySound(jumpSound);
                rb.velocity = Vector2.up * jumpingPower;
                canDoubleJump = false;
            }


        }

        else if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
 

        else
        {
            animator.SetBool("isJumping", true);
        }

        Flip();

        if (Input.GetKeyDown(KeyCode.Q) && currentHealth < health && currentHealth > 0 && LicznikPotions.licznik.potions > 0)
        {
            currentHealth += 25;
            healthBar.SetHealth(currentHealth);
            LicznikPotions.licznik.potions -= LicznikPotions.licznik.PotionValue;

            if (currentHealth > health)
            {
                currentHealth = health;
            }

            SoundManager.instance.PlaySound(potionsound);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && LicznikPotions.licznik.potions <= 0)
        {
            Debug.Log("Nie masz potionkow!");
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        SoundManager.instance.PlaySound(hitsound);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("zwoj"))
        {
            Destroy(other.gameObject);
            end.position += new Vector3(0, +100, 0);
        }
        //respawn po smierci
        if (other.tag == "FallPoint")
        {
            Die();
        }

        if (other.gameObject.tag == "Boss")
        {
            TakeDamage(5);
        }

        if (other.gameObject.tag == "KoniecGry")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.enabled = false;
            this.GetComponent<Animator>().enabled = false;
        }
    }


   public void Die()
    {
        animator.SetBool("isDead", true);
        animator.SetBool("isJumping", false);
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        StartCoroutine(Checkpoint());
        ScoreManager coins = GetComponent<ScoreManager>();
        coins.score -= 50;      
        if (coins.score <= 0)
        {
            coins.score = 0;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;

    }


    IEnumerator Checkpoint()
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPosition;
        animator.SetBool("isDead", false);
        this.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;


        Start();
        pos.x = (sterownikKamery.transform.position.x - (sterownikKamery.GetComponent<PodazajZaGraczem>().blokadaKameryX));
        pos.y = transform.position.y;
        pos.z = -10;
        kamera.transform.position = pos;
        gameObject.layer = LayerMask.NameToLayer("Player");

    }

    


}
