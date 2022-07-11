using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public float JakDaleko = 1.5f;  // Amount to move left and right from the start point
    public float Szypkosc = 2.0f;
    private Vector3 startPos;

    
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        collision.GetComponent<PlayerController>().TakeDamage(damage);
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 v = startPos;
        v.x += JakDaleko * Mathf.Sin(Time.time * Szypkosc);
        transform.position = v;
    }
}
