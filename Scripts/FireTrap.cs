using UnityEngine;
using System.Collections;
 
public class FireTrap : MonoBehaviour
{
    [SerializeField] private int damage;
     [SerializeField] private AudioClip DzwiekPalnik;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool load;
 
    private bool triggered; //when the trap gets triggered
    private bool active; //when the trap is active and can hurt the player
 
    private PlayerController player;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered )
                StartCoroutine(ActivateFiretrap());
                load=true;
                
 
            player = collision.GetComponent<PlayerController>();
            SoundManager.instance.PlaySound(DzwiekPalnik);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
 
    private void Update()
    {
         if (active && player != null && load)
            player.TakeDamage(damage);
            load=false;
    }
 
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red; 
 
 
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; 
        active = true;
        anim.SetBool("activated", true);
 

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}