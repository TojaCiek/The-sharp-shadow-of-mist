using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NietoperzWybuch : MonoBehaviour
{

       

    public float speed = 10f;
    public Rigidbody2D rb;
    public int damage;
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            collision.GetComponent<PlayerController>().TakeDamage(damage);
    }


}
