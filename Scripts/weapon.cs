using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public Animator animator;
    ShopOpener shop;

   [SerializeField] private AudioClip shurikensound;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            animator.SetTrigger("attack");
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot ()
    {
        SoundManager.instance.PlaySound(shurikensound);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }



}
