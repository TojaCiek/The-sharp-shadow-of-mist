using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 0;
    public float TimeToLive = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, TimeToLive);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        nietoper nietoper = hitInfo.GetComponent<nietoper>();
        if(nietoper != null)
        {
            nietoper.TakeDamage(damage);
        }

        MeleeEnemy MeleeEnemy = hitInfo.GetComponent<MeleeEnemy>();
        if(MeleeEnemy != null)
        {
            MeleeEnemy.TakeDamage(damage);
        }

        Boss Boss = hitInfo.GetComponent<Boss>();
        if (Boss != null)
        {
            Boss.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    

}
