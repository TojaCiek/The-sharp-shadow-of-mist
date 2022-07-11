using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage;
    public float TimeToLive = 5f;
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, TimeToLive);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController PlayerController = hitInfo.GetComponent<PlayerController>();
        if (PlayerController != null)
        {
            PlayerController.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}