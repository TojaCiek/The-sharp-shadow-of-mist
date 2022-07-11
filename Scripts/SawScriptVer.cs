using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScriptVer : MonoBehaviour
{
    public float JakDaleko = 1.5f;  // Amount to move left and right from the start point
    public float Szypkosc = 2.0f;
    private Vector3 startPos;

    
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        collision.GetComponent<PlayerController>().TakeDamage(damage);
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 v = startPos;
        v.y += JakDaleko * Mathf.Sin(Time.time * Szypkosc);
        transform.position = v;
    }
}
